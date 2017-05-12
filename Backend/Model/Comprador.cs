﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using WinstonChurchill.API.Common.Atributos;
using System.Collections.Generic;

namespace WinstonChurchill.Backend.Model
{
    [Table("Comprador")]
    public class Comprador
    {
        [DataMember]
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [DataMember]
        [Column("CNPJ"), StringLength(150)]
        [Required(ErrorMessage = "CNPJ é obrigatório")]
        public string CNPJ { get; set; }

        [DataMember]
        [Column("RazaoSocial"), StringLength(50)]
        [Required(ErrorMessage = "Razao Social é obrigatório")]
        public string RazaoSocial { get; set; }

        [DataMember]
        [Column("NomeFantasia"), StringLength(50)]
        [Required(ErrorMessage = "Nome Fantasia é obrigatório")]
        public string NomeFantasia { get; set; }

        [DataMember]
        [Column("Telefone"), StringLength(50)]
        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Telefone { get; set; }

        [DataMember]
        [Column("Celular"), StringLength(11)]
        [Required(ErrorMessage = "Celular é obrigatório")]
        public string Celular { get; set; }

        [DataMember]
        [Column("Email"), StringLength(150)]
        [JsonConverter(typeof(EmailAttribute))]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }


        #region Foreign Keys

        [DataMember]
        [Column("EnderecoID")]
        public int EnderecoID { get; set; }

        [ForeignKey("EnderecoID")]
        public Endereco Endereco { get; set; }

        [DataMember]
        [Column("UsuarioID")]
        public int UsuarioID { get; set; }

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }

        [ForeignKey("CompradorID")]
        public List<CompradorGrupoCompra> CompradorGrupoCompra { get; set; } = new List<CompradorGrupoCompra>();

        [ForeignKey("CompradorID")]
        public List<CompradorProduto> CompradorProduto { get; set; } = new List<Model.CompradorProduto>();

        [ForeignKey("CompradorID")]
        public List<Contato> Contato { get; set; } = new List<Contato>();


        #endregion

    }
}
