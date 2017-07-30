using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;

namespace WinstonChurchill.Backend.Model
{
    [Table("ParceiroNegocio")]
    public class ParceiroNegocio
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("CNPJ"), StringLength(17)]
        [Required(ErrorMessage = "CNPJ é obrigatório")]
        public string CNPJ { get; set; }

        [Column("RazaoSocial"), StringLength(100)]
        [Required(ErrorMessage = "Razao Social é obrigatório")]
        public string RazaoSocial { get; set; }

        [Column("NomeFantasia"), StringLength(100)]
        [Required(ErrorMessage = "Nome Fantasia é obrigatório")]
        public string NomeFantasia { get; set; }

        [Column("Telefone"), StringLength(50)]
        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Telefone { get; set; }

        [Column("Celular"), StringLength(15)]
        [Required(ErrorMessage = "Celular é obrigatório")]
        public string Celular { get; set; }

        [Column("Email"), StringLength(150)]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }


        [Column("TipoParceiro")]
        public int TipoParceiro { get; set; }

        [Column("DataCadastro")]
        public DateTime DataCadastro { get; set; }

        #region Foreign Keys

        [Column("EnderecoID")]
        public int EnderecoID { get; set; }

        [ForeignKey("EnderecoID")]
        public Endereco Endereco { get; set; }

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

        [ForeignKey("ParceiroID")]
        public List<ParceiroNegocioGrupo> Grupos { get; set; } = new List<ParceiroNegocioGrupo>();

        [ForeignKey("ParceiroNegocioID")]
        public List<ParceiroNegocioUsuario> Usuarios { get; set; } = new List<ParceiroNegocioUsuario>();

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

        internal void ValidaTipoParceiro()
        {
            if (this.TipoParceiro == 1)
                this.FornecedorProduto = null;

            else if (this.TipoParceiro == 2)
                this.CompradorProduto = null;
        }

        #endregion
    }
}
