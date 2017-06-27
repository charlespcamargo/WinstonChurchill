using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    public class ProdutosImagensBusiness
    {
        Expression<Func<ProdutoImagem, bool>> predicate;
        public static ProdutosImagensBusiness New
        {
            get
            {
                return new ProdutosImagensBusiness();
            }
        }

        public ProdutoImagem Salvar(byte[] data, HttpPostedFile arquivo, int idProduto)
        {
            ProdutoImagem produtoImagem = new ProdutoImagem();
            produtoImagem.ProdutoID = idProduto;
            produtoImagem.Imagem = new Imagem();
            produtoImagem.Imagem.DataCadastro = DateTime.Now;
            produtoImagem.Imagem.DiretorioFisico = "/produtos";
            produtoImagem.Imagem.NomeArquivo = arquivo.FileName;
            produtoImagem.Imagem.TamanhoBytes = arquivo.ContentLength;
            produtoImagem.Imagem.Tipo = arquivo.ContentType;

            AnexoBusiness.New.GravarArquivoFisico<Imagem>(produtoImagem.Imagem, data);
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Produto produto = uow.ProdutosRepository.Carregar(p => p.ID == idProduto, ord=>ord.OrderBy(p=>p.ID));
                    produtoImagem.Imagem.UsuarioID = produto.UsuarioID;
                    uow.ProdutoImagensRepository.Inserir(produtoImagem);
                    uow.Save();
                }
            }
            catch
            {
                AnexoBusiness.New.ExcluirArquivoFisico<Imagem>(produtoImagem.Imagem);
            }
            return produtoImagem;
        }

        public List<ProdutoImagem> Carregar(ProdutoImagem filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                List<ProdutoImagem> lista = uow.ProdutoImagensRepository.Listar(predicate, ord => ord.OrderBy(p => p.ID), "Imagem");
                return lista;
            }
        }

        public void Excluir(ProdutoImagem filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                ProdutoImagem objProdutoImagem = uow.ProdutoImagensRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID));
                if (objProdutoImagem == null)
                    throw new ArgumentException("Imagem não encontrada!");

                uow.ProdutoImagensRepository.Excluir(objProdutoImagem);
                Imagem objImagem = uow.ImagemRepository.Carregar(p => p.ID == objProdutoImagem.ImagemID, ord => ord.OrderBy(p => p.ID));
                uow.ImagemRepository.Excluir(objImagem);
                uow.Save();

                AnexoBusiness.New.ExcluirArquivoFisico<Imagem>(objImagem);
            }
        }

        private void MontarFiltro(ProdutoImagem filtro)
        {
            predicate = UtilEntity.True<ProdutoImagem>();

            if (filtro.ID != 0)
                predicate = predicate.And(p => p.ID == filtro.ID);

            if (filtro.ProdutoID > 0)
                predicate = predicate.And(p => p.ProdutoID == filtro.ProdutoID);

            predicate = predicate.And(p => p.Imagem.UsuarioID == filtro.Imagem.UsuarioID);
        }

    }
}
