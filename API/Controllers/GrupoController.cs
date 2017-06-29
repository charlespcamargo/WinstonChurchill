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
    [RoutePrefix("grupo")]
    public class GrupoController : ApiController
    {
        [HttpGet, Route("listar/{tipo}")]
        public HttpResponseMessage Listar(int? tipo)
        {
            try
            {
                Grupo filtro = new Grupo();
                filtro.UsuarioID = UsuarioToken.ObterId(this);

                if (tipo.HasValue && tipo > 0)
                    filtro.TipoGrupo = Convert.ToInt32(tipo);

                List<Grupo> lista = GrupoBusiness.New.Listar(filtro);

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
        [HttpGet, Route("listarCombo/{tipo}")]
        public HttpResponseMessage ListarCombo(int? tipo)
        {
            try
            {
                Grupo filtro = new Grupo();
                filtro.UsuarioID = UsuarioToken.ObterId(this);

                var key = this.Request.GetQueryNameValuePairs().Where(c => c.Key == "id").FirstOrDefault();
                if (!string.IsNullOrEmpty(key.Value))
                {
                    string termo = key.Value.ToUpper().Trim();
                    int codigo;
                    if (int.TryParse(termo, out codigo))
                        filtro.ID = int.Parse(termo);

                    if (filtro.ID == 0)
                        filtro.Nome = termo;
                }

                if (tipo.HasValue && tipo > 0)
                    filtro.TipoGrupo = Convert.ToInt32(tipo);

                List<Grupo> lista = GrupoBusiness.New.Listar(filtro);

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
                Grupo filtro = new Grupo();

                Usuario usuario = UsuarioToken.Obter(this);
                filtro.UsuarioID = usuario.ID;
                filtro.ID = id;
                Grupo Grupo = GrupoBusiness.New.Carregar(filtro);

                return Request.CreateResponse(HttpStatusCode.OK, Grupo);
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
                Grupo filtro = new Grupo();

                Usuario usuario = UsuarioToken.Obter(this);
                filtro.UsuarioID = usuario.ID;
                filtro.ID = id;
                GrupoBusiness.New.Excluir(filtro);

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
        public HttpResponseMessage Salvar([FromBody] Grupo entidade)
        {
            try
            {
                Usuario usuario = UsuarioToken.Obter(this);
                GrupoBusiness.New.Salvar(entidade, usuario);
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
