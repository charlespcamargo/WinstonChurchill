using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System;

namespace WinstonChurchill.Backend.Model
{
    [Table("Endereco")]
    public class Endereco
    {
        [DataMember]
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [DataMember]
        [Column("Logradouro"), StringLength(150)]
        [Required(ErrorMessage = "Logradouro é obrigatório")]
        public string Logradouro { get; set; }

        [DataMember]
        [Column("Bairro"), StringLength(50)]
        [Required(ErrorMessage = "Bairro é obrigatório")]
        public string Bairro { get; set; }

        [DataMember]
        [Column("Cidade"), StringLength(50)]
        [Required(ErrorMessage = "Cidade é obrigatório")]
        public string Cidade { get; set; }

        [DataMember]
        [Column("Estado"), StringLength(50)]
        [Required(ErrorMessage = "Estado é obrigatório")]
        public string Estado { get; set; }

        [DataMember]
        [Column("CEP"), StringLength(11)]
        [Required(ErrorMessage = "CEP é obrigatório")]
        public string CEP { get; set; }



        #region Métodos Internos
        internal void AlterarObjeto(Endereco endereco)
        {
            if (endereco != null) {
                this.Bairro = endereco.Bairro;
                this.CEP = endereco.CEP;
                this.Cidade = endereco.Cidade;
                this.Estado = endereco.Estado;
                this.Logradouro = endereco.Logradouro;
            }
        }

        #endregion
    }
}
