using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace WinstonChurchill.API.Controllers
{
    [RoutePrefix("cep")]
    public class CepController : ApiController
    {
        [HttpGet, Route("{cep}")]
        public HttpResponseMessage Carregar(string cep)
        {
            try
            {
                StringBuilder strUrl = new StringBuilder();
                strUrl.Append("http://cep.republicavirtual.com.br/web_cep.php?");
                strUrl.Append("cep=" + cep.Replace("-", "") + "&formato=xml");

                WebRequest request = WebRequest.Create(strUrl.ToString());
                WebResponse response = request.GetResponse();
                StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));

                DataSet ds = new DataSet();
                ds.ReadXml(stream);

                stream.Dispose();

                CEP objCEP =  PupulaCEP(ds.Tables[0].Rows[0]);

                return Request.CreateResponse(HttpStatusCode.OK, objCEP);
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

        private CEP PupulaCEP(DataRow dr)
        {
            try
            {
                CEP cep = new CEP();
                cep.Resultado = (dr["resultado"].ToString() != "0" ? true : false);
                cep.ResultadoText = dr["resultado_txt"].ToString();
                cep.Estado = dr["uf"].ToString();
                cep.Cidade = dr["cidade"].ToString();
                cep.Bairro = dr["bairro"].ToString();
                cep.TipoLogradouro = dr["tipo_logradouro"].ToString();
                cep.Logradouro = dr["logradouro"].ToString();
                return cep;
            }
            catch
            {
                return null;
            }
        }

        public class CEP
        {
            public bool Resultado { get; set; }
            public string ResultadoText { get; set; }
            public string Estado { get; set; }
            public string Cidade { get; set; }
            public string Bairro { get; set; }
            public string TipoLogradouro { get; set; }
            public string Logradouro { get; set; }
        }
    }
}
