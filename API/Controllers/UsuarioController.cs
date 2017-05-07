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

namespace WinstonChurchill.API.Controllers
{
    [RoutePrefix("usuario")]
    public class UsuarioController : ApiController
    {
        [HttpPost , Route("listar")]
        public HttpResponseMessage Listar([FromBody] Usuario filtro)
        {
            try
            {
                if (filtro == null)
                    filtro = new Usuario();

                ///***PEGA DO  TOKEN DE AUTENTICAÇÃO **///
                Usuario usuario = UsuarioBusiness.New.Carregar(1);
                filtro.ID = usuario.ID;
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

        [HttpGet, Route("obter/{id}")]
        public HttpResponseMessage Obter(int id)
        {
            try
            {
                Usuario talento = new Usuario();

                using (UnitOfWork uow = new UnitOfWork())
                {
                    talento = uow.UsuarioRepository.Carregar(c => c.ID == id,
                                                             o => o.OrderBy(by => by.ID));
                }

                return Request.CreateResponse(HttpStatusCode.OK, talento);
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

        [HttpPost, Route("excluir/{id}")]
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
        public HttpResponseMessage Salvar([FromBody] Usuario talento)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    string senha = Encrypting.sha512encrypt("");


                    if (talento.ID == 0)
                        uow.UsuarioRepository.Inserir(talento);
                    else
                        uow.UsuarioRepository.Alterar(talento);

                    uow.Save();
                }

                return Request.CreateResponse(HttpStatusCode.OK, talento);
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
