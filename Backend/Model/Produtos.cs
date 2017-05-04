using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using WinstonChurchill.API.Common.Conversores;


namespace WinstonChurchill.Backend.Model
{
    [Table("Produtos")]
    public class Produtos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public decimal ID { get; set; }

        [Column("Nome"), StringLength(50)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }


        [Column("Descricao"), StringLength(255)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Descricao { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }

        [Column("DataCadastro")]
        [JsonConverter(typeof(CustomDateTime))]
        public DateTime DataCadastro { get; set; }


        #region Foreign Keys

        [Column("UsuarioID")]
        public decimal UsuarioID { get; set; }

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }


        [ForeignKey("ProdutoID")]
        public List<ProdutoImagens> Imagens { get; set; }


        [ForeignKey("ProdutoID")]
        public List<CaracteristicasProduto> Caracteristicas { get; set; }

        #endregion
    }
}
