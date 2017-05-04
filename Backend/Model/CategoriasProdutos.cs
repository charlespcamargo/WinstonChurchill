using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using WinstonChurchill.API.Common.Conversores;

namespace WinstonChurchill.Backend.Model
{
    [Table("CategoriasProdutos")]
    public class CategoriasProdutos
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }


        #region Foreign Keys

        [Column("CategoriaID")]
        public int CategoriaID { get; set; }

        [ForeignKey("CategoriaID")]
        public Categorias Categoria { get; set; }

        [Column("ProdutoID")]
        public int ProdutoID { get; set; }

        [ForeignKey("ProdutoID")]
        public Produtos Produto { get; set; }

        #endregion
    }
}
