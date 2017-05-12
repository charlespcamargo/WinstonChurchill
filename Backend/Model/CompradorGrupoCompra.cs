using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using WinstonChurchill.API.Common.Atributos;
namespace WinstonChurchill.Backend.Model
{
    [Table("CompradorGrupoCompra")]
    public class CompradorGrupoCompra
    {
        [DataMember]
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        #region Foreign Keys

        [DataMember]
        [Column("CompradorID")]
        public int CompradorID { get; set; }

        [ForeignKey("CompradorID")]
        public Comprador Comprador { get; set; }

        [DataMember]
        [Column("GrupoCompraID")]
        public int GrupoCompraID { get; set; }

        [ForeignKey("GrupoCompraID")]
        public GrupoCompra GrupoCompra { get; set; }

        #endregion
    }
}
