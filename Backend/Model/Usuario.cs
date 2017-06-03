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
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Nome"), StringLength(50)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Column("Senha")]
        public string Senha { get; set; }

        [Column("Email"), StringLength(50)]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }

        [Column("DataCadastro")]
        [JsonConverter(typeof(CustomDate))]
        public DateTime DataCadastro { get; set; }

        #region Foreign Keys

        [ForeignKey("UsuarioID")]
        public List<UsuarioXGrupoUsuario> Grupos { get; set; }

        #endregion


       

        [NotMapped]
        public string SenhaNova { get; set; }

        [NotMapped]
        public string SenhaNovaConfirmar { get; set; }

    }
}