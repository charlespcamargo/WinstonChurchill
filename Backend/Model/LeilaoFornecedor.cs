using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WinstonChurchill.Backend.Model
{
    [Table("LeilaoFornecedor")]
    public class LeilaoFornecedor
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("LeilaoID")]
        public int LeilaoID { get; set; }

        [Column("ParceiroNegocioID")]
        public int ParceiroNegocioID { get; set; }

        [Column("Participando")]
        public bool Participando { get; set; }

        [Column("QtdMinima")]
        public decimal QtdMinima { get; set; }

        [Column("QtdMaxima")]
        public decimal QtdMaxima { get; set; }



        #region ForeignKeys

        [ForeignKey("LeilaoID")]
        public Leilao Leilao { get; set; }

        [ForeignKey("ParceiroNegocioID")]
        public ParceiroNegocio ParceiroNegocio { get; set; }

        #endregion
    }
}
