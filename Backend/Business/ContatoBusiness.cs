﻿using System.Collections.Generic;
using System.Linq;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;


namespace WinstonChurchill.Backend.Business
{
    public class ContatoBusiness
    {
        public static ContatoBusiness New { get { return new ContatoBusiness(); } }

        public void Salvar(List<Contato> lista, int parceiroId, UnitOfWork uow)
        {
            List<Contato> listaSalva = uow.ContatoRepository.Listar(p => p.ParceiroID == parceiroId);
            if (lista == null || lista.Count == 0)
                Excluir(uow, listaSalva);
            else
            {
                List<Contato> listaExcluir = listaSalva.Where(w => !lista.Any(a => a.ID == w.ID)).ToList();
                Excluir(uow, listaExcluir);
                Salvar(uow, listaSalva, lista);
            }
        }

        private void Salvar(UnitOfWork uow, List<Contato> listaSalva, List<Contato> listaSalvar)
        {
            if (listaSalvar != null && listaSalvar.Count > 0)
            {
                foreach (var itemSalvar in listaSalvar)
                {
                    Contato itemSalvo = listaSalva.FirstOrDefault(f => f.ID == itemSalvar.ID);
                    if (itemSalvo == null)
                        uow.ContatoRepository.Inserir(itemSalvar);
                    else
                    {
                        itemSalvo.Email = itemSalvar.Email;
                        itemSalvo.Nome = itemSalvar.Nome;
                        itemSalvo.Telefone = itemSalvar.Telefone;
                        uow.ContatoRepository.Alterar(itemSalvar);
                    }
                }
            }
        }

        private static void Excluir(UnitOfWork uow, List<Contato> listaExcluir)
        {
            if (listaExcluir != null && listaExcluir.Count > 0)
            {
                foreach (var itemExcluir in listaExcluir)
                    uow.ContatoRepository.Excluir(itemExcluir);
            }
        }
    }
}