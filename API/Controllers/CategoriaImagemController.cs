using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WinstonChurchill.Backend.Business;
using WinstonChurchill.Backend.Model;

namespace WinstonChurchill.API.Controllers
{
    [RoutePrefix("categoriaImagem")]
    public class CategoriaImagemController : ApiController
    {
        [HttpGet, Route("{idCategoria}")]
        public HttpResponseMessage Carregar(int idCategoria)
        {
            try
            {
                CategoriaImagem filtro = new CategoriaImagem();
                ///***PEGA DO  TOKEN DE AUTENTICAÇÃO **///
                Usuario usuario = UsuarioBusiness.New.Carregar(1);
                filtro.Imagem = new Imagem();
                filtro.Imagem.UsuarioID = usuario.ID;
                filtro.CategoriaID = idCategoria;
                List<CategoriaImagem> lista = CategoriaImagemBusiness.New.Carregar(filtro);

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

        [HttpPost, Route("anexar/{idCategoria}")]
        public HttpResponseMessage Anexar(int idCategoria)
        {
            try
            {
                ///***PEGA DO  TOKEN DE AUTENTICAÇÃO **///
                Usuario usuario = UsuarioBusiness.New.Carregar(1);

                System.Web.HttpPostedFile arquivo = System.Web.HttpContext.Current.Request.Files[0];
                byte[] data = new Byte[arquivo.ContentLength];
                arquivo.InputStream.Read(data, 0, arquivo.ContentLength);

                CategoriaImagem CategoriaImagem = CategoriaImagemBusiness.New.Salvar(data, arquivo, usuario, idCategoria);

                return Request.CreateResponse(HttpStatusCode.OK, CategoriaImagem);
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
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                CategoriaImagem filtro = new CategoriaImagem();
                ///***PEGA DO  TOKEN DE AUTENTICAÇÃO **///
                Usuario usuario = UsuarioBusiness.New.Carregar(1);
                filtro.Imagem = new Imagem();
                filtro.Imagem.UsuarioID = usuario.ID;
                filtro.ID = id;
                CategoriaImagemBusiness.New.Excluir(filtro);

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
