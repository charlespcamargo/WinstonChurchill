using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    public class EnderecoBusiness
    {
        public static EnderecoBusiness New { get { return new EnderecoBusiness(); } }

        public void Salvar(Endereco entidade, int enderecoID, UnitOfWork uow)
        {
            if (entidade != null)
            {
                Endereco objSalvo = uow.EnderecoRepository.Carregar(p => p.ID == enderecoID, ord => ord.OrderBy(p => p.ID));
                if (objSalvo != null)
                {
                    objSalvo.Bairro = entidade.Bairro;
                    objSalvo.CEP = entidade.CEP;
                    objSalvo.Cidade = entidade.Cidade;
                    objSalvo.Estado = entidade.Estado;
                    objSalvo.Logradouro = entidade.Logradouro;
                    uow.EnderecoRepository.Alterar(objSalvo);
                }
                else
                    uow.EnderecoRepository.Inserir(entidade);
            }
        }
    }
}