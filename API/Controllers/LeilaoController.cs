using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WinstonChurchill.Backend.Business;
using WinstonChurchill.Backend.Repository;
using WinstonChurchill.API.Common.Util.Captcha;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Utils;
using WinstonChurchill.API.Autenticacao;

namespace WinstonChurchill.API.Controllers
{
    [OwinAuthorize]
    [RoutePrefix("leilao")]
    public class LeilaoController : ApiController
    {
        [HttpPost, Route("listar")]
        public HttpResponseMessage Listar([FromBody] Leilao filtro)
        {
            try
            {
                if (filtro == null)
                    filtro = new Leilao();

                List<Leilao> data = LeilaoBusiness.New.Listar(filtro);

                return Request.CreateResponse(HttpStatusCode.OK, data);
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
                Leilao leilao = new Leilao();

                using (UnitOfWork UoW = new UnitOfWork())
                {
                    leilao = UoW.LeilaoRepository.Carregar(c => c.ID == id, o => o.OrderBy(by => by.ID), "Produto");
                    leilao.DuracaoRodadasDias = UoW.ParametroRepository.Listar(null).FirstOrDefault().DiasCadaRodada;

                }

                return Request.CreateResponse(HttpStatusCode.OK, leilao);
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

        //[HttpDelete, Route("{id}")]
        [HttpPost, Route("excluir/{id}")]
        public HttpResponseMessage Excluir(int id)
        {
            try
            {
                LeilaoBusiness.New.Excluir(id);

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
        public HttpResponseMessage Salvar([FromBody] Leilao leilao)
        {
            try
            {
                //LeilaoBusiness.New.Salvar(leilao);

                return Request.CreateResponse(HttpStatusCode.OK, leilao);
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
