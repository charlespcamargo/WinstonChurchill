﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using WinstonChurchill.API.Common.Conversores;
using WinstonChurchill.Backend.Model.Interfaces;
using System.Configuration;

namespace WinstonChurchill.Backend.Model
{
    [Table("Imagem")]
    public class Imagem : IAnexo
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("NomeArquivo")]
        public string NomeArquivo { get; set; }

        [Column("DiretorioFisico")]
        public string DiretorioFisico { get; set; }

        [Column("TamanhoBytes")]
        public long TamanhoBytes { get; set; }


        [Column("DataCadastro")]
        [JsonConverter(typeof(CustomDateTime))]
        public DateTime DataCadastro { get; set; }


        [Column("Tipo")]
        public string Tipo { get; set; }


        #region Foreign Keys

        [Column("UsuarioID")]
        public int? UsuarioID { get; set; }

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }

        #endregion

        #region NotMapped
        [NotMapped]
        public string Url
        {
            get
            {
                return $"{ConfigurationManager.AppSettings["URL.SITE"]}Anexos/{this.DiretorioFisico}/{this.NomeArquivo}";
            }
            #endregion
        }
    }
}
