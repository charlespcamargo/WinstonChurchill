using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WinstonChurchill.API.Autenticacao;
using WinstonChurchill.API.Common.Atributos;
using WinstonChurchill.Backend.Business;
using WinstonChurchill.Backend.Model;

namespace WinstonChurchill.API.Controllers
{
    [OwinAuthorize]
    [RoutePrefix("parametro")]
    public class ParametroController : ApiController
    {
        [HttpGet, AllowAnonymous, Route("carregar")]
        public HttpResponseMessage Carregar()
        {
            try
            {
                Parametro Parametro = ParametroBusiness.New.Carregar();

                return Request.CreateResponse(HttpStatusCode.OK, Parametro);
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

        [ValidateModel]
        [HttpPost, Route("salvar")]
        public HttpResponseMessage Salvar([FromBody] Parametro entidade)
        {
            try
            {
                Usuario usuario = UsuarioToken.Obter(this);
                ParametroBusiness.New.Salvar(entidade);
                return Request.CreateResponse(HttpStatusCode.OK, entidade);
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
