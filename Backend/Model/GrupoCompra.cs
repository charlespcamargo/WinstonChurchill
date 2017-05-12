using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace WinstonChurchill.Backend.Model
{
    [Table("GrupoCompra")]
    public class GrupoCompra
    {
        [DataMember]
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [DataMember]
        [Column("Nome"), StringLength(150)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }


        #region Foreign keys

        [ForeignKey("GrupoCompraID")]
        public List<GrupoCompraCategoria> GrupoCompraCategoria { get; set; } = new List<GrupoCompraCategoria>();

        [ForeignKey("GrupoCompraID")]
        public List<CompradorGrupoCompra> CompradorGrupoCompra { get; set; } = new List<CompradorGrupoCompra>();

        #endregion
    }
}
