using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;
using WinstonChurchill.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinstonChurchill.API.Common.Util.Captcha;

namespace WinstonChurchill.Backend.Business
{
    public class LeilaoBusiness
    {
        private Expression<Func<Leilao, bool>> predicate;

        public static LeilaoBusiness New
        {
            get
            {
                return new LeilaoBusiness();
            }
        }

        public int Contar(Leilao entidade)
        {
            MontarFiltro(entidade);

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.LeilaoRepository.Contar(predicate);
            }
        }

        public List<Leilao> Listar(Leilao filtro)
        {
            MontarFiltro(filtro);

            List<Leilao> data = new List<Leilao>();

            using (UnitOfWork UoW = new UnitOfWork())
            {
                //filtro.Grupos = UoW.LeilaoRepository.Listar(p => p.UsuarioID == filtro.ID, null, "GrupoUsuario");

                //if (filtro.Grupos != null && filtro.Grupos.Any(a => a.GrupoUsuario.ID == 1001)) //Lista apenas os usuários de responsabilidade de cada admin
                //    predicate = predicate.And(p => p.Grupos.Any(w => w.ResponsavelID == filtro.ID));

                //else if (filtro.Grupos != null && !filtro.Grupos.Any(a => a.GrupoUsuario.ID == 1000))   //Se for usuário comum lista apenas informações dele
                //    predicate = predicate.And(p => p.ID == filtro.ID);

                data = UoW.LeilaoRepository.Listar(predicate, null, "Produto,Representante");
            }

            return data;
        }

        public Leilao Carregar(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Leilao leilao = uow.LeilaoRepository.Carregar(p => p.ID == id, ord => ord.OrderBy(p => p.ID));
                if (leilao == null)
                    throw new ArgumentException("Usuário não encontrado!");

                return leilao;
            }
        }

        public void Excluir(int id)
        {
            Leilao leilao = new Leilao();

            using (UnitOfWork uow = new UnitOfWork())
            {
                leilao = uow.LeilaoRepository.Carregar(c => c.ID == id, o => o.OrderBy(by => by.ID));

                if (leilao == null)
                    throw new ArgumentException("Usuário não encontrado.");

                
                uow.UsuarioRepository.Excluir(leilao.ID);

                uow.Save();
            }
        }

        private void MontarFiltro(string busca)
        {
            predicate = UtilEntity.True<Leilao>();

            if (!string.IsNullOrEmpty(busca))
                predicate = predicate.And(a => a.Nome.Contains(busca));
            else
                predicate = predicate.And(a => a.ID >= 0);
        }

        private void MontarFiltro(Leilao entidade)
        {
            predicate = UtilEntity.True<Leilao>();

            if (!string.IsNullOrEmpty(entidade.Nome))
                predicate = predicate.And(o => o.Nome == entidade.Nome);
            
        }
         
    }
}
