using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WinstonChurchill.Backend.Business;
using WinstonChurchill.Backend.Model;

namespace WinstonChurchill.API.Autenticacao
{
    public class OwinAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            string token = actionContext.Request.Headers.Authorization.Parameter;
            var result = new JwtSecurityToken(token);
            if (result != null && result.Claims != null && result.Claims.Any())
            {
                List<GrupoUsuarioRecurso> recursos = GrupoUsuarioRecursoBusiness.New.Carregar();
                var regra = result.Claims.Where(c => c.Type == "role").FirstOrDefault();
                if (regra == null)
                    throw new UnauthorizedAccessException("Regra não encontrada");

                if (recursos.Any(a => a.GrupoID == int.Parse(regra.Value)))
                {
                    GrupoUsuarioRecurso recursoUsuario = recursos.Where(p => p.Recurso == actionContext.ControllerContext.ControllerDescriptor.ControllerName).FirstOrDefault();
                    if(recursoUsuario == null)
                        HandleUnauthorizedRequest(actionContext);
                }
                else
                    HandleUnauthorizedRequest(actionContext);
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