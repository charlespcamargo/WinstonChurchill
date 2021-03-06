﻿using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Business;
using WinstonChurchill.API.Common.Util;
using WinstonChurchill.Backend.Model.Enumeradores;

namespace WinstonChurchill.API.Autenticacao
{
    internal class CustomOAuthProviderJwt : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id não pode ser nulo");
                return Task.FromResult<object>(null);
            }

            //Procurando pelo Client Id
            var token = context.ClientId.Split(':');

            var client_id = token.First();
            var accessKey = token.Last();

            var applicationAccess = WebApplicationAccess.Find(client_id);

            if (applicationAccess == null)
            {
                context.SetError("invalid_clientId", "client_Id não encontrado");
                return Task.FromResult<object>(null);
            }

            if (applicationAccess.AccessKey != accessKey)
            {
                context.SetError("invalid_clientId", "access key não encontrado ou inválido");
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            // string cacheKey = $"usuario.{context.UserName}";
            Usuario usuario = UsuarioBusiness.New.Autenticar(new Usuario { Email = context.UserName, Senha = context.Password });

            if (usuario == null)
            {
                context.SetError("invalid_grant", "Usuário ou senha invalidos");
                return Task.FromResult<object>(null);
            }

            if (!usuario.Grupos.Any() || usuario.Grupos.FirstOrDefault().GrupoUsuario == null)
            {
                context.SetError("invalid_role", "Usuário sem configurações de acesso");
                return Task.FromResult<object>(null);
            }

            List<GrupoUsuarioRecurso> recursos = GrupoUsuarioRecursoBusiness.New.Carregar();

            if (recursos == null || !recursos.Any())
            {
                context.SetError("invalid_role", "Recursos não cadastrados");
                return Task.FromResult<object>(null);
            }

            if ((usuario.Grupos != null && usuario.Grupos.Any(a => a.GrupoUsuarioID != (int)eTipoGrupoUsuario.SuperUsuario && a.GrupoUsuarioID != (int)eTipoGrupoUsuario.Administrador)) 
                && !recursos.Any(a => usuario.Grupos.Any(w => w.GrupoUsuarioID == a.GrupoID)))
            {
                context.SetError("invalid_role", "Faltam configurações de regra ou o usuário possui um perfil inválido");
                return Task.FromResult<object>(null);
            }

            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim(ClaimTypes.Name, usuario.ID.ToString()));
            identity.AddClaim(new Claim("sub", usuario.Email));
            foreach (var item in usuario.Grupos.Where(w=>w.Ativo ==true))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, item.GrupoUsuarioID.ToString()));
            }

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", (context.ClientId == null) ? string.Empty : context.ClientId
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);

            //  CacheManager<Usuario>.GravarCache(usuario, cacheKey);

            return Task.FromResult<object>(null);
        }
    }
}