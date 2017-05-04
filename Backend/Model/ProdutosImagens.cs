using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinstonChurchill.Backend.Model
{
    [Table("ProdutosImagens")]
    public class ProdutosImagens
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }


        #region ForeignKeys

        [Column("ProdutoID")]
        public int ProdutoID { get; set; }

        [ForeignKey("ProdutoID")]
        public Produtos Produtos { get; set; }


        [Column("ImagemID")]
        public int ImagemID { get; set; }

        [ForeignKey("ImagemID")]
        public Imagem Imagem { get; set; }

        #endregion
    }
}
