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

        public void Salvar(List<CategoriasProdutos> lista, int produtoId, UnitOfWork uow)
        {
            if (lista == null || lista.Count == 0)
            {
                List<CategoriasProdutos> listaExcluir = uow.CategoriasProdutosRepository.Listar(p => p.ProdutoID == produtoId);
                Excluir(uow, listaExcluir);
            }
            else
            {
                List<CategoriasProdutos> listaSalva = uow.CategoriasProdutosRepository.Listar(p => p.ProdutoID == produtoId);
                List<CategoriasProdutos> listaExcluir = listaSalva.Where(w => !lista.Any(a => a.ProdutoID == w.ProdutoID && a.CategoriaID == w.CategoriaID)).ToList();
                Excluir(uow, listaExcluir);
                Inserir(uow, listaSalva, lista);
            }
        }

        private void Inserir(UnitOfWork uow, List<CategoriasProdutos> listaSalva, List<CategoriasProdutos> listaSalvar)
        {
            if (listaSalvar != null && listaSalvar.Count > 0)
            {
                foreach (var itemSalvar in listaSalvar)
                {
                    CategoriasProdutos itemSalvo = listaSalva.FirstOrDefault(f => f.ProdutoID == itemSalvar.ProdutoID && f.CategoriaID == itemSalvar.CategoriaID);
                    if (itemSalvo == null)
                    {
                        uow.CategoriasProdutosRepository.Inserir(itemSalvar);
                    }
                }
            }
        }

        private static void Excluir(UnitOfWork uow, List<CategoriasProdutos> listaExcluir)
        {
            if (listaExcluir != null && listaExcluir.Count > 0)
            {
                foreach (var itemExcluir in listaExcluir)
                    uow.CategoriasProdutosRepository.Excluir(itemExcluir);
            }
        }
    }
}
