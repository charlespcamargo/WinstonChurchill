﻿using System;
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
                Usuario usuario = UsuarioToken.Obter(this);

                if (filtro == null)
                    filtro = new Leilao();

                List<Leilao> data = LeilaoBusiness.New.Listar(usuario, filtro);

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
                Leilao leilao = LeilaoBusiness.New.Carregar(id);

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
        
        [HttpGet, Route("lances/{id}")]
        public HttpResponseMessage Lances(int id)
        {
            try
            {
                Usuario usuario = UsuarioToken.Obter(this);
                Leilao leilao = LeilaoBusiness.New.Lances(usuario, id);

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

        [HttpPost, Route("efetuarlance")]
        public HttpResponseMessage EfetuarLance([FromBody] LeilaoFornecedorRodada lance)
        {
            try
            {
                Usuario usuario = UsuarioToken.Obter(this);
                LeilaoBusiness.New.EfetuarLance(usuario, lance);
                
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

        [HttpPost, Route("salvarComprador")]
        public HttpResponseMessage SalvarComprador([FromBody] LeilaoComprador leilaoComprador)
        {
            try
            {
                Usuario usuario = UsuarioToken.Obter(this);
                LeilaoBusiness.New.SalvarComprador(usuario, leilaoComprador);

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

        [HttpPost, Route("salvarFornecedor")]
        public HttpResponseMessage SalvarFornecedor([FromBody] LeilaoFornecedor leilaoFornecedor)
        {
            try
            {
                Usuario usuario = UsuarioToken.Obter(this);
                LeilaoBusiness.New.SalvarFornecedor(usuario, leilaoFornecedor);

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
                Usuario usuario = UsuarioToken.Obter(this);
                LeilaoBusiness.New.Salvar(usuario, leilao);

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
