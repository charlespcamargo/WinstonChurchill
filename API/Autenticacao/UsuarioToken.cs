using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using WinstonChurchill.API.Controllers;
using WinstonChurchill.Backend.Business;
using WinstonChurchill.Backend.Model;

namespace WinstonChurchill.API.Autenticacao
{
    public class UsuarioToken
    {
        internal static Usuario Obter(ApiController controller)
        {
            int usuarioId = ObterId(controller);
            return UsuarioBusiness.New.Carregar(usuarioId);
        }

        internal static int ObterId(ApiController controller)
        {
            string token = controller.ActionContext.Request.Headers.Authorization.Parameter;
            var result = new JwtSecurityToken(token);
            if (result != null && result.Claims != null && result.Claims.Any())
            {
                var sid = result.Claims.Where(c => c.Type == "unique_name").FirstOrDefault();
                if (sid == null)
                    throw new ArgumentException("Usuário não encontrado");
                int usuarioId = int.Parse(sid.Value);
                return usuarioId;
            }
            throw new ArgumentException("Usuário não encontrado");
        }
    }
}