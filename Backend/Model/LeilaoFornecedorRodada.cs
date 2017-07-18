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
    public class LeilaoFornecedorRodada
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        
        [Column("LeilaoFornecedorID")]
        public int LeilaoFornecedorID { get; set; }

        [Column("ValorPrimeiraMargem")]
        public decimal ValorPrimeiraMargem { get; set; }

        [Column("ValorSegundaMargem")]
        public decimal ValorSegundaMargem { get; set; }

        #region ForeignKeys
         
        [ForeignKey("LeilaoFornecedorID")]
        public LeilaoFornecedor LeilaoFornecedor { get; set; }

        #endregion

    }
}
