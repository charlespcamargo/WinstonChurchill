using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WinstonChurchill.Backend.Business;
using WinstonChurchill.Backend.Repository;
using WinstonChurchill.API.Common.Util.Captcha;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Utils;
using WinstonChurchill.API.Autenticacao;

namespace WinstonChurchill.API.Controllers
{
    [OwinAuthorize]
    [RoutePrefix("usuario")]
    //[TokenAutenticacao]
    public class UsuarioController : ApiController
    {
        [HttpPost, Route("listar")]
        public HttpResponseMessage Listar([FromBody] Usuario filtro)
        {
            try
            {
                if (filtro == null)
                    filtro = new Usuario();

                filtro.ID = UsuarioToken.ObterId(this);
                List<Usuario> data = UsuarioBusiness.New.Listar(filtro);

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (ArgumentException aex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError(aex.Message));
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.Conflict, new HttpError(ex.Message));
                throw new HttpResponseException(errorResponse);
            }
        }

        [HttpGet, Route("{id}")]
        public HttpResponseMessage Carregar(int id)
        {
            try
            {
                Usuario usuario = new Usuario();

                using (UnitOfWork uow = new UnitOfWork())
                {
                    usuario = uow.UsuarioRepository.Carregar(c => c.ID == id,
                                                             o => o.OrderBy(by => by.ID), "Grupos");

                    usuario.Grupos.RemoveAll(p => p.Ativo == false);
                }

                return Request.CreateResponse(HttpStatusCode.OK, usuario);
            }
            catch (ArgumentException aex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError(aex.Message));
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.Conflict, new HttpError(ex.Message));
                throw new HttpResponseException(errorResponse);
            }
        }

        [HttpDelete, Route("{id}")]
        public HttpResponseMessage Excluir(int id)
        {
            try
            {
                UsuarioBusiness.New.Excluir(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ArgumentException aex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError(aex.Message));
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.Conflict, new HttpError(ex.Message));
                throw new HttpResponseException(errorResponse);
            }
        }

        [HttpPost, Route("salvar")]
        public HttpResponseMessage Salvar([FromBody] Usuario usuario)
        {
            try
            {
                usuario.ResponvelID = UsuarioToken.ObterId(this);
                UsuarioBusiness.New.Salvar(usuario);

                return Request.CreateResponse(HttpStatusCode.OK, usuario);
            }
            catch (ArgumentException aex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError(aex.Message));
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.Conflict, new HttpError(ex.Message));
                throw new HttpResponseException(errorResponse);
            }
        }
    }
}
