using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using WinstonChurchill.API.Common.Atributos;
using System.Collections.Generic;

namespace WinstonChurchill.Backend.Model
{
    [Table("FornecedorProduto")]
    public class FornecedorProduto
    {
        [DataMember]
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [DataMember]
        [Column("Valor")]
        public decimal Valor { get; set; }

        [DataMember]
        [Column("Volume")]
        public int Volume { get; set; }

        [DataMember]
        [Column("CapacidadeMaxima")]
        public int CapacidadeMaxima { get; set; }


        #region Foreign Keys

        [DataMember]
        [Column("ParceiroID")]
        public int ParceiroID { get; set; }

        [ForeignKey("ParceiroID")]
        public ParceiroNegocio Parceiro { get; set; }

        [DataMember]
        [Column("ProdutoID")]
        public int ProdutoID { get; set; }

        [ForeignKey("ProdutoID")]
        public Produto Produto { get; set; }

        #endregion
    }
}
