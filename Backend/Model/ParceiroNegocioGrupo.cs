using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using WinstonChurchill.API.Common.Atributos;
namespace WinstonChurchill.Backend.Model
{
    [Table("ParceiroNegocioGrupo")]
    public class ParceiroNegocioGrupo
    {
        [DataMember]
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        #region Foreign Keys

        [DataMember]
        [Column("ParceiroID")]
        public int ParceiroID { get; set; }

        [ForeignKey("ParceiroID")]
        public ParceiroNegocio Parceiro { get; set; }

        [DataMember]
        [Column("GrupoID")]
        public int GrupoID { get; set; }

        [ForeignKey("GrupoID")]
        public Grupo Grupo { get; set; }

        #endregion
    }
}
