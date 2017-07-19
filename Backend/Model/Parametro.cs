using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using WinstonChurchill.API.Common.Conversores;
using System.Runtime.Serialization;

namespace WinstonChurchill.Backend.Model
{
    [Table("Parametro")]
    public class Parametro
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("LimiteCancelCompra")]
        [Required(ErrorMessage = "O Limite de Cancelamento de Compra é obrigatório")]
        public int LimiteCancelCompra { get; set; }

        [Column("PercLucroEmpresa")]
        [Required(ErrorMessage = "O percentual de Lucratividade da Empresa é obrigatório")]
        [JsonConverter(typeof(CustomMoneyReal))]
        public decimal PercLucroEmpresa { get; set; }

        [Column("PercLucroRepComercial")]
        [JsonConverter(typeof(CustomMoneyReal))]
        [Required(ErrorMessage = "O percentual de Lucratividade do Representante comercial é obrigatório")]
        public decimal PercLucroRepComercial { get; set; }

        [Column("RodadasLeilao")]
        [Required(ErrorMessage = "Rodas do Leilão é obrigatório")]
        public int RodadasLeilao { get; set; }

        [Column("DiasCadaRodada")]
        [Required(ErrorMessage = "Dias entre cada rodada é obrigatório")]
        public int DiasCadaRodada { get; set; }

        [Column("MargemGarantiaPreco")]
        [JsonConverter(typeof(CustomMoneyReal))]
        [Required(ErrorMessage = "Margem de Garantia de Preço é obrigatório")]
        public decimal MargemGarantiaPreco { get; set; }

        [Column("SegundaMargemGarantiaPreco")]
        [JsonConverter(typeof(CustomMoneyReal))]
        [Required(ErrorMessage = "Segunda Margem de Garantia de Preço é obrigatório")]
        public decimal SegundaMargemGarantiaPreco { get; set; }
    }
}
