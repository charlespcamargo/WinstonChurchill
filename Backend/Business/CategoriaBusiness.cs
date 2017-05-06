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
    public class CategoriaBusiness
    {
        Expression<Func<Categorias, bool>> predicate;

        public static CategoriaBusiness New
        {
            get
            {
                return new CategoriaBusiness();
            }
        }

        public Categorias Salvar(Categorias entidade, Usuario usuario)
        {
            if (entidade == null)
                throw new ArgumentException("Erro: nenhuma informação foi gerado");

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (entidade.ID == 0)
                {
                    entidade.UsuarioID = usuario.ID;
                    entidade.DataCadastro = DateTime.Now;
                    uow.CategoriaRepository.Inserir(entidade);
                }
                else
                {
                    Categorias objSalvo = uow.CategoriaRepository.Carregar(p => p.ID == entidade.ID, ord => ord.OrderBy(p => p.ID));
                    if (objSalvo != null)
                    {
                        objSalvo.Descricao = entidade.Descricao;
                        objSalvo.Ativo = entidade.Ativo;
                        objSalvo.Nome = entidade.Nome;
                        objSalvo.UsuarioID = usuario.ID;
                        objSalvo.Descricao = entidade.Descricao;
                        uow.CategoriaRepository.Alterar(entidade);
                    }
                }

                uow.Save();
                return entidade;
            }
        }

        public List<Categorias> Listar(Categorias filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                return uow.CategoriaRepository.Listar(predicate).ToList();
            }

        }

        public Categorias Carregar(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(new Categorias { ID = id });
                Categorias objeto = uow.CategoriaRepository.Carregar(predicate);
                return objeto;
            }
        }

        public void Excluir(int id)
        {
            if (id == 0)
                throw new ArgumentException("Informe o Categoria para realizar a exclusão");
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.CategoriaRepository.Excluir(id);
                uow.Save();
            }
        }

        private void MontarFiltro(Categorias filtro)
        {
            predicate = UtilEntity.True<Categorias>();

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
