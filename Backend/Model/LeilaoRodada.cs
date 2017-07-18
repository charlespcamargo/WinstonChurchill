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
    [Table("LeilaoRodada")]
    public class LeilaoRodada
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("LeilaoID")]
        public int LeilaoID { get; set; }

        [Column("Numero")]
        public int Numero { get; set; }

        [Column("DataEncerramento")]
        public DateTime DataEncerramento { get; set; }

        
        
        #region ForeignKeys

        [ForeignKey("LeilaoID")]
        public Leilao Leilao { get; set; }

        #endregion


    }
}
