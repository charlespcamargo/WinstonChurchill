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
    [RoutePrefix("categoria")]
    public class CategoriaController : ApiController
    {
        [HttpGet, Route("listar")]
        public HttpResponseMessage Listar()
        {
            try
            {
                Categoria filtro = new Categoria();
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
                            filtro.Nome = termo;
                    }
                }
                    List<Categoria> lista = CategoriaBusiness.New.Listar(filtro);

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

        [AllowAnonymous]
        [HttpGet, Route("listarCombo")]
        public HttpResponseMessage ListarCombo()
        {
            try
            {
                Categoria filtro = new Categoria();

                var key = this.Request.GetQueryNameValuePairs().Where(c => c.Key == "id").FirstOrDefault();
                if (!string.IsNullOrEmpty(key.Value))
                {
                    string termo = key.Value.ToUpper().Trim();

                    {
                        int codigo;
                        if (int.TryParse(termo, out codigo))
                            filtro.ID = int.Parse(termo);

                        if (filtro.ID == 0)
                            filtro.Nome = termo;
                    }
                }
                List<Categoria> lista = CategoriaBusiness.New.Listar(filtro);

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
                Categoria filtro = new Categoria();
                filtro.UsuarioID = UsuarioToken.ObterId(this);
                filtro.ID = id;
                Categoria Categoria = CategoriaBusiness.New.Carregar(filtro);

                return Request.CreateResponse(HttpStatusCode.OK, Categoria);
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
                Categoria filtro = new Categoria();
                filtro.UsuarioID = UsuarioToken.ObterId(this);
                filtro.ID = id;
                CategoriaBusiness.New.Excluir(filtro);

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
        public HttpResponseMessage Salvar([FromBody] Categoria entidade)
        {
            try
            {
                Usuario usuario = UsuarioToken.Obter(this);
                CategoriaBusiness.New.Salvar(entidade, usuario);
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
