using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WinstonChurchill.Publico
{
    public partial class Redirecionar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int id = 0;

                if (int.TryParse(Request.QueryString["id"], out id))
                    RedirecionarPagina((ePagina)id);
            }

        }

        private void RedirecionarPagina(ePagina pagina)
        {
            string sistema = System.Configuration.ConfigurationManager.AppSettings["ADM"];
            string url = "";

            switch (pagina)
            {
                case ePagina.Participar:
                    url = sistema + "Login.aspx";
                    break;
                case ePagina.Institucional:
                    url = "/Institucional.aspx";
                    break;
                case ePagina.Contato:
                    url = "/Contato.aspx";
                    break;

                default:
                    break;
            }

            Response.Redirect(url, true);
        }

        public enum ePagina
        {
            Participar = 1,
            Institucional = 2,
            Contato = 3
        }

    }
}