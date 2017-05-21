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

        public void Salvar(List<ParceiroNegocioGrupo> lista, int grupoId, int parceiroId, UnitOfWork uow)
        {
            List<ParceiroNegocioGrupo> listaSalva = uow.ParceiroNegocioGrupoRepository.Listar(p => (grupoId > 0 && p.GrupoID == grupoId)
                                                                                                      || (parceiroId > 0 && p.ParceiroID == parceiroId));
            if (lista == null || lista.Count == 0)
                Excluir(uow, listaSalva);
            else
            {
                List<ParceiroNegocioGrupo> listaExcluir = listaSalva.Where(w => !lista.Any(a => a.GrupoID == w.GrupoID && a.ParceiroID == w.ParceiroID)).ToList();
                Excluir(uow, listaExcluir);
                Inserir(uow, listaSalva, lista, grupoId, parceiroId);
            }
        }

        private void Inserir(UnitOfWork uow, List<ParceiroNegocioGrupo> listaSalva, List<ParceiroNegocioGrupo> listaSalvar, int grupoId, int parceiroId)
        {
            if (listaSalvar != null && listaSalvar.Count > 0)
            {
                foreach (var itemSalvar in listaSalvar)
                {
                    ParceiroNegocioGrupo itemSalvo = listaSalva.FirstOrDefault(f => f.ID == itemSalvar.ID);
                    if (itemSalvo == null)
                    {
                        if (grupoId > 0)
                            itemSalvar.GrupoID = grupoId;

                        if(parceiroId > 0)
                        itemSalvar.ParceiroID = parceiroId;

                        uow.ParceiroNegocioGrupoRepository.Inserir(itemSalvar);
                    }
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