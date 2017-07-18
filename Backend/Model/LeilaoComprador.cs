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
    [Table("LeilaoComprador")]
    public class LeilaoComprador
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

        [Column("QtdDesejada")]
        public decimal QtdDesejada { get; set; }


        #region ForeignKeys

        [ForeignKey("LeilaoID")]
        public Leilao Leilao { get; set; }

        [ForeignKey("ParceiroNegocioID")]
        public ParceiroNegocio ParceiroNegocio { get; set; }

        #endregion
    }
}
