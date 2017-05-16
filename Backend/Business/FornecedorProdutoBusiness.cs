using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    public class FornecedorProdutoBusiness
    {
        public static FornecedorProdutoBusiness New
        {
            get
            {
                return new FornecedorProdutoBusiness();
            }
        }

        public void Salvar(List<FornecedorProduto> lista, int parceiroId, UnitOfWork uow)
        {
            if (lista == null || lista.Count == 0)
            {
                List<FornecedorProduto> listaExcluir = uow.FornecedorProdutoRepository.Listar(p => p.ParceiroID == parceiroId);
                Excluir(uow, listaExcluir);
            }
            else
            {
                List<FornecedorProduto> listaSalva = uow.FornecedorProdutoRepository.Listar(p => p.ParceiroID == parceiroId);
                List<FornecedorProduto> listaExcluir = listaSalva.Where(w => !lista.Any(a => a.ID == w.ID)).ToList();
                Excluir(uow, listaExcluir);
                Alterar(uow, listaSalva, lista);
            }
        }

        private void Alterar(UnitOfWork uow, List<FornecedorProduto> listaSalva, List<FornecedorProduto> listaSalvar)
        {
            if (listaSalvar != null && listaSalvar.Count > 0)
            {
                foreach (var itemSalvar in listaSalvar)
                {
                    FornecedorProduto itemSalvo = listaSalva.FirstOrDefault(f => f.ID == itemSalvar.ID);
                    if (itemSalvo != null)
                    {
                        itemSalvo.CapacidadeMaxima = itemSalvar.CapacidadeMaxima;
                        itemSalvo.Valor = itemSalvar.Valor;
                        itemSalvo.Volume = itemSalvar.CapacidadeMaxima;
                        uow.FornecedorProdutoRepository.Alterar(itemSalvo);
                    }
                    else
                        uow.FornecedorProdutoRepository.Inserir(itemSalvar);
                }
            }
        }

        private static void Excluir(UnitOfWork uow, List<FornecedorProduto> listaExcluir)
        {
            if (listaExcluir != null && listaExcluir.Count > 0)
            {
                foreach (var itemExcluir in listaExcluir)
                    uow.FornecedorProdutoRepository.Excluir(itemExcluir);
            }
        }
    }
}
