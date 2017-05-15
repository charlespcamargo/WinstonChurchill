using System.Collections.Generic;
using System.Linq;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    public class CompradorProdutoBusiness
    {
        public static CompradorProdutoBusiness New { get { return new CompradorProdutoBusiness(); } }

        public void Salvar(List<CompradorProduto> lista, int compradorId, UnitOfWork uow)
        {
            if (lista == null || lista.Count == 0)
            {
                List<CompradorProduto> listaExcluir = uow.CompradorProdutoRepository.Listar(p => p.ParceiroID == compradorId);
                Excluir(uow, listaExcluir);
            }
            else
            {
                List<CompradorProduto> listaSalva = uow.CompradorProdutoRepository.Listar(p => p.ParceiroID == compradorId);
                List<CompradorProduto> listaExcluir = listaSalva.Where(w => !lista.Any(a => a.ID == w.ID)).ToList();
                Excluir(uow, listaExcluir);
                Salvar(uow, listaSalva, lista);
            }
        }

        private void Salvar(UnitOfWork uow, List<CompradorProduto> listaSalva, List<CompradorProduto> listaSalvar)
        {
            if (listaSalvar != null && listaSalvar.Count > 0)
            {
                foreach (var itemSalvar in listaSalvar)
                {
                    CompradorProduto itemSalvo = listaSalva.FirstOrDefault(f => f.ID == itemSalvar.ID);
                    if (itemSalvo == null)
                        uow.CompradorProdutoRepository.Inserir(itemSalvar);
                }
            }
        }

        private static void Excluir(UnitOfWork uow, List<CompradorProduto> listaExcluir)
        {
            if (listaExcluir != null && listaExcluir.Count > 0)
            {
                foreach (var itemExcluir in listaExcluir)
                    uow.CompradorProdutoRepository.Excluir(itemExcluir);
            }
        }
    }
}