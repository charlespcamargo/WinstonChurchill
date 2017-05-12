using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    public class CompradorBusiness
    {
        Expression<Func<Comprador, bool>> predicate;

        public static CompradorBusiness New
        {
            get
            {
                return new CompradorBusiness();
            }
        }

        public Comprador Salvar(Comprador entidade, Usuario usuario)
        {
            if (entidade == null)
                throw new ArgumentException("Erro: nenhuma informação foi gerado");

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (entidade.ID == 0)
                {
                    entidade.UsuarioID = usuario.ID;
                    uow.CompradorRepository.Inserir(entidade);
                }
                else
                {
                    Comprador objSalvo = uow.CompradorRepository.Carregar(p => p.ID == entidade.ID, ord => ord.OrderBy(p => p.ID));
                    if (objSalvo != null)
                    {
                        objSalvo.Celular = entidade.Celular;
                        objSalvo.CNPJ = entidade.CNPJ;
                        objSalvo.Email = entidade.Email;
                        objSalvo.Telefone = entidade.Telefone;
                        objSalvo.NomeFantasia = entidade.NomeFantasia;
                        objSalvo.RazaoSocial = entidade.RazaoSocial;
                        objSalvo.UsuarioID = usuario.ID;
                        uow.CompradorRepository.Alterar(objSalvo);
                        CompradorGrupoCompraBusiness.New.Salvar(entidade.CompradorGrupoCompra, objSalvo.ID, uow);
                        CompradorProdutoBusiness.New.Salvar(entidade.CompradorProduto, objSalvo.ID, uow);
                        ContatoBusiness.New.Salvar(entidade.Contato, objSalvo.ID, uow);
                        EnderecoBusiness.New.Salvar(entidade.Endereco, objSalvo.ID, uow);
                    }
                }

                uow.Save();
                return entidade;
            }
        }

        public List<Comprador> Listar(Comprador filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                return uow.CompradorRepository.Listar(predicate).ToList();
            }

        }

        public Comprador Carregar(Comprador filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                Comprador comprador = uow.CompradorRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID), "Endereco");
                if(comprador != null)
                {
                    comprador.CompradorGrupoCompra = uow.CompradorGrupoCompraRepository.Listar(p => p.CompradorID == comprador.ID);
                    comprador.CompradorProduto = uow.CompradorProdutoRepository.Listar(p => p.CompradorID == comprador.ID);
                    comprador.Contato = uow.ContatoRepository.Listar(p => p.CompradorID == comprador.ID);
                }
                return comprador;
            }
        }

        public void Excluir(Comprador filtro)
        {
            if (filtro == null)
                throw new ArgumentException("Informe o Comprador para realizar a exclusão");

            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                Comprador CompradorExcluir = uow.CompradorRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID), "Endereco");
                if (CompradorExcluir == null)
                    throw new ArgumentException("Nenhum Comprador encontrado");

                CompradorExcluir.CompradorGrupoCompra = uow.CompradorGrupoCompraRepository.Listar(p => p.CompradorID == CompradorExcluir.ID);
                CompradorExcluir.CompradorProduto = uow.CompradorProdutoRepository.Listar(p => p.CompradorID == CompradorExcluir.ID);
                CompradorExcluir.Contato = uow.ContatoRepository.Listar(p => p.CompradorID == CompradorExcluir.ID);

                uow.CompradorRepository.Excluir(CompradorExcluir);

                uow.Save();
            }
        }
        private void MontarFiltro(Comprador filtro)
        {
            predicate = UtilEntity.True<Comprador>();

            if (!string.IsNullOrEmpty(filtro.CNPJ))
                predicate = predicate.And(p => p.CNPJ.Contains(filtro.CNPJ));

            if (!string.IsNullOrEmpty(filtro.Email))
                predicate = predicate.And(p => p.Email == filtro.Email);

            if (!string.IsNullOrEmpty(filtro.NomeFantasia))
                predicate = predicate.And(p => p.NomeFantasia == filtro.NomeFantasia);

            if (!string.IsNullOrEmpty(filtro.RazaoSocial))
                predicate = predicate.And(p => p.RazaoSocial == filtro.RazaoSocial);

            if (filtro.ID > 0)
                predicate = predicate.And(p => p.ID == filtro.ID);

            predicate = predicate.And(p => p.UsuarioID == filtro.UsuarioID);
        }
    }
}
