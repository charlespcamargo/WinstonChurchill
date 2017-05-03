using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UI.classes;

namespace UI.pages
{
    public partial class MasterDefault : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hfURLAPI.Value = Configuracao.URL_API;
        }
    }
}