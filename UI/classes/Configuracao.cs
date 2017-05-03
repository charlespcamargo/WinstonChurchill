using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UI.classes
{
    public static class Configuracao
    {
        public static string URL_API
        {
            get
            {
                return ConfigurationManager.AppSettings["URL.API"].ToString();
            }
        }
    }
}