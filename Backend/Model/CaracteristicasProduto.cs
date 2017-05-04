using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using WinstonChurchill.API.Common.Conversores;

namespace WinstonChurchill.Backend.Model
{
    [Table("CaracteristicasProduto")]
    public class CaracteristicasProduto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public decimal ID { get; set; }

        [Column("Nome"), StringLength(50)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }


        #region ForeignKeys

        [Column("ProdutoID")]
        public decimal ProdutoID { get; set; }

        [ForeignKey("ProdutoID")]
        public Produtos Produtos { get; set; }

        #endregion

    }
}
