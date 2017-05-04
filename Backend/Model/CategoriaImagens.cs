using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WinstonChurchill.Backend.Model
{
    [Table("CategoriaImagens")]
    public class CategoriaImagens
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public decimal ID { get; set; }


        #region ForeignKeys

        [Column("CategoriaID")]
        public decimal CategoriaID { get; set; }

        [ForeignKey("CategoriaID")]
        public CategoriaProdutos Categoria { get; set; }


        [Column("ImagemID")]
        public decimal ImagemID { get; set; }

        [ForeignKey("ImagemID")]
        public Imagem Imagem { get; set; }

        #endregion
    }
}
