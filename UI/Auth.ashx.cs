using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using UI.classes;

namespace WinstonChurchill.UI
{
    /// <summary>
    /// Summary description for Auth
    /// </summary>
    public class Auth : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            RestSharp.RestResponseBase objResponse = Redirecionar(context);
            context.Response.ContentType = "application/json"; // preenche o objeto para a mensagem de erro
            context.Response.Write(objResponse.Content);
        }

        public RestSharp.RestResponseBase Redirecionar(HttpContext context)
        {

            string json = new System.IO.StreamReader(context.Request.InputStream).ReadToEnd();
            dynamic deserializado = new System.Dynamic.ExpandoObject();

            if (!String.IsNullOrEmpty(json))
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                deserializado = serializer.Deserialize<dynamic>(json);
            }


            RestSharp.Method metodo = ObterTipoMetodo(context.Request.QueryString["tipo"], json);
            string api = ObterAPI(Configuracao.URL_API);
            string recurso = ObterRecurso(context.Request.QueryString);

            var client = new RestSharp.RestClient(api);
            var request = new RestSharp.RestRequest(recurso, metodo);

            if (!string.IsNullOrEmpty(json))
            {
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddBody((Object)deserializado);
            }

            //request.AddHeader("CodigoPrograma", context.Request.Headers["CodigoPrograma"]);
            //request.AddHeader("CodigoModulo", context.Request.Headers["CodigoModulo"]);
            //request.AddHeader("CodigoTarget", context.Request.Headers["CodigoTarget"]);
            //request.AddHeader("FreeAccess", context.Request.Headers["FreeAccess"]);

            RestSharp.IRestResponse response = client.Execute(request);
            RestSharp.RestResponseBase objResponse = (RestSharp.RestResponseBase)response;


            context.Response.StatusCode = (Int32)objResponse.StatusCode;
            context.Response.StatusDescription = objResponse.StatusDescription;

            return objResponse;
        }


        private string ObterAPI(string api)
        {
            if (!String.IsNullOrEmpty(api))
                return api;
            else
                throw new ArgumentException("A API não foi informada.");
        }

        private string ObterRecurso(System.Collections.Specialized.NameValueCollection queryString)
        {
            string recurso = queryString["url"];
            string id = queryString["id"];
            string token = queryString["token"];

            if (!String.IsNullOrEmpty(recurso))
            {
                if (!String.IsNullOrEmpty(id))
                    recurso = String.Concat(recurso, "?id=", id, "&");
                else
                    recurso = String.Concat(recurso, "?");

                //if (String.IsNullOrEmpty(token))
                //    recurso = String.Concat(recurso, "token=", AutenticacaoObjetos.TokenUsuario.token);
                //else
                //    recurso = String.Concat(recurso, "token=", token);

                return recurso;
            }
            else
            {
                throw new ArgumentException("A URL do Recurso não foi informada corretamente");
            }
        }

        private RestSharp.Method ObterTipoMetodo(string tipo, string json)
        {
            RestSharp.Method metodo = RestSharp.Method.GET;

            #region OBTEM O TIPO DO MÉTODO INFORMADO

            switch (tipo.ToUpper())
            {
                case "DELETE":
                    metodo = RestSharp.Method.DELETE;
                    break;
                case "GET":
                    metodo = RestSharp.Method.GET;
                    break;
                case "HEAD":
                    metodo = RestSharp.Method.HEAD;
                    break;
                case "MERGE":
                    metodo = RestSharp.Method.MERGE;
                    break;
                case "OPTIONS":
                    metodo = RestSharp.Method.OPTIONS;
                    break;
                case "PATCH":
                    metodo = RestSharp.Method.PATCH;
                    break;
                case "POST":
                    metodo = RestSharp.Method.POST;
                    break;
                case "PUT":
                    metodo = RestSharp.Method.PUT;
                    break;

                default:
                    throw new ArgumentException("O tipo do método é desconhecido...");
            }

            #endregion OBTEM O TIPO DO MÉTODO INFORMADO


            if (!string.IsNullOrEmpty(json) && metodo != RestSharp.Method.POST)
            {
                throw new ArgumentException("O método deveria ser POST, mas foi informado como: " + metodo.ToString());
            }


            return metodo;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}