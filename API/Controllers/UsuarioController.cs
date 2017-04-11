using WinstonChurchill.Backend.Repository;
using WinstonChurchill.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WinstonChurchill.Backend.Utils;
using WinstonChurchill.Backend.Business;

namespace WinstonChurchill.API.Controllers
{
    [RoutePrefix("usuario")]
    public class UsuarioController : ApiController
    {
        [HttpGet, Route("listar")]
        public HttpResponseMessage Listar()
        {
            try
            {
                int current = Convert.ToInt32(Request.GetQueryNameValuePairs().Where(c => c.Key == "current").FirstOrDefault().Value);
                int rowCount = Convert.ToInt32(Request.GetQueryNameValuePairs().Where(c => c.Key == "rowCount").FirstOrDefault().Value);
                string busca = Request.GetQueryNameValuePairs().Where(c => c.Key == "searchPhrase").FirstOrDefault().Value;

                BootgridResponseData<Usuario> dataSource = UsuarioBusiness.New.Listar(current, rowCount, busca);

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
