using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace WinstonChurchill.Backend.Model
{
    [Table("Grupo")]
    public class Grupo
    {
        [DataMember]
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [DataMember]
        [Column("Nome"), StringLength(150)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }


        [Column("TipoGrupo")]
        public int TipoGrupo { get; set; }

        #region Foreign keys

        [Column("UsuarioID")]
        public int UsuarioID { get; set; }

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }

        [ForeignKey("GrupoID")]
        public List<GrupoCategoria> GrupoCategoria { get; set; } = new List<GrupoCategoria>();

        [ForeignKey("GrupoID")]
        public List<ParceiroNegocioGrupo> ParceiroNegocioGrupo { get; set; } = new List<ParceiroNegocioGrupo>();

        #endregion


        #region Métodos
        public void AdicionarDependentes()
        {
            if (this.GrupoCategoria != null && this.ID > 0)
            {
                foreach (var item in this.GrupoCategoria)
                {
                    item.GrupoID = ID;
                }
            }

            if (this.ParceiroNegocioGrupo != null && this.ID > 0)
            {
                foreach (var item in this.ParceiroNegocioGrupo)
                {
                    item.GrupoID = ID;
                }
            }
        }
        #endregion
    }
}
