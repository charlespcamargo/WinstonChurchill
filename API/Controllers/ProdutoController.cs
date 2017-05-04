using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WinstonChurchill.Backend.Business;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;
using WinstonChurchill.Backend.Utils;

namespace WinstonChurchill.API.Controllers
{
    [RoutePrefix("produto")]
    //[TokenAutenticacao]
    public class ProdutoController : ApiController
    {

        [HttpPost, Route("listar")]
        public HttpResponseMessage Listar([FromBody] Array parametros)
        {
            try
            {
                int current = 0;/*Convert.ToInt32(Request.GetQueryNameValuePairs().Where(c => c.Key == "current").FirstOrDefault().Value);*/
                int rowCount = 0;/*Convert.ToInt32(Request.GetQueryNameValuePairs().Where(c => c.Key == "rowCount").FirstOrDefault().Value);*/
                string busca = "";/*Request.GetQueryNameValuePairs().Where(c => c.Key == "searchPhrase").FirstOrDefault().Value;*/

                DataTableResponseData<Produtos> dataSource = ProdutoBusiness.New.Listar(current, rowCount, busca);

                return Request.CreateResponse(HttpStatusCode.OK, dataSource);
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
                Produtos talento = new Produtos();

                using (UnitOfWork uow = new UnitOfWork())
                {
                    talento = uow.ProdutosRepository.Carregar(c => c.ID == id,
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
                ProdutoBusiness.New.Excluir(id);

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
        public HttpResponseMessage Salvar([FromBody] Produtos entidade)
        {
            try
            {
                ///***PEGA DO  TOKEN DE AUTENTICAÇÃO **///
                Usuario usuario = UsuarioBusiness.New.Carregar(1);
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
    }
}
