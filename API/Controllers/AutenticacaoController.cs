using Microsoft.Owin;
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
    public class AutenticacaoController : ApiController
    {
        [AllowAnonymous]
        [HttpPost, Route("autenticar")]
        public HttpResponseMessage Autenticar([FromBody] Usuario usuario)
        {
            try
            {
                //Vamos usar o Owen para gerar a autenticação. Se houver algum motivo implementamos o login nesta controller

                //if (string.IsNullOrEmpty(usuario.Nome))
                //    throw new ArgumentException("Informe o Usuário");

                //if (string.IsNullOrEmpty(usuario.Senha))
                //    throw new ArgumentException("Informe a Senha");

                usuario = UsuarioBusiness.New.Autenticar(usuario);

                //GerarToken(usuario);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (UnauthorizedAccessException uae)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new HttpError(uae.Message));
                throw new HttpResponseException(errorResponse);
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

        public IHttpActionResult Post([FromBody] string applicationName)
        {
            var webApplication = WebApplicationAccess.GrantApplication(applicationName);
            return Ok<WebApplicationAccess>(webApplication);
        }
    }
}
