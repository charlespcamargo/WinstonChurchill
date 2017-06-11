using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using WinstonChurchill.API.Common.Autenticacao;
using WinstonChurchill.Backend.Business;
using WinstonChurchill.Backend.Model;

namespace WinstonChurchill.API.Autenticacao
{
    public class OwinAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                base.OnAuthorization(actionContext);

                if (actionContext.Request.Headers.Authorization == null || actionContext.Request.Headers.Authorization.Parameter == null)
                    throw new UnauthorizedAccessException("Usuário não autenticado");

                string token = actionContext.Request.Headers.Authorization.Parameter;
                var result = new JwtSecurityToken(token);
                if (result != null && result.Claims != null && result.Claims.Any())
                {
                    List<GrupoUsuarioRecurso> recursos = GrupoUsuarioRecursoBusiness.New.Carregar();

                    var regra = result.Claims.Where(c => c.Type == "role").ToList();
                    if (regra == null || !regra.Any())
                        throw new UnauthorizedAccessException("Regra não encontrada");

                    //if (regra.Any(a => int.Parse(a.Value) == 1000) || regra.Any(a => int.Parse(a.Value) == 1001)) //1000 - Super admin e 1001 - Admin - Acesso livre
                    //    return;

                    if (recursos.Any(a => regra.Any(r => int.Parse(r.Value) == a.GrupoID)))
                    {
                        recursos = recursos.Where(w => regra.Any(r => int.Parse(r.Value) == w.GrupoID)).ToList();

                        GrupoUsuarioRecurso recursoUsuario = recursos.Where(p => p.Recurso == actionContext.ControllerContext.ControllerDescriptor.ControllerName).FirstOrDefault();
                        if (recursoUsuario == null)
                            throw new PlatformNotSupportedException("Regra não encontrada");
                    }
                    else
                        HandleUnauthorizedRequest(actionContext);
                }
            }
            catch (PlatformNotSupportedException ure)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotAcceptable, ure);
            }
            catch (UnauthorizedAccessException uae)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, uae);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool autorizado = base.IsAuthorized(actionContext);
            return autorizado;
        }
    }
}