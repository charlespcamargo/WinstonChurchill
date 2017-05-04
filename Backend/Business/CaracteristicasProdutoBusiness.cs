using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    public class CaracteristicasProdutoBusiness
    {
        public static CaracteristicasProdutoBusiness New
        {
            get
            {
                return new CaracteristicasProdutoBusiness();
            }
        }

        public void Salvar(List<CaracteristicasProduto> lista, int produtoId, UnitOfWork uow)
        {
            if (lista == null || lista.Count == 0)
            {
                List<CaracteristicasProduto> listaExcluir = uow.CaracteristicasProdutoRepository.Listar(p => p.ProdutoID == produtoId);
                Excluir(uow, listaExcluir);
            }
            else
            {
                List<CaracteristicasProduto> listaSalva = uow.CaracteristicasProdutoRepository.Listar(p => p.ProdutoID == produtoId);
                List<CaracteristicasProduto> listaExcluir = listaSalva.Where(w => !lista.Any(a => a.ID == w.ID)).ToList();
                Excluir(uow, listaExcluir);
                Alterar(uow, listaSalva, lista);
            }
        }

        private void Alterar(UnitOfWork uow, List<CaracteristicasProduto> listaSalva, List<CaracteristicasProduto> listaSalvar)
        {
            if (listaSalvar != null && listaSalvar.Count > 0)
            {
                foreach (var itemSalvar in listaSalvar)
                {
                    CaracteristicasProduto itemSalvo = listaSalva.FirstOrDefault(f => f.ID == itemSalvar.ID);
                    if (itemSalvo != null)
                    {
                        itemSalvo.Nome = itemSalvar.Nome;
                        uow.CaracteristicasProdutoRepository.Alterar(itemSalvo);
                    }
                    else
                        uow.CaracteristicasProdutoRepository.Inserir(itemSalvar);
                }
            }
        }

        private static void Excluir(UnitOfWork uow, List<CaracteristicasProduto> listaExcluir)
        {
            if (listaExcluir != null && listaExcluir.Count > 0)
            {
                foreach (var itemExcluir in listaExcluir)
                    uow.CaracteristicasProdutoRepository.Excluir(itemExcluir);
            }
        }
    }
}
