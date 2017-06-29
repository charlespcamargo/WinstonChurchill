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
    [RoutePrefix("parceiroNegocio")]
    public class ParceiroNegocioController : ApiController
    {
        [AllowAnonymous]
        [HttpGet, Route("listarComprador")]
        public HttpResponseMessage ListarComprador()
        {
            int[] tipo = new int[] { 1, 3 };
            return Listar(tipo, null);
        }

        [AllowAnonymous]
        [HttpGet, Route("listarFornecedor")]
        public HttpResponseMessage ListarFornecedor()
        {
            int[] tipo = new int[] { 2, 3 };
            return Listar(tipo, null);
        }

        [HttpPost, Route("listar")]
        public HttpResponseMessage Listar([FromBody] ParceiroNegocio filtro)
        {
            return Listar(null, filtro);
        }

        private HttpResponseMessage Listar(int[] tipo, ParceiroNegocio filtro)
        {
            try
            {
                if (filtro == null)
                    filtro = new ParceiroNegocio();
                filtro.UsuarioID = UsuarioToken.ObterId(this);

                var key = this.Request.GetQueryNameValuePairs().Where(c => c.Key == "id").FirstOrDefault();
                if (!string.IsNullOrEmpty(key.Value))
                {
                    string termo = key.Value.ToUpper().Trim();

                    {
                        int codigo;
                        if (int.TryParse(termo, out codigo))
                            filtro.ID = int.Parse(termo);

                        if (filtro.ID == 0)
                            filtro.RazaoSocial = termo;
                    }
                }

                List<ParceiroNegocio> lista = ParceiroNegocioBusiness.New.Listar(filtro, tipo);
                return Request.CreateResponse(HttpStatusCode.OK, lista);
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
                ParceiroNegocio filtro = new ParceiroNegocio();

                Usuario usuario = UsuarioToken.Obter(this);
                filtro.UsuarioID = usuario.ID;
                filtro.ID = id;
                ParceiroNegocio ParceiroNegocio = ParceiroNegocioBusiness.New.Carregar(filtro);

                return Request.CreateResponse(HttpStatusCode.OK, ParceiroNegocio);
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
                ParceiroNegocio filtro = new ParceiroNegocio();

                Usuario usuario = UsuarioToken.Obter(this);
                filtro.UsuarioID = usuario.ID;
                filtro.ID = id;
                ParceiroNegocioBusiness.New.Excluir(filtro);

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
        public HttpResponseMessage Salvar([FromBody] ParceiroNegocio entidade)
        {
            try
            {
                Usuario usuario = UsuarioToken.Obter(this);
                ParceiroNegocioBusiness.New.Salvar(entidade, usuario);
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
