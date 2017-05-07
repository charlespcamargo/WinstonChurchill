using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;
using WinstonChurchill.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.Backend.Business
{
    public class UsuarioBusiness
    {
        private Expression<Func<Usuario, bool>> predicate;

        public static UsuarioBusiness New
        {
            get
            {
                return new UsuarioBusiness();
            }
        }

        public int Contar(Usuario entidade)
        {
            MontarFiltro(entidade);

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.UsuarioRepository.Contar(predicate);
            }
        }

        public List<Usuario> Listar(Usuario filtro)
        {
            MontarFiltro(filtro);

            List<Usuario> data = new List<Usuario>();

            using (UnitOfWork UoW = new UnitOfWork())
            {
                data = UoW.UsuarioRepository.Listar(predicate); 
            }

            return data;
        }

        public Usuario Carregar(int id) {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Usuario usuario = uow.UsuarioRepository.Carregar(p => p.ID == id, ord=>ord.OrderBy(p=>p.ID));
                if (usuario == null)
                    throw new ArgumentException("Usuário não encontrado!");

                return usuario;
            }
        }

        public void Excluir(int id)
        {
            Usuario talento = new Usuario();

            using (UnitOfWork uow = new UnitOfWork())
            {
                talento = uow.UsuarioRepository.Carregar(c => c.ID == id, o => o.OrderBy(by => by.ID));
                
                uow.UsuarioRepository.Excluir(talento.ID);

                uow.Save();
            }
        }

        private void MontarFiltro(string busca)
        {
            predicate = UtilEntity.True<Usuario>();

            if (!string.IsNullOrEmpty(busca))
                predicate = predicate.And(a => a.Nome.Contains(busca));
            else
                predicate = predicate.And(a => a.ID >= 0);
        }

        private void MontarFiltro(Usuario entidade)
        {
            predicate = UtilEntity.True<Usuario>();

            if (!string.IsNullOrEmpty(entidade.Nome))
                predicate = predicate.And(o => o.Nome.Contains(entidade.Nome)); 
        }

    }
}
