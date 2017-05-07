using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    public class CaracteristicaProdutoBusiness
    {
        public static CaracteristicaProdutoBusiness New
        {
            get
            {
                return new CaracteristicaProdutoBusiness();
            }
        }

        public void Salvar(List<CaracteristicaProduto> lista, int produtoId, UnitOfWork uow)
        {
            if (lista == null || lista.Count == 0)
            {
                List<CaracteristicaProduto> listaExcluir = uow.CaracteristicaProdutoRepository.Listar(p => p.ProdutoID == produtoId);
                Excluir(uow, listaExcluir);
            }
            else
            {
                List<CaracteristicaProduto> listaSalva = uow.CaracteristicaProdutoRepository.Listar(p => p.ProdutoID == produtoId);
                List<CaracteristicaProduto> listaExcluir = listaSalva.Where(w => !lista.Any(a => a.ID == w.ID)).ToList();
                Excluir(uow, listaExcluir);
                Alterar(uow, listaSalva, lista);
            }
        }

        private void Alterar(UnitOfWork uow, List<CaracteristicaProduto> listaSalva, List<CaracteristicaProduto> listaSalvar)
        {
            if (listaSalvar != null && listaSalvar.Count > 0)
            {
                foreach (var itemSalvar in listaSalvar)
                {
                    CaracteristicaProduto itemSalvo = listaSalva.FirstOrDefault(f => f.ID == itemSalvar.ID);
                    if (itemSalvo != null)
                    {
                        itemSalvo.Nome = itemSalvar.Nome;
                        uow.CaracteristicaProdutoRepository.Alterar(itemSalvo);
                    }
                    else
                        uow.CaracteristicaProdutoRepository.Inserir(itemSalvar);
                }
            }
        }

        private static void Excluir(UnitOfWork uow, List<CaracteristicaProduto> listaExcluir)
        {
            if (listaExcluir != null && listaExcluir.Count > 0)
            {
                foreach (var itemExcluir in listaExcluir)
                    uow.CaracteristicaProdutoRepository.Excluir(itemExcluir);
            }
        }
    }
}
