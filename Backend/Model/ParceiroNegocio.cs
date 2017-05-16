using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using WinstonChurchill.API.Common.Atributos;
using System.Collections.Generic;
using System;
using WinstonChurchill.Backend.Model.Enumeradores;

namespace WinstonChurchill.Backend.Model
{
    [Table("ParceiroNegocio")]
    public class ParceiroNegocio
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


        [Column("TipoParceiro")]
        public int TipoParceiro { get; set; }

        [Column("DataCadastro")]
        public DateTime DataCadastro { get; internal set; }

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

        [ForeignKey("ParceiroID")]
        public List<FornecedorProduto> FornecedorProduto { get; set; } = new List<FornecedorProduto>();

        [ForeignKey("ParceiroID")]
        public List<CompradorProduto> CompradorProduto { get; set; } = new List<CompradorProduto>();

        [ForeignKey("ParceiroID")]
        public List<Contato> Contatos { get; set; } = new List<Contato>();

        #endregion


        #region Métodos internos

        internal void AlterarObjeto(ParceiroNegocio entidade)
        {
            if (entidade != null)
            {
                this.Celular = entidade.Celular;
                this.CNPJ = entidade.CNPJ;
                this.Email = entidade.Email;
                this.NomeFantasia = entidade.NomeFantasia;
                this.RazaoSocial = entidade.RazaoSocial;
                this.Telefone = entidade.Telefone;
                this.TipoParceiro = entidade.TipoParceiro;
                this.UsuarioID = entidade.UsuarioID;
            }

            if (this.Endereco != null)
                this.Endereco.AlterarObjeto(entidade.Endereco);
        }

        #endregion
    }
}
