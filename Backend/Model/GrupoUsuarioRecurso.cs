using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinstonChurchill.Backend.Model
{
    [Table("GrupoUsuarioRecurso")]
    public class GrupoUsuarioRecurso
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Recurso")]
        public string Recurso { get; set; }

        #region Foreign Keys


        [Column("GrupoID")]
        public int GrupoID { get; set; }

        [ForeignKey("GrupoID")]
        public GrupoUsuario GrupoUsuario { get; set; }

        #endregion
    }
}
