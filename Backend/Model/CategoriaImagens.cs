﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WinstonChurchill.Backend.Model
{
    [Table("CategoriaImagens")]
    public class CategoriaImagens
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }


        #region ForeignKeys

        [Column("CategoriaID")]
        public int CategoriaID { get; set; }

        [ForeignKey("CategoriaID")]
        public Categorias Categoria { get; set; }


        [Column("ImagemID")]
        public int ImagemID { get; set; }

        [ForeignKey("ImagemID")]
        public Imagem Imagem { get; set; }

        #endregion
    }
}
