﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using WinstonChurchill.API.Common.Atributos;

namespace WinstonChurchill.Backend.Model
{
    [Table("GrupoCategoria")]
    public class GrupoCategoria
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        #region Foreign Keys

        [Column("CategoriaID")]
        public int CategoriaID { get; set; }

        [ForeignKey("CategoriaID")]
        public Categoria Categoria { get; set; }

        [Column("GrupoID")]
        public int GrupoID { get; set; }

        [ForeignKey("GrupoID")]
        public Grupo Grupo { get; set; }

        #endregion
    }
}
