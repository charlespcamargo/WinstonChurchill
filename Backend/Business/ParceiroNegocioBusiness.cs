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
                throw new ArgumentException("Erro ao tentar salvar os dados");

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (entidade.CompradorProduto != null && entidade.CompradorProduto.Count > 0)
                {
                    foreach (var item in entidade.CompradorProduto)
                    {
                        item.ProdutoID = item.Produto.ID;
                        item.Produto = null;
                    }
                }

                if (entidade.FornecedorProduto != null && entidade.FornecedorProduto.Count > 0)
                {
                    foreach (var item in entidade.FornecedorProduto)
                    {
                        item.ProdutoID = item.Produto.ID;
                        item.Produto = null;
                    }
                }

                if (entidade.Usuarios != null && entidade.Usuarios.Count > 0)
                {
                    foreach (var item in entidade.Usuarios)
                    {
                        if (item.CriadorID == 0)
                            item.CriadorID = usuario.ID;

                        item.Criador = null;
                        item.Usuario = null;
                        item.DataCadastro = DateTime.Now;
                    }
                }

                entidade.UsuarioID = usuario.ID;

                if (entidade.ID == 0)
                {
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

                    entidade.ValidaTipoParceiro();


                    FornecedorProdutoBusiness.New.Salvar(entidade.FornecedorProduto, objSalvo.ID, uow);
                    CompradorProdutoBusiness.New.Salvar(entidade.CompradorProduto, objSalvo.ID, uow);
                    ContatoBusiness.New.Salvar(entidade.Contatos, objSalvo.ID, uow);
                    ParceiroNegocioGrupoBusiness.New.Salvar(entidade.Grupos, 0, objSalvo.ID, uow);
                    ParceiroNegocioUsuarioBusiness.New.Salvar(entidade, usuario, uow);
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

        public List<ParceiroNegocio> Listar(ParceiroNegocio filtro, int[] tipos)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                if (tipos != null && tipos.Length > 0)
                {
                    predicate = predicate.And(p => tipos.Any(a => a == p.TipoParceiro));
                }

                return uow.ParceiroNegocioRepository.Listar(predicate).ToList();
            }

        }

        public ParceiroNegocio Carregar(ParceiroNegocio filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                ParceiroNegocio objeto = uow.ParceiroNegocioRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID), "Endereco, Contatos, Usuarios");
                if (objeto != null)
                {
                    objeto.FornecedorProduto = uow.FornecedorProdutoRepository.Listar(p => p.ParceiroID == filtro.ID,
                                                                                        ord => ord.OrderBy(p => p.ID),
                                                                                        "Produto");

                    objeto.CompradorProduto = uow.CompradorProdutoRepository.Listar(p => p.ParceiroID == filtro.ID,
                                                                                      ord => ord.OrderBy(p => p.ID),
                                                                                      "Produto");

                    objeto.Grupos = uow.ParceiroNegocioGrupoRepository.Listar(p => p.ParceiroID == filtro.ID, null, "Grupo");

                    objeto.Usuarios.ForEach(f =>
                    {
                        f.Usuario = uow.UsuarioRepository.Carregar(c => c.ID == f.UsuarioID, o => o.OrderBy(by => by.ID));
                        f.Usuario.Senha = null;
                    });
                }
                return objeto;
            }
        }

        public void Excluir(ParceiroNegocio filtro)
        {
            if (filtro == null)
                throw new ArgumentException("Informe o Parceiro de Negocio para realizar a exclusão");

            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                ParceiroNegocio objExcluir = uow.ParceiroNegocioRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID), "Contatos,FornecedorProduto, CompradorProduto, Grupos");
                if (objExcluir == null)
                    throw new ArgumentException("Parceiro de Negocio não encontrado");

                Endereco endereco = uow.EnderecoRepository.Carregar(p => p.ID == objExcluir.EnderecoID, ord => ord.OrderBy(p => p.ID));
                uow.EnderecoRepository.Excluir(endereco);
                uow.ParceiroNegocioRepository.Excluir(objExcluir);
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

            if (filtro.Usuario == null || !filtro.Usuario.ehAdministrador)
                predicate = predicate.And(p => p.UsuarioID == filtro.UsuarioID || p.Usuarios.Any(a => a.UsuarioID == filtro.UsuarioID));



        }
    }
}
