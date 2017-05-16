using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    public class ParceiroNegocioBusiness
    {
        Expression<Func<ParceiroNegocio, bool>> predicate;

        public static ParceiroNegocioBusiness New
        {
            get
            {
                return new ParceiroNegocioBusiness();
            }
        }

        public ParceiroNegocio Salvar(ParceiroNegocio entidade, Usuario usuario)
        {
            if (entidade == null)
                throw new ArgumentException("Erro: nenhuma informação foi gerado");

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (entidade.ID == 0)
                {
                    entidade.UsuarioID = usuario.ID;
                    entidade.DataCadastro = DateTime.Now;
                    uow.ParceiroNegocioRepository.Inserir(entidade);
                }
                else
                {
                    ParceiroNegocio objSalvo = uow.ParceiroNegocioRepository.Carregar(p => p.ID == entidade.ID, ord => ord.OrderBy(p => p.ID), "Endereco");
                    if (objSalvo != null)
                    {
                        objSalvo.AlterarObjeto(entidade);
                        uow.ParceiroNegocioRepository.Alterar(objSalvo);
                        uow.EnderecoRepository.Alterar(objSalvo.Endereco);
                    }
                    FornecedorProdutoBusiness.New.Salvar(entidade.FornecedorProduto, objSalvo.ID, uow);
                    CompradorProdutoBusiness.New.Salvar(entidade.CompradorProduto, objSalvo.ID, uow);
                    ContatoBusiness.New.Salvar(entidade.Contatos, objSalvo.ID, uow);
                }

                uow.Save();
                return entidade;
            }
        }

        public List<ParceiroNegocio> Listar(ParceiroNegocio filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                return uow.ParceiroNegocioRepository.Listar(predicate).ToList();
            }

        }

        public ParceiroNegocio Carregar(ParceiroNegocio filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                ParceiroNegocio objeto = uow.ParceiroNegocioRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID));
                return objeto;
            }
        }

        public void Excluir(ParceiroNegocio filtro)
        {
            if (filtro == null)
                throw new ArgumentException("Informe o ParceiroNegocio para realizar a exclusão");

            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                ParceiroNegocio ParceiroNegocioExcluir = uow.ParceiroNegocioRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID), "Endereco");
                uow.ParceiroNegocioRepository.Excluir(ParceiroNegocioExcluir);
                uow.Save();
            }
        }

        private void MontarFiltro(ParceiroNegocio filtro)
        {
            predicate = UtilEntity.True<ParceiroNegocio>();

            if (!string.IsNullOrEmpty(filtro.CNPJ))
                predicate = predicate.And(p => p.CNPJ.Contains(filtro.CNPJ));

            if (!string.IsNullOrEmpty(filtro.NomeFantasia))
                predicate = predicate.And(p => p.NomeFantasia.Contains(filtro.NomeFantasia));

            if (!string.IsNullOrEmpty(filtro.RazaoSocial))
                predicate = predicate.And(p => p.RazaoSocial.Contains(filtro.RazaoSocial));

            if (filtro.TipoParceiro > 0)
                predicate = predicate.And(p => p.TipoParceiro == filtro.TipoParceiro);

            if (filtro.ID > 0)
                predicate = predicate.And(p => p.ID == filtro.ID);

            predicate = predicate.And(p => p.UsuarioID == filtro.UsuarioID);
        }
    }
}
