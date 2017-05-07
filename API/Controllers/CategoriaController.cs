using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WinstonChurchill.Backend.Business;
using WinstonChurchill.Backend.Model;

namespace WinstonChurchill.API.Controllers
{
    [RoutePrefix("categoria")]
    public class CategoriaController : ApiController
    {
        [HttpGet, Route("listar")]
        public HttpResponseMessage Listar()
        {
            try
            {
                Categoria filtro = new Categoria();
                ///***PEGA DO  TOKEN DE AUTENTICAÇÃO **///
                Usuario usuario = UsuarioBusiness.New.Carregar(1);
                filtro.UsuarioID = usuario.ID;

                string termo = this.Request.GetQueryNameValuePairs().Where(c => c.Key == "id").FirstOrDefault().Value.ToUpper().Trim();
                if (!string.IsNullOrEmpty(termo))
                {
                    int codigo;
                    if (int.TryParse(termo, out codigo))
                        filtro.ID = int.Parse(termo);

                    if (filtro.ID == 0)
                        filtro.Nome = termo;
                }
               List<Categoria> lista = CategoriaBusiness.New.Listar(filtro);

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
    }
}
