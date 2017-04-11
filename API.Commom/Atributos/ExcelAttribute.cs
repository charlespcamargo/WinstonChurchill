using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.API.Common.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ExcelSpreadSheetAttribute : Attribute
    {
        string nomeAba = string.Empty;

        public ExcelSpreadSheetAttribute(string nomeAba)
        {
            this.nomeAba = nomeAba;
        }
    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ExcelColumnAttribute : Attribute
    {
        ExcelColuna coluna;
        ExcelFormatoTipo tipo;
        Object valor;

        public bool Formula { get; set; }

        public string SintaxeFormula { get; set; }

        public ExcelColumnAttribute(ExcelColuna coluna)
        {
            this.coluna = coluna;
        }

        public ExcelColumnAttribute(bool formula)
        {
            this.Formula = formula;
        }

        public ExcelColumnAttribute(bool formula, string sintaxeFormula)
        {
            this.Formula = formula;
            this.SintaxeFormula = sintaxeFormula;
        }

        public ExcelColumnAttribute(ExcelColuna coluna, Object valor)
        {
            this.coluna = coluna;
            this.valor = valor;
        }

        public ExcelColumnAttribute(ExcelColuna coluna, bool formula)
        {
            this.coluna = coluna;
            this.Formula = formula;
        }

        public ExcelColumnAttribute(ExcelColuna coluna, ExcelFormatoTipo tipo, Object valor, bool formula)
        {

            this.coluna = coluna;
            this.tipo = tipo;
            this.valor = valor;
            this.Formula = formula; 
        }

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ExcelColumnNotExportableAttribute : Attribute
    {

    }

    public enum ExcelColuna
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
        F = 6,
        G = 7,
        H = 8,
        I = 9,
        J = 10,
        K = 11,
        L = 12,
        M = 13,
        N = 14,
        O = 15,
        P = 16,
        Q = 17,
        R = 18,
        S = 19,
        T = 20,
        U = 21,
        V = 22,
        X = 23,
        Z = 24,
        W = 25,
        Y = 26,
    }

    public enum ExcelFormatoTipo
    {
        Texto = 0,
        Inteiro = 1,
        Decimal = 2,
        Real = 3,
        Dolar = 4,
        Date = 5,
        DateTime = 6
    }


}
