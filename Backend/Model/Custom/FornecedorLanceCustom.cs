using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.Backend.Model
{
    public class FornecedorLanceCustom
    {
        public ParceiroNegocio ParceiroNegocio { get; set; }

        public int RodadaNumero { get; set; }

        public LeilaoFornecedorRodada FornecedoresRodada { get; set; }

        public bool RodadaEncerrada { get; set; }
    }
}
