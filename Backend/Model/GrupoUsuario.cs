using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinstonChurchill.Backend.Model
{
    [Table("GrupoUsuario")]
    public class GrupoUsuario
    {
        [Key]
        [Column("ID")]
        public decimal ID { get; set; }

        [Column("Nome"), StringLength(50)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Column("Descricao"), StringLength(500)]
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string Descricao { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }

        [Column("DataCadastro")]
        public DateTime DataCadastro { get; set; }



    }
}
