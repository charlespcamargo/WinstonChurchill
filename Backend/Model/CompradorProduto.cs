using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WinstonChurchill.Backend.Model
{
    [Table("CompradorProduto")]
    public class CompradorProduto
    {
        private int _id;

        [DataMember]
        [Key]
        [Column("ID")]
        public int ID
        {
            get
            {
                if (_id < 0) return 0;
                return _id;
            }
            set
            { _id = value; }
        }

        [DataMember]
        [Column("ValorMedioCompra")]
        public decimal ValorMedioCompra { get; set; }

        [DataMember]
        [Column("Quantidade")]
        public int Quantidade { get; set; }

        [DataMember]
        [Column("Frequencia")]
        public int Frequencia { get; set; }



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
