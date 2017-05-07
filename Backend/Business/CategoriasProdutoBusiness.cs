using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    public class CategoriasProdutoBusiness
    {
        public static CategoriasProdutoBusiness New
        {
            get
            {
                return new CategoriasProdutoBusiness();
            }
        }

        public void Salvar(List<CategoriaProduto> lista, int produtoId, UnitOfWork uow)
        {
            if (lista == null || lista.Count == 0)
            {
                List<CategoriaProduto> listaExcluir = uow.CategoriaProdutoRepository.Listar(p => p.ProdutoID == produtoId);
                Excluir(uow, listaExcluir);
            }
            else
            {
                List<CategoriaProduto> listaSalva = uow.CategoriaProdutoRepository.Listar(p => p.ProdutoID == produtoId);
                List<CategoriaProduto> listaExcluir = listaSalva.Where(w => !lista.Any(a => a.ProdutoID == w.ProdutoID && a.CategoriaID == w.CategoriaID)).ToList();
                Excluir(uow, listaExcluir);
                Inserir(uow, listaSalva, lista);
            }
        }

        private void Inserir(UnitOfWork uow, List<CategoriaProduto> listaSalva, List<CategoriaProduto> listaSalvar)
        {
            if (listaSalvar != null && listaSalvar.Count > 0)
            {
                foreach (var itemSalvar in listaSalvar)
                {
                    CategoriaProduto itemSalvo = listaSalva.FirstOrDefault(f => f.ProdutoID == itemSalvar.ProdutoID && f.CategoriaID == itemSalvar.CategoriaID);
                    if (itemSalvo == null)
                    {
                        uow.CategoriaProdutoRepository.Inserir(itemSalvar);
                    }
                }
            }
        }

        private static void Excluir(UnitOfWork uow, List<CategoriaProduto> listaExcluir)
        {
            if (listaExcluir != null && listaExcluir.Count > 0)
            {
                foreach (var itemExcluir in listaExcluir)
                    uow.CategoriaProdutoRepository.Excluir(itemExcluir);
            }
        }
    }
}
