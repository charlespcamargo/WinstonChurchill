using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinstonChurchill.Backend.Model
{
    [Table("UsuarioXGrupoUsuario")]
    public class UsuarioXGrupoUsuario
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        
        [Column("UsuarioID")]
        public int UsuarioID { get; set; }

        [Column("GrupoUsuarioID")]
        public int GrupoUsuarioID { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }

        [Column("DataCadastro")]
        public DateTime DataCadastro { get; set; }

        [Column("ResponsavelID")]
        public int ResponsavelID { get; set; }


        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }

        [ForeignKey("GrupoUsuarioID")]
        public GrupoUsuario GrupoUsuario { get; set; }

        [ForeignKey("ResponsavelID")]
        public Usuario Responsavel { get; set; }

    }
}
