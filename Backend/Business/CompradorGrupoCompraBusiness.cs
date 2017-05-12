using System.Collections.Generic;
using System.Linq;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    public class CompradorGrupoCompraBusiness
    {
        public static CompradorGrupoCompraBusiness New { get { return new CompradorGrupoCompraBusiness(); } }

        public void Salvar(List<CompradorGrupoCompra> lista, int compradorId, UnitOfWork uow)
        {
            if (lista == null || lista.Count == 0)
            {
                List<CompradorGrupoCompra> listaExcluir = uow.CompradorGrupoCompraRepository.Listar(p => p.CompradorID == compradorId);
                Excluir(uow, listaExcluir);
            }
            else
            {
                List<CompradorGrupoCompra> listaSalva = uow.CompradorGrupoCompraRepository.Listar(p => p.CompradorID == compradorId);
                List<CompradorGrupoCompra> listaExcluir = listaSalva.Where(w => !lista.Any(a => a.ID == w.ID)).ToList();
                Excluir(uow, listaExcluir);
                Salvar(uow, listaSalva, lista);
            }
        }

        private void Salvar(UnitOfWork uow, List<CompradorGrupoCompra> listaSalva, List<CompradorGrupoCompra> listaSalvar)
        {
            if (listaSalvar != null && listaSalvar.Count > 0)
            {
                foreach (var itemSalvar in listaSalvar)
                {
                    CompradorGrupoCompra itemSalvo = listaSalva.FirstOrDefault(f => f.ID == itemSalvar.ID);
                    if (itemSalvo == null)
                        uow.CompradorGrupoCompraRepository.Inserir(itemSalvar);
                }
            }
        }

        private static void Excluir(UnitOfWork uow, List<CompradorGrupoCompra> listaExcluir)
        {
            if (listaExcluir != null && listaExcluir.Count > 0)
            {
                foreach (var itemExcluir in listaExcluir)
                    uow.CompradorGrupoCompraRepository.Excluir(itemExcluir);
            }
        }
    }
}