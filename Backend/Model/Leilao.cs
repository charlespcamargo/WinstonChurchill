using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using WinstonChurchill.API.Common.Conversores;

namespace WinstonChurchill.Backend.Model
{
    [Table("Leilao")]
    public class Leilao
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Nome"), StringLength(50)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Column("ProdutoID")]
        public int ProdutoID { get; set; }

        [Column("DataFinalFormacao")]
        [JsonConverter(typeof(CustomDate))]
        public DateTime DataFinalFormacao { get; set; }

        [Column("QtdDesejada")]
        public decimal QtdDesejada { get; set; }

        [Column("RodadasLeilao")] 
        public int RodadasLeilao { get; set; }

        [Column("DiasCadaRodada")] 
        public int DiasCadaRodada { get; set; }


        [Column("DataAbertura")]
        [JsonConverter(typeof(CustomDate))]
        public DateTime DataAbertura { get; set; }

        [Column("CriadorID")]
        public int CriadorID { get; set; }

        [Column("RepresentanteID")]
        public int RepresentanteID { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }

        [NotMapped]
        public int DuracaoRodadasDias { get; set; }


        #region ForeignKeys

        [ForeignKey("ProdutoID")]
        public Produto Produto { get; set; }

        [ForeignKey("CriadorID")]
        public Usuario Criador { get; set; }

        [ForeignKey("RepresentanteID")]
        public Usuario Representante { get; set; }

        [ForeignKey("LeilaoID")]
        public List<LeilaoComprador> Compradores { get; set; }

        [ForeignKey("LeilaoID")]
        public List<LeilaoFornecedor> Fornecedores { get; set; }

        public bool temParticipantes
        {
            get
            {
                if (Compradores != null && Fornecedores != null)
                    return (Compradores.Count(c => c.Participando) > 0 || Fornecedores.Count(c => c.Participando) > 0);

                return false;
            }
        }

        
        #endregion

    }
}
