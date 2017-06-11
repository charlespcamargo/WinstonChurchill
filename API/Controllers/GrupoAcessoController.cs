using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WinstonChurchill.API.Autenticacao;
using WinstonChurchill.Backend.Model;
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
                    UsuarioXGrupoUsuario grupoUsuario = uow.UsuarioXGrupoUsuarioRepository.Carregar(p => p.UsuarioID == usuarioId, ord => ord.OrderBy(p => p.ID), "GrupoUsuario");
                    if (grupoUsuario == null)
                        throw new ArgumentException("Usuário sem grupo cadastrado");

                    if (grupoUsuario.GrupoUsuario.ID == 1000)    //Se for super usuário retorna o grupo de admin na consulta, senão apenas os grupos que estão abaixo do admin
                    {
                        lista = uow.GrupoUsuarioRepository.Listar(p => p.ID != 1000); //Apenas não retorna o super usuário porque ele é único
                    }
                    else {
                        lista = uow.GrupoUsuarioRepository.Listar(p => p.ID != 1000 && p.ID != 1001);
                    } 

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
