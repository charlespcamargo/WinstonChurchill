using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WinstonChurchill.Backend.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Nome"), StringLength(50)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Column("Descricao"), StringLength(500)]
        public string Descricao { get; set; }

    }
}