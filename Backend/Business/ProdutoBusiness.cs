using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

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

        public void Salvar(Produtos entidade, Usuario usuario)
        {
            if (entidade == null)
                throw new ArgumentException("Erro: nenhuma informação foi gerado");

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (entidade.ID == 0)
                {
                    entidade.UsuarioID = usuario.ID;
                    entidade.DataCadastro = DateTime.Now;
                    uow.ProdutosRepository.Inserir(entidade);
                }
                else
                {
                    Produtos objSalvo = uow.ProdutosRepository.Carregar(p => p.ID == entidade.ID);
                    if (objSalvo != null)
                    {
                        objSalvo.Descricao = entidade.Descricao;
                        objSalvo.Ativo = entidade.Ativo;
                        objSalvo.Nome = entidade.Nome;
                        objSalvo.UsuarioID = usuario.ID;
                        objSalvo.Descricao = entidade.Descricao;
                    }
                }

                uow.Save();
            }
        }

        public List<Produtos> Listar(Produtos filtro)
        {
            if (filtro == null)
                throw new ArgumentException("Erro: ao tentar Listar os produtos");

            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                List<Produtos> lista = uow.ProdutosRepository.Listar(predicate);
                return lista;
            }
        }

        public Produtos Carregar(Produtos filtro)
        {
            if (filtro == null)
                throw new ArgumentException("Erro: ao tentar Listar os produtos");

            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                Produtos objeto = uow.ProdutosRepository.Carregar(predicate);
                return objeto;
            }
        }

        public void Excluir(int id)
        {
            if (id == 0)
                throw new ArgumentException("Informe o produto para realizar a exclusão");
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.ProdutosRepository.Excluir(id);
                uow.Save();
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
        }
    }
}
