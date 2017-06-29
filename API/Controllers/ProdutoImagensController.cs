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
    [RoutePrefix("produtoImagens")]
    public class ProdutoImagensController : ApiController
    {
        [HttpGet, Route("{idProduto}")]
        public HttpResponseMessage Carregar(int idProduto)
        {
            try
            {
                ProdutoImagem filtro = new ProdutoImagem();

                filtro.Imagem = new Imagem();
                filtro.Imagem.UsuarioID = UsuarioToken.ObterId(this);
                filtro.ProdutoID = idProduto;
                List<ProdutoImagem> lista = ProdutosImagensBusiness.New.Carregar(filtro);

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
        [HttpPost, Route("anexar/{idProduto}")]
        public HttpResponseMessage Anexar(int idProduto)
        {
            try
            {
                System.Web.HttpPostedFile arquivo = System.Web.HttpContext.Current.Request.Files[0];
                byte[] data = new Byte[arquivo.ContentLength];
                arquivo.InputStream.Read(data, 0, arquivo.ContentLength);

                ProdutoImagem produtoImagem = ProdutosImagensBusiness.New.Salvar(data, arquivo, idProduto);

                return Request.CreateResponse(HttpStatusCode.OK, produtoImagem);
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
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                ProdutoImagem filtro = new ProdutoImagem();

                Usuario usuario = UsuarioToken.Obter(this);
                filtro.Imagem = new Imagem();
                filtro.Imagem.UsuarioID = usuario.ID;
                filtro.ID = id;
                ProdutosImagensBusiness.New.Excluir(filtro);

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
    }
}
