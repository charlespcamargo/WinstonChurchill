using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WinstonChurchill.API.Autenticacao;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Model.Enumeradores;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.API.Controllers
{
    [OwinAuthorize]
    [RoutePrefix("grupoacesso")]
    public class GrupoAcessoController : ApiController
    {
        [HttpGet, Route("listar")]
        public HttpResponseMessage Listar()
        {
            try
            {
                List<GrupoUsuario> lista = null;
                int usuarioId = UsuarioToken.ObterId(this);

                using (UnitOfWork uow = new UnitOfWork())
                {
                    List<UsuarioXGrupoUsuario> lstGrupoUsuario = uow.UsuarioXGrupoUsuarioRepository.Listar(p => p.UsuarioID == usuarioId,
                                                                                                           o => o.OrderByDescending(p => p.ID), "GrupoUsuario");
                    if (lstGrupoUsuario == null || lstGrupoUsuario.Count == 0)
                        throw new ArgumentException("Usuário sem grupo cadastrado");

                    if (lstGrupoUsuario.Exists(e => e.GrupoUsuario.ID == (int)eTipoGrupoUsuario.SuperUsuario || e.GrupoUsuario.ID == (int)eTipoGrupoUsuario.Administrador))    //Se for super usuário retorna o grupo de admin na consulta, senão apenas os grupos que estão abaixo do admin
                        lista = uow.GrupoUsuarioRepository.Listar(p => p.ID != (int)eTipoGrupoUsuario.SuperUsuario); //Apenas não retorna o super usuário porque ele é único
                    else
                        lista = uow.GrupoUsuarioRepository.Listar(p => p.ID == lstGrupoUsuario.First().GrupoUsuarioID);

                    return Request.CreateResponse(HttpStatusCode.OK, lista);
                }
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
