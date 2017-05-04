using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinstonChurchill.Backend.Model
{
    [Table("ProdutoImagens")]
    public class ProdutoImagens
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public decimal ID { get; set; }


        #region ForeignKeys

        [Column("ProdutoID")]
        public decimal ProdutoID { get; set; }

        [ForeignKey("ProdutoID")]
        public Produtos Produtos { get; set; }


        [Column("ImagemID")]
        public decimal ImagemID { get; set; }

        [ForeignKey("ImagemID")]
        public Imagem Imagem { get; set; }

        #endregion
    }
}
