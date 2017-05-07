using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;
using WinstonChurchill.Backend.Utils;

namespace WinstonChurchill.Backend.Business
{
    public class ProdutoBusiness
    {
        Expression<Func<Produtos, bool>> predicate;

        public static ProdutoBusiness New
        {
            get
            {
                return new ProdutoBusiness();
            }
        }

        public Produtos Salvar(Produtos entidade, Usuario usuario)
        {
            if (entidade == null)
                throw new ArgumentException("Erro: nenhuma informação foi gerado");

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (entidade.ID == 0)
                {
                    entidade.UsuarioID = usuario.ID;
                    entidade.DataCadastro = DateTime.Now;
                    entidade.Ativo = true;
                    uow.ProdutosRepository.Inserir(entidade);
                }
                else
                {
                    Produtos objSalvo = uow.ProdutosRepository.Carregar(p => p.ID == entidade.ID, ord => ord.OrderBy(p => p.ID));
                    if (objSalvo != null)
                    {
                        entidade.AdicionarProdutosFilhos();
                        objSalvo.Descricao = entidade.Descricao;
                        objSalvo.Ativo = entidade.Ativo;
                        objSalvo.Nome = entidade.Nome;
                        objSalvo.UsuarioID = usuario.ID;
                        objSalvo.Descricao = entidade.Descricao;
                        uow.ProdutosRepository.Alterar(objSalvo);
                        CaracteristicasProdutoBusiness.New.Salvar(entidade.Caracteristicas, objSalvo.ID, uow);
                        CategoriasProdutoBusiness.New.Salvar(entidade.CategoriasProdutos, objSalvo.ID, uow);
                    }
                }

                uow.Save();
                return entidade;
            }
        }

        public List<Produtos> Listar(Produtos filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                return uow.ProdutosRepository.Listar(predicate, null, "CategoriasProdutos.Categoria").ToList();
            }

        }

        public Produtos Carregar(Produtos filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                Produtos objeto = uow.ProdutosRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID), "CategoriasProdutos.Categoria");
                return objeto;
            }
        }

        public void Excluir(Produtos filtro)
        {
            if (filtro == null)
                throw new ArgumentException("Informe o produto para realizar a exclusão");

            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                Produtos produtoExcluir = uow.ProdutosRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID), "CategoriasProdutos,ProdutosImagens,Caracteristicas");
                if (produtoExcluir == null)
                    throw new ArgumentException("Nenhum produto encontrado");

                List<Imagem> listaImagens = new List<Imagem>();
                if(produtoExcluir.ProdutosImagens != null)
                {
                    foreach (var item 
                        in produtoExcluir.ProdutosImagens)
                    {
                        Imagem imagem = uow.ImagemRepository.Carregar(p => p.ID == item.ImagemID, ord => ord.OrderBy(p => p.ID));
                        if(imagem != null)
                        {
                            listaImagens.Add(imagem);
                        }
                    }
                }

                uow.ProdutosRepository.Excluir(produtoExcluir);

                if (listaImagens != null)
                {
                    foreach (var imagem in listaImagens)
                    {
                        uow.ImagemRepository.Excluir(imagem);
                        AnexoBusiness.New.ExcluirArquivoFisico<Imagem>(imagem);
                    }
                }

                uow.Save();
            }
        }

        public List<CaracteristicasProduto> ListarCaracteristicas(int idProduto)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return uow.CaracteristicasProdutoRepository.Listar(p => p.ProdutoID == idProduto);
            }
        }

        private void MontarFiltro(Produtos filtro)
        {
            predicate = UtilEntity.True<Produtos>();

            if (!string.IsNullOrEmpty(filtro.Nome))
                predicate = predicate.And(p => p.Nome.Contains(filtro.Nome));

            if (filtro.Ativo.HasValue)
                predicate = predicate.And(p => p.Ativo == filtro.Ativo);

            if (filtro.ID > 0)
                predicate = predicate.And(p => p.ID == filtro.ID);

            predicate = predicate.And(p => p.UsuarioID == filtro.UsuarioID);
        }


    }
}
