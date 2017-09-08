using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using WinstonChurchill.API.Common.Conversores;

namespace WinstonChurchill.Backend.Model
{
    [Table("leilaofornecedorrodada")]
    public class LeilaoFornecedorRodada
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        
        [Column("LeilaoFornecedorID")]
        public int LeilaoFornecedorID { get; set; }

        [Column("LeilaoRodadaID")]
        public int LeilaoRodadaID { get; set; }

        [Column("ValorPrimeiraMargem")]
        [JsonConverter(typeof(CustomMoneyReal))]
        public decimal ValorPrimeiraMargem { get; set; }

        [Column("ValorSegundaMargem")]
        [JsonConverter(typeof(CustomMoneyReal))]
        public decimal ValorSegundaMargem { get; set; }
        
        [Column("DataLance")]
        [JsonConverter(typeof(CustomDate))]
        public DateTime DataLance { get; set; }

        #region ForeignKeys

        [ForeignKey("LeilaoFornecedorID")]
        public LeilaoFornecedor LeilaoFornecedor { get; set; }

        [ForeignKey("LeilaoRodadaID")]
        public LeilaoRodada LeilaoRodada { get; set; }

        #endregion

    }
}
