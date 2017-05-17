using System.Collections.Generic;
using System.Linq;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    internal class ParceiroNegocioGrupoBusiness
    {
        public static ParceiroNegocioGrupoBusiness New
        {
            get
            {
                return new ParceiroNegocioGrupoBusiness();
            }
        }

        public void Salvar(List<ParceiroNegocioGrupo> lista, int grupoId, UnitOfWork uow)
        {
            if (lista == null || lista.Count == 0)
            {
                List<ParceiroNegocioGrupo> listaExcluir = uow.ParceiroNegocioGrupoRepository.Listar(p => p.GrupoID == grupoId);
                Excluir(uow, listaExcluir);
            }
            else
            {
                List<ParceiroNegocioGrupo> listaSalva = uow.ParceiroNegocioGrupoRepository.Listar(p => p.GrupoID == grupoId);
                List<ParceiroNegocioGrupo> listaExcluir = listaSalva.Where(w => !lista.Any(a => a.GrupoID == w.GrupoID && a.ParceiroID == w.ParceiroID)).ToList();
                Excluir(uow, listaExcluir);
                Inserir(uow, listaSalva, lista);
            }
        }

        private void Inserir(UnitOfWork uow, List<ParceiroNegocioGrupo> listaSalva, List<ParceiroNegocioGrupo> listaSalvar)
        {
            if (listaSalvar != null && listaSalvar.Count > 0)
            {
                foreach (var itemSalvar in listaSalvar)
                {
                    ParceiroNegocioGrupo itemSalvo = listaSalva.FirstOrDefault(f => f.ID == itemSalvar.ID);
                    if (itemSalvo == null)
                        uow.ParceiroNegocioGrupoRepository.Inserir(itemSalvar);
                }
            }
        }

        private static void Excluir(UnitOfWork uow, List<ParceiroNegocioGrupo> listaExcluir)
        {
            if (listaExcluir != null && listaExcluir.Count > 0)
            {
                foreach (var itemExcluir in listaExcluir)
                    uow.ParceiroNegocioGrupoRepository.Excluir(itemExcluir);
            }
        }
    }
}