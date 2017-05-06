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
                    uow.ProdutosRepository.Inserir(entidade);
                }
                else
                {
                    Produtos objSalvo = uow.ProdutosRepository.Carregar(p => p.ID == entidade.ID, ord => ord.OrderBy(p => p.ID));
                    if (objSalvo != null)
                    {
                        objSalvo.Descricao = entidade.Descricao;
                        objSalvo.Ativo = entidade.Ativo;
                        objSalvo.Nome = entidade.Nome;
                        objSalvo.UsuarioID = usuario.ID;
                        objSalvo.Descricao = entidade.Descricao;
                        uow.ProdutosRepository.Alterar(entidade);
                        CaracteristicasProdutoBusiness.New.Salvar(objSalvo.Caracteristicas, objSalvo.ID, uow);
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
                return uow.ProdutosRepository.Listar(predicate).ToList();
            }

        }

        public Produtos Carregar(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(new Produtos { ID = id });
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

            predicate = predicate.And(p => p.UsuarioID == filtro.UsuarioID);
        }
    }
}
