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
    [RoutePrefix("produto")]
    public class ProdutoController : ApiController
    {

        [HttpPost, HttpGet, Route("listar")]
        public HttpResponseMessage Listar([FromBody] Produto filtro)
        {
            try
            {
                if (filtro == null)
                    filtro = new Produto();
                filtro.UsuarioID = UsuarioToken.ObterId(this);

                List<Produto> lista = ProdutoBusiness.New.Listar(filtro);
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
        [HttpPost, HttpGet, Route("listarCombo")]
        public HttpResponseMessage ListarCombo([FromBody] Produto filtro)
        {
            try
            {
                if (filtro == null)
                    filtro = new Produto();
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

                List<Produto> lista = ProdutoBusiness.New.Listar(filtro);
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
                Produto filtro = new Produto();
                
                Usuario usuario= UsuarioToken.Obter(this);
                filtro.UsuarioID = usuario.ID;
                filtro.ID = id;
                Produto produto = ProdutoBusiness.New.Carregar(filtro);

                return Request.CreateResponse(HttpStatusCode.OK, produto);
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
                Produto filtro = new Produto();
                
                Usuario usuario = UsuarioToken.Obter(this);
                filtro.UsuarioID = usuario.ID;
                filtro.ID = id;
                ProdutoBusiness.New.Excluir(filtro);

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
        public HttpResponseMessage Salvar([FromBody] Produto entidade)
        {
            try
            {
               
                Usuario usuario = UsuarioToken.Obter(this);
                ProdutoBusiness.New.Salvar(entidade, usuario);
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

        [HttpGet, Route("listarCaracteristicas/{idProduto}")]
        public HttpResponseMessage ListarCaracteristicas(int idProduto)
        {
            try
            {
                List<CaracteristicaProduto> lista = ProdutoBusiness.New.ListarCaracteristicas(idProduto);
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
    }
}
