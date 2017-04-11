﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.API.Common.Extensions
{
    public static class FormatsExtensions
    {
        public static string FormatGcdMask(this string value)
        {
            string textoFormatado = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                textoFormatado = value.Substring(0, 1);
                textoFormatado += "." + value.Substring(1, 1);
                textoFormatado += "." + value.Substring(2, 2);
                textoFormatado += ".00";
            }

            return textoFormatado;
        }

        public static string FormatAtvMask(this string value)
        {
            string textoFormatado = string.Empty;

            if (value != null && !string.IsNullOrEmpty(value.Trim()) && value.Length >= 6)
            {
                textoFormatado = value.Substring(0, 1);
                textoFormatado += "." + value.Substring(1, 1);
                textoFormatado += "." + value.Substring(2, 2);
                textoFormatado += "." + value.Substring(4, 2);
            }
            return textoFormatado;
        }
    }
}
