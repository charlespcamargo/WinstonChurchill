using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WinstonChurchill.API.Autenticacao;
using WinstonChurchill.Backend.Business;
using WinstonChurchill.Backend.Model;

namespace WinstonChurchill.API.Controllers
{
    [OwinAuthorize]
    [RoutePrefix("perfil")]
    public class PerfilController : ApiController
    {
        [HttpGet, Route("carregar")]
        public HttpResponseMessage Carregar()
        {
            try
            {
                Usuario usuario = UsuarioToken.Obter(this);
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

        [HttpPost, Route("salvar")]
        public HttpResponseMessage Salvar([FromBody] Usuario usuario)
        {
            try
            {
                Usuario salvo = UsuarioToken.Obter(this);
                salvo.Nome = usuario.Nome;
                salvo.Senha = usuario.Senha;
                salvo.SenhaNova = usuario.SenhaNova;
                salvo.SenhaNovaConfirmar = usuario.SenhaNovaConfirmar;

                UsuarioBusiness.New.Salvar(salvo);

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
