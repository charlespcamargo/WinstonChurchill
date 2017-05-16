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
    public class GrupoBusiness
    {
        Expression<Func<Grupo, bool>> predicate;

        public static GrupoBusiness New
        {
            get
            {
                return new GrupoBusiness();
            }
        }

        public Grupo Salvar(Grupo entidade, Usuario usuario)
        {
            if (entidade == null)
                throw new ArgumentException("Erro: nenhuma informação foi gerado");

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (entidade.ID == 0)
                {
                    entidade.UsuarioID = usuario.ID;
                    uow.GrupoRepository.Inserir(entidade);
                }
                else
                {
                    Grupo objSalvo = uow.GrupoRepository.Carregar(p => p.ID == entidade.ID, ord => ord.OrderBy(p => p.ID), "Endereco");
                    if (objSalvo != null)
                    {
                        objSalvo.Nome = entidade.Nome;
                        objSalvo.TipoGrupo = entidade.TipoGrupo;
                        uow.GrupoRepository.Alterar(objSalvo);
                    }
                    ParceiroNegocioGrupoBusiness.New.Salvar(entidade.ParceiroNegocioGrupo, objSalvo.ID, uow);
                    GrupoCategoriaBusiness.New.Salvar(entidade.GrupoCategoria, objSalvo.ID, uow);
                }

                uow.Save();
                return entidade;
            }
        }

        public List<Grupo> Listar(Grupo filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                return uow.GrupoRepository.Listar(predicate).ToList();
            }

        }

        public Grupo Carregar(Grupo filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                Grupo objeto = uow.GrupoRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID));
                return objeto;
            }
        }

        public void Excluir(Grupo filtro)
        {
            if (filtro == null)
                throw new ArgumentException("Informe o Grupo para realizar a exclusão");

            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                Grupo GrupoExcluir = uow.GrupoRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID), "Endereco");
                uow.GrupoRepository.Excluir(GrupoExcluir);
                uow.Save();
            }
        }

        private void MontarFiltro(Grupo filtro)
        {
            predicate = UtilEntity.True<Grupo>();

            if (!string.IsNullOrEmpty(filtro.Nome))
                predicate = predicate.And(p => p.Nome.Contains(filtro.Nome));

            if (filtro.TipoGrupo > 0)
                predicate = predicate.And(p => p.TipoGrupo == filtro.TipoGrupo);

            if (filtro.ID > 0)
                predicate = predicate.And(p => p.ID == filtro.ID);

            predicate = predicate.And(p => p.UsuarioID == filtro.UsuarioID);
        }
    }
}
