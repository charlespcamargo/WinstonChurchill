using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using WinstonChurchill.API.Common.Conversores;

namespace WinstonChurchill.Backend.Model
{
    [Table("Categoria")]
   public class Categoria
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Nome"), StringLength(50)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Column("Descricao"), StringLength(255)]
        [Required(ErrorMessage = "Descricao é obrigatório")]
        public string Descricao { get; set; }

        [Column("Ativo")]
        public bool? Ativo { get; set; }

        [Column("DataCadastro")]
        [JsonConverter(typeof(CustomDateTime))]
        public DateTime DataCadastro { get; set; }


        #region Foreign Keys

        [Column("UsuarioID")]
        public int UsuarioID { get; set; }

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }


        [ForeignKey("CategoriaID")]
        public List<CategoriaImagem> Imagens { get; set; }


        [ForeignKey("CategoriaID")]
        public List<CategoriaProduto> CategoriasProdutos { get; set; }

        #endregion
    }
}
