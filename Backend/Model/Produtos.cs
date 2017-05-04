using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using WinstonChurchill.API.Common.Conversores;


namespace WinstonChurchill.Backend.Model
{
    [Table("Produtos")]
    public class Produtos
    {
        [Key]//, DatabaseGenerated(DatabaseGeneratedOption.Identity)]  -- Não usa isso pq da pau no insert do MYSQL
        [Column("ID")]
        public int ID { get; set; }

        [Column("Nome"), StringLength(50)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }


        [Column("Descricao"), StringLength(255)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Descricao { get; set; }

        [Column("Ativo")]
        public bool? Ativo { get; set; }

        [Column("DataCadastro")]
        [JsonConverter(typeof(CustomDateTime))]
        public DateTime DataCadastro { get; set; }


        #region Foreign Keys

        private int _UsuarioID;

        [Column("UsuarioID")]
        public int UsuarioID
        {
            get
            {
                return _UsuarioID;
            }


            set {
                this._UsuarioID = value;
                AdicionarUsuarioFilhos();
            }
        }

        private void AdicionarUsuarioFilhos()
        {
            if (this.Imagens != null) {
                foreach (var item in this.Imagens)
                {
                    if (item.Imagem != null)
                        item.Imagem.UsuarioID = this.UsuarioID;
                }
            }
        }

        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }


        [ForeignKey("ProdutoID")]
        public List<ProdutosImagens> Imagens { get; set; }


        [ForeignKey("ProdutoID")]
        public List<CaracteristicasProduto> Caracteristicas { get; set; }

        [ForeignKey("ProdutoID")]
        public List<CategoriasProdutos> CategoriasProdutos { get; set; }

        #endregion
    }
}
