using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using WinstonChurchill.API.Common.Conversores;
using WinstonChurchill.Backend.Model.Enumeradores;

namespace WinstonChurchill.Backend.Model
{
    [Table("ParceiroNegocioUsuario")]
    public class ParceiroNegocioUsuario
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("ParceiroNegocioID")]
        public int ParceiroNegocioID { get; set; }

        [Column("UsuarioID")]
        public int UsuarioID { get; set; }

        [Column("CriadorID")]
        public int CriadorID { get; set; }

        [Column("DataCadastro")]
        [JsonConverter(typeof(CustomDate))]
        public DateTime DataCadastro { get; set; }


        #region Foreign Keys

        [ForeignKey("ParceiroNegocioID")]
        public ParceiroNegocio ParceiroNegocio { get; set; }

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }

        [ForeignKey("CriadorID")]
        public Usuario Criador { get; set; }

        #endregion

    }
}
