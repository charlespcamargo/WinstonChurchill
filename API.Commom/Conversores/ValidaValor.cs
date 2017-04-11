using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.API.Common.Conversores
{
    public static class ValidaValor
    {
        public static object IsBlank(object value, object replacement)
        {
            if (IsBlank(value))
                return replacement;
            else
                return value;
        }

        public static bool IsBlank(object AObjeto)
        {
            if (Convert.IsDBNull(AObjeto))
                return true;
            else
                if (AObjeto == null)
                return true;
            else
                    if (AObjeto is string)
                if (((string)AObjeto) == "")
                    return true;
                else
                    return false;
            else
                        if ((AObjeto is int || AObjeto is uint))
                if (((int)AObjeto) == 0)
                    return true;
                else
                    return false;
            else if (AObjeto is long)
            {
                if (((long)AObjeto) == 0)
                    return true;
                else
                    return false;
            }
            else
                if (AObjeto is decimal)
                if (((decimal)AObjeto) == 0)
                    return true;
                else
                    return false;
            else
                    if (AObjeto is double)
                if (((double)AObjeto) == 0)
                    return true;
                else
                    return false;
            else
                        if (AObjeto is DateTime)
                return IsBlankDate((DateTime)AObjeto);
            else
                return false;
        }

        public static bool IsBlankDate(DateTime ADate)
        {
            if (ADate == DateTime.MinValue || ADate == null)
                return true;
            else
                return (ADate == new DateTime(1, 1, 1, 0, 0, 0));
        }

        public static bool IsDate(string date)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(date, CultureInfo.GetCultureInfo("pt-BR"));

                if (IsBlankDate(dt))
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                string extext = ex.Message;
                return false;
            }
        }
    }
}