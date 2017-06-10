using System;
using System.Linq;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using WinstonChurchill.API.Autenticacao;
using WinstonChurchill.Backend.Utils;
using Microsoft.Owin.Security.Cookies;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace WinstonChurchill.API
{
    public partial class Startup
    {// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions authServerOptions = new OAuthAuthorizationServerOptions()
            {
                //Em produção se atentar que devemos usar HTTPS
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new CustomOAuthProviderJwt(),
                AccessTokenFormat = new CustomJwtFormat(Configuracoes.Url)
            };

            app.UseOAuthAuthorizationServer(authServerOptions);


            var issuer = Configuracoes.Url;
            var audience = WebApplicationAccess.WebApplicationAccessList.Select(x => x.Value.ClientId).AsEnumerable();
            var secretsSymmetricKey = (from x in WebApplicationAccess.WebApplicationAccessList
                                       select new SymmetricKeyIssuerSecurityTokenProvider(issuer, TextEncodings.Base64Url.Decode(x.Value.SecretKey))).ToArray();


            // Api controllers with an [Authorize] attribute will be validated with JWT
           // UsarAutenticacaoBearer(app, audience, secretsSymmetricKey);

            //UsarAutenticacaoCookie(app);

            //Vamos usar esta opção para gravar o token no cookie.. se não funcionar voltamos no bearer authentication

        }

        protected void UsarAutenticacaoBearer(IAppBuilder app, IEnumerable<string> audience, IEnumerable<IIssuerSecurityTokenProvider> secretsSymmetricKey)
        {
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = audience,
                    IssuerSecurityTokenProviders = secretsSymmetricKey
                });
        }

        //public void UsarAutenticacaoCookie(IAppBuilder app)
        //{
        //    app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
        //    {
        //        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        //        LoginPath = new PathString("/oauth2/token"),
        //        CookieSecure = CookieSecureOption.Always
        //    });
        //}
    }
}