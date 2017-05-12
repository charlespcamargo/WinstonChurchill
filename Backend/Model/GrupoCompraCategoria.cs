using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using WinstonChurchill.API.Common.Atributos;

namespace WinstonChurchill.Backend.Model
{
    [Table("GrupoCompraCategoria")]
    public class GrupoCompraCategoria
    {
        [DataMember]
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        #region Foreign Keys

        [DataMember]
        [Column("CategoriaID")]
        public int CategoriaID { get; set; }

        [ForeignKey("CategoriaID")]
        public Categoria Categoria { get; set; }

        [DataMember]
        [Column("GrupoCompraID")]
        public int GrupoCompraID { get; set; }

        [ForeignKey("GrupoCompraID")]
        public GrupoCompra GrupoCompra { get; set; }

        #endregion
    }
}
