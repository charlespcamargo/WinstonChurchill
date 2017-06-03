using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.Backend.Utils
{
    public static class Configuracoes
    {
        public static string Url { get { return ConfigurationManager.AppSettings["URL.SITE"]?.ToString(); } }
        public static string PathAnexos { get { return ConfigurationManager.AppSettings["PATH.ANEXOS"]?.ToString(); } }
    }
}
