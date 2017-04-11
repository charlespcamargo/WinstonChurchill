using WinstonChurchill.Backend.Models;
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

        public BootgridResponseData<Usuario> Listar(int current, int rowCount, string busca)
        {
            current = (current == 0 ? 1 : current);
            rowCount = (rowCount == 0 ? 10 : rowCount);

            MontarFiltro(busca);

            BootgridResponseData<Usuario> dataSource = new BootgridResponseData<Usuario>();

            using (UnitOfWork uow = new UnitOfWork())
            {
                dataSource.current = current;
                dataSource.rowCount = rowCount;
                dataSource.rows = uow.UsuarioRepository.Listar(predicate).Skip((current - 1) * rowCount).Take(rowCount).ToList();
                dataSource.total = uow.UsuarioRepository.Contar(predicate);
            }

            return dataSource;
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
