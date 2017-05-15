using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using WinstonChurchill.API.Common.Atributos;

namespace WinstonChurchill.Backend.Model
{
    [Table("Contato")]
    public class Contato
    {
        [DataMember]
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [DataMember]
        [Column("Nome"), StringLength(100)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [DataMember]
        [Column("Email"), StringLength(150)]
        [JsonConverter(typeof(EmailAttribute))]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }

        [DataMember]
        [Column("Telefone"), StringLength(14)]
        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Telefone { get; set; }


        #region Foreign Keys

        [DataMember]
        [Column("ParceiroID")]
        public int ParceiroID { get; set; }

        [ForeignKey("ParceiroID")]
        public ParceiroNegocio Parceiro { get; set; }

        #endregion
    }
}
