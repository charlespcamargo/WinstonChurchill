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
using WinstonChurchill.Backend.Model.Enumeradores;

namespace WinstonChurchill.API.Controllers
{
    [OwinAuthorize]
    [RoutePrefix("usuario")]
    //[TokenAutenticacao]
    public class UsuarioController : ApiController
    {
        [HttpPost, Route("listar")]
        public HttpResponseMessage Listar([FromBody] Usuario filtro)
        {
            try
            {
                if (filtro == null)
                    filtro = new Usuario();

                Usuario usuario = UsuarioToken.Obter(this);
                List<Usuario> data = UsuarioBusiness.New.Listar(usuario, filtro);

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
                Usuario usuario = new Usuario();

                using (UnitOfWork uow = new UnitOfWork())
                {
                    usuario = uow.UsuarioRepository.Carregar(c => c.ID == id,
                                                             o => o.OrderBy(by => by.ID), "Grupos");

                    usuario.Senha = string.Empty;
                    usuario.Grupos.RemoveAll(p => p.Ativo == false);
                }

                return Request.CreateResponse(HttpStatusCode.OK, usuario);
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
        public HttpResponseMessage Salvar([FromBody] Usuario usuario)
        {
            try
            {
                usuario.ResponsavelID = UsuarioToken.ObterId(this);
                UsuarioBusiness.New.Salvar(usuario);

                return Request.CreateResponse(HttpStatusCode.OK, usuario);
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

        [AllowAnonymous, HttpGet, Route("listarRepresentantes")]
        public HttpResponseMessage ListarRepresentantes([FromBody] Usuario filtro)
        {
            try
            {
                if (filtro == null)
                    filtro = new Usuario();

                Usuario usuario = UsuarioToken.Obter(this);

                var key = this.Request.GetQueryNameValuePairs().Where(c => c.Key == "id").FirstOrDefault();
                if (!string.IsNullOrEmpty(key.Value))
                {
                    string termo = key.Value.ToUpper().Trim();
                    int codigo;

                    if (int.TryParse(termo, out codigo))
                        filtro.ID = int.Parse(termo);

                    if (filtro.ID == 0)
                        filtro.Nome = termo;

                    filtro.Ativo = true;
                }

                List<Usuario> lista = UsuarioBusiness.New.ListarRepresentantes(usuario, filtro);
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

        [AllowAnonymous, HttpGet, Route("listarResponsavel/{tipo}")]
        public HttpResponseMessage listarResponsavel(int tipo)
        {
            try
            {
                Usuario filtro = new Usuario();
                Usuario usuario = UsuarioToken.Obter(this);

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

                List<Usuario> lista = UsuarioBusiness.New.ListarResponsavel(usuario, filtro, tipo);

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

        [AllowAnonymous, HttpGet, Route("listarGrupos")]
        public HttpResponseMessage ListarGrupos()
        {
            try
            {
                Usuario usuario = UsuarioToken.Obter(this);

                List<string> lst = new List<string>();

                usuario.Grupos.ForEach(f =>
                  {
                      if (f.Ativo)
                      {
                          switch ((eTipoGrupoUsuario)f.GrupoUsuarioID)
                          {
                              case eTipoGrupoUsuario.SuperUsuario:
                                  lst.Add("S");
                                  break;
                              case eTipoGrupoUsuario.Administrador:
                                  lst.Add("A");
                                  break;
                              case eTipoGrupoUsuario.Fornecedor:
                                  lst.Add("F");
                                  break;
                              case eTipoGrupoUsuario.Comprador:
                                  lst.Add("C");
                                  break;
                              case eTipoGrupoUsuario.RepresentanteComercial:
                                  lst.Add("R");
                                  break;
                          }
                      }
                  });



                return Request.CreateResponse(HttpStatusCode.OK, lst);
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
