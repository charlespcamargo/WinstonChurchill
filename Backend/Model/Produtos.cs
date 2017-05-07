using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using WinstonChurchill.API.Common.Conversores;
using System.Runtime.Serialization;

namespace WinstonChurchill.Backend.Model
{
    [DataContract]
    [Table("Produtos")]
    public class Produtos
    {
        [DataMember]
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [DataMember]
        [Column("Nome"), StringLength(50)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }


        [DataMember]
        [Column("Descricao"), StringLength(255)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Descricao { get; set; }

        [Column("Ativo")]
        [DataMember]
        public bool? Ativo { get; set; }

        [DataMember]
        [Column("DataCadastro")]
        [JsonConverter(typeof(CustomDate))]
        public DateTime DataCadastro { get; set; }


        #region Foreign Keys


        [DataMember]
        [Column("UsuarioID")]
        public int UsuarioID { get; set; }


        public void AdicionarProdutosFilhos()
        {
            if (this.CategoriasProdutos != null && this.ID > 0)
            {
                foreach (var item in this.CategoriasProdutos)
                {
                    item.ProdutoID = ID;
                }
            }
        }

        [DataMember]
        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }


        [DataMember]
        [ForeignKey("ProdutoID")]
        public List<ProdutosImagens> ProdutosImagens { get; set; }


        [DataMember]
        [ForeignKey("ProdutoID")]
        public List<CaracteristicasProduto> Caracteristicas { get; set; }

        [DataMember]
        [ForeignKey("ProdutoID")]
        public List<CategoriasProdutos> CategoriasProdutos { get; set; }

        #endregion
    }
}
