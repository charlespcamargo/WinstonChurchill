using System;

namespace WinstonChurchill.API.Common.Mapeador
{
    [Serializable()]
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class EDIMapper : Attribute
    {
        public bool EhObrigatrio { get; set; }
        public int PosicaoInicial { get; set; }
        public int PosicaoFinal { get; set; }
        public int QtdCaracteres { get; set; }
        public int NumeroLinha { get; set; }
    }
}
