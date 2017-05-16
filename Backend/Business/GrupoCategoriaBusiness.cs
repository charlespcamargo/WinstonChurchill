using System.Collections.Generic;
using System.Linq;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    internal class GrupoCategoriaBusiness
    {
        public static GrupoCategoriaBusiness New
        {
            get
            {
                return new GrupoCategoriaBusiness();
            }
        }

        public void Salvar(List<GrupoCategoria> lista, int grupoId, UnitOfWork uow)
        {
            if (lista == null || lista.Count == 0)
            {
                List<GrupoCategoria> listaExcluir = uow.GrupoCategoriaRepository.Listar(p => p.GrupoID == grupoId);
                Excluir(uow, listaExcluir);
            }
            else
            {
                List<GrupoCategoria> listaSalva = uow.GrupoCategoriaRepository.Listar(p => p.GrupoID == grupoId);
                List<GrupoCategoria> listaExcluir = listaSalva.Where(w => !lista.Any(a => a.ID == w.ID)).ToList();
                Excluir(uow, listaExcluir);
                Inserir(uow, listaSalva, lista);
            }
        }

        private void Inserir(UnitOfWork uow, List<GrupoCategoria> listaSalva, List<GrupoCategoria> listaSalvar)
        {
            if (listaSalvar != null && listaSalvar.Count > 0)
            {
                foreach (var itemSalvar in listaSalvar)
                {
                    GrupoCategoria itemSalvo = listaSalva.FirstOrDefault(f => f.ID == itemSalvar.ID);
                    if (itemSalvo == null)
                        uow.GrupoCategoriaRepository.Inserir(itemSalvar);
                }
            }
        }

        private static void Excluir(UnitOfWork uow, List<GrupoCategoria> listaExcluir)
        {
            if (listaExcluir != null && listaExcluir.Count > 0)
            {
                foreach (var itemExcluir in listaExcluir)
                    uow.GrupoCategoriaRepository.Excluir(itemExcluir);
            }
        }
    }
}