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
        Expression<Func<ProdutosImagens, bool>> predicate;
        public static ProdutosImagensBusiness New
        {
            get
            {
                return new ProdutosImagensBusiness();
            }
        }

        public ProdutosImagens Salvar(byte[] data, HttpPostedFile arquivo, Usuario usuario, int idProduto)
        {
            ProdutosImagens produtoImagem = new ProdutosImagens();
            produtoImagem.ProdutoID = idProduto;
            produtoImagem.Imagem = new Imagem();
            produtoImagem.Imagem.DataCadastro = DateTime.Now;
            produtoImagem.Imagem.DiretorioFisico = "/produtos";
            produtoImagem.Imagem.NomeArquivo = arquivo.FileName;
            produtoImagem.Imagem.TamanhoBytes = arquivo.ContentLength;
            produtoImagem.Imagem.Tipo = arquivo.ContentType;
            produtoImagem.Imagem.UsuarioID = usuario.ID;

            AnexoBusiness.New.GravarArquivoFisico<Imagem>(produtoImagem.Imagem, data);
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
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

        public List<ProdutosImagens> Carregar(ProdutosImagens filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                List<ProdutosImagens> lista = uow.ProdutoImagensRepository.Listar(predicate, ord => ord.OrderBy(p => p.ID), "Imagem");
                return lista;
            }
        }

        public void Excluir(ProdutosImagens filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                ProdutosImagens objProdutoImagem = uow.ProdutoImagensRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID));
                if (objProdutoImagem == null)
                    throw new ArgumentException("Imagem não encontrada!");

                uow.ProdutoImagensRepository.Excluir(objProdutoImagem);
                Imagem objImagem = uow.ImagemRepository.Carregar(p => p.ID == objProdutoImagem.ImagemID, ord=>ord.OrderBy(p=>p.ID));
                uow.ImagemRepository.Excluir(objImagem);
                uow.Save();

                AnexoBusiness.New.ExcluirArquivoFisico<Imagem>(objImagem);
            }
        }

        private void MontarFiltro(ProdutosImagens filtro)
        {
            predicate = UtilEntity.True<ProdutosImagens>();

            if (filtro.ID != 0)
                predicate = predicate.And(p => p.ID == filtro.ID);

            if (filtro.ProdutoID > 0)
                predicate = predicate.And(p => p.ProdutoID == filtro.ProdutoID);

            predicate = predicate.And(p => p.Imagem.UsuarioID == filtro.Imagem.UsuarioID);
        }

    }
}
