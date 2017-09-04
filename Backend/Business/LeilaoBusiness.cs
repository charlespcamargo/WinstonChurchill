using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;
using WinstonChurchill.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinstonChurchill.API.Common.Util.Captcha;
using WinstonChurchill.Backend.Model.Enumeradores;

namespace WinstonChurchill.Backend.Business
{
    public class LeilaoBusiness
    {
        private Expression<Func<Leilao, bool>> predicate;

        public static LeilaoBusiness New
        {
            get
            {
                return new LeilaoBusiness();
            }
        }

        public int Contar(Usuario usuario, Leilao entidade)
        {
            MontarFiltro(usuario, entidade);

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.LeilaoRepository.Contar(predicate);
            }
        }

        public List<Leilao> Listar(Usuario usuario, Leilao filtro)
        {
            List<Leilao> lst = new List<Leilao>();

            using (UnitOfWork UoW = new UnitOfWork())
            {
                if (usuario.ehAdministrador)
                    lst = UoW.LeilaoRepository.Listar(null, o => o.OrderBy(by => by.ID), "Produto,Representante");
                else
                {
                    // LEILÃO QUE: Possua um comprador do qual o usuário criou ou é responsável
                    // LEILÃO QUE: Possua um fornecedor do qual o usuário criou ou é responsável
                    lst = UoW.LeilaoRepository.Listar(l => l.Compradores.Any(c => c.ParceiroNegocio.Usuarios.Any(u => u.UsuarioID == usuario.ID)) ||
                                                           l.Compradores.Any(c => c.ParceiroNegocio.UsuarioID == usuario.ID) ||
                                                           l.Fornecedores.Any(c => c.ParceiroNegocio.UsuarioID == usuario.ID) ||
                                                           l.Fornecedores.Any(c => c.ParceiroNegocio.Usuarios.Any(u => u.UsuarioID == usuario.ID)) ||
                                                           l.RepresentanteID == usuario.ID || l.CriadorID == usuario.ID,

                                                      o => o.OrderBy(by => by.ID),
                                                      "Produto,Representante");
                }
            }


            return lst;
        }

        public Leilao Carregar(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Leilao leilao = uow.LeilaoRepository.Carregar(p => p.ID == id,
                                                              o => o.OrderBy(p => p.ID),
                                                              "Produto,Representante");

                leilao.Compradores = uow.LeilaoCompradorRepository.Listar(l => l.LeilaoID == leilao.ID, o => o.OrderBy(by => by.ID), "ParceiroNegocio");
                leilao.Fornecedores = uow.LeilaoFornecedorRepository.Listar(l => l.LeilaoID == leilao.ID, o => o.OrderBy(by => by.ID), "ParceiroNegocio");

                return leilao;
            }
        }

        public Leilao Lances(Usuario usuario, int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Leilao leilao = uow.LeilaoRepository.Carregar(p => p.ID == id, o => o.OrderBy(p => p.ID), "Produto,Representante");

                if (usuario.ehAdministrador)
                {
                    leilao.Compradores = uow.LeilaoCompradorRepository.Listar(l => l.LeilaoID == leilao.ID, o => o.OrderBy(by => by.ID), "ParceiroNegocio");
                    leilao.Fornecedores = uow.LeilaoFornecedorRepository.Listar(l => l.LeilaoID == leilao.ID, o => o.OrderBy(by => by.ID), "ParceiroNegocio");
                }
                else
                {
                    List<ParceiroNegocio> lstPN = uow.ParceiroNegocioRepository.Listar(l => l.UsuarioID == usuario.ID || l.Usuarios.Any(a => a.UsuarioID == usuario.ID),
                                                                                       o => o.OrderBy(by => by.ID));

                    leilao.Compradores = uow.LeilaoCompradorRepository.Listar(l => l.LeilaoID == leilao.ID, o => o.OrderBy(by => by.ID), "ParceiroNegocio");
                    leilao.Compradores = leilao.Compradores.Where(w => lstPN.Exists(e => e.ID == w.ParceiroNegocioID)).ToList();

                    leilao.Fornecedores = uow.LeilaoFornecedorRepository.Listar(l => l.LeilaoID == leilao.ID, o => o.OrderBy(by => by.ID), "ParceiroNegocio");
                    leilao.Fornecedores = leilao.Fornecedores.Where(w => lstPN.Exists(e => e.ID == w.ParceiroNegocioID)).ToList();
                }

                return leilao;
            }
        }

        public void SalvarFornecedor(Usuario usuario, LeilaoFornecedor leilaoFornecedor)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                //usuario TEM PERMISSÂO????

                var leilao = uow.LeilaoRepository.Carregar(c => c.ID == leilaoFornecedor.LeilaoID, o => o.OrderBy(by => by.ID));

                if (leilao.DataFinalFormacao < DateTime.Now)
                    throw new ArgumentException("Não é permitido alterar a participação do [Fornecedor] após a [Data Final de Formação]");

                var salvo = uow.LeilaoFornecedorRepository.Carregar(c => c.LeilaoID == leilaoFornecedor.LeilaoID && c.ParceiroNegocioID == leilaoFornecedor.ParceiroNegocioID,
                                                                    o => o.OrderBy(by => by.ID));

                salvo.ParceiroNegocio = null;
                salvo.Participando = leilaoFornecedor.Participando;
                salvo.QtdMinima = leilaoFornecedor.QtdMinima;
                salvo.QtdMaxima = leilaoFornecedor.QtdMaxima;

                uow.LeilaoFornecedorRepository.Alterar(salvo, "ID");
                uow.Save();
            }
        }

        public void SalvarComprador(Usuario usuario, LeilaoComprador leilaoComprador)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                //usuario TEM PERMISSÂO????

                var leilaoSalvo = uow.LeilaoRepository.Carregar(c => c.ID == leilaoComprador.LeilaoID, o => o.OrderBy(by => by.ID));

                if (leilaoSalvo.DataFinalFormacao < DateTime.Now)
                    throw new ArgumentException("Não é permitido alterar a participação do [Comprador] após a [Data Final de Formação]");

                var salvo = uow.LeilaoCompradorRepository.Carregar(c => c.LeilaoID == leilaoComprador.LeilaoID && c.ParceiroNegocioID == leilaoComprador.ParceiroNegocioID,
                                                                   o => o.OrderBy(by => by.ID));

                salvo.ParceiroNegocio = null;
                salvo.Participando = leilaoComprador.Participando;
                salvo.QtdDesejada = leilaoComprador.QtdDesejada;
                uow.LeilaoCompradorRepository.Alterar(salvo, "ID");
                uow.Save();

                Leilao leilao = uow.LeilaoRepository.Carregar(c => c.ID == salvo.LeilaoID, o => o.OrderBy(by => by.ID));
                var lst = uow.LeilaoCompradorRepository.Listar(l => l.LeilaoID == leilao.ID && l.Participando, o => o.OrderBy(by => by.ID));

                if (lst != null)
                {
                    leilao.QtdDesejada = lst.Sum(s => s.QtdDesejada);
                    uow.LeilaoRepository.Alterar(leilao, "ID");
                    uow.Save();
                }
            }
        }

        public void Excluir(int id)
        {
            Leilao leilao = new Leilao();

            using (UnitOfWork uow = new UnitOfWork())
            {
                leilao = uow.LeilaoRepository.Carregar(c => c.ID == id, o => o.OrderBy(by => by.ID));

                if (leilao == null)
                    throw new ArgumentException("Usuário não encontrado.");


                uow.UsuarioRepository.Excluir(leilao.ID);

                uow.Save();
            }
        }

        private void MontarFiltro(string busca)
        {
            predicate = UtilEntity.True<Leilao>();

            if (!string.IsNullOrEmpty(busca))
                predicate = predicate.And(a => a.Nome.Contains(busca));
            else
                predicate = predicate.And(a => a.ID >= 0);
        }

        private void MontarFiltro(Usuario usuario, Leilao entidade)
        {
            predicate = UtilEntity.True<Leilao>();

            if (!string.IsNullOrEmpty(entidade.Nome))
                predicate = predicate.And(o => o.Nome == entidade.Nome);

        }

        public void Salvar(Usuario usuario, Leilao leilao)
        {
            Leilao salvo = null;

            if (leilao.ID > 0)
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    salvo = uow.LeilaoRepository.Carregar(c => c.ID == leilao.ID, o => o.OrderBy(by => by.ID));
                    salvo.Fornecedores = uow.LeilaoFornecedorRepository.Listar(l => l.LeilaoID == salvo.ID, o => o.OrderBy(by => by.ID));
                    salvo.Compradores = uow.LeilaoCompradorRepository.Listar(l => l.LeilaoID == salvo.ID, o => o.OrderBy(by => by.ID));
                }

                ValidarSalvar(usuario, leilao, salvo);

                salvo.DataAbertura = leilao.DataAbertura;
                salvo.DataFinalFormacao = leilao.DataFinalFormacao;
                salvo.DuracaoRodadasDias = leilao.DuracaoRodadasDias;
                salvo.Nome = leilao.Nome;
                salvo.ProdutoID = leilao.ProdutoID;
                salvo.RepresentanteID = leilao.RepresentanteID;
                salvo.Ativo = leilao.Ativo;
            }
            else
            {
                ValidarSalvar(usuario, leilao, salvo);

                leilao.CriadorID = usuario.ID;
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                SalvarCompradoresFornecedores(uow, leilao, salvo);

                if (leilao.ID > 0)
                    uow.LeilaoRepository.Alterar(salvo, "ID");
                else
                {
                    Parametro p = uow.ParametroRepository.Carregar(c => c.ID > 0, o => o.OrderByDescending(by => by.ID));
                    leilao.DiasCadaRodada = p.DiasCadaRodada;
                    leilao.RodadasLeilao = p.RodadasLeilao;

                    uow.LeilaoRepository.Inserir(leilao);
                }

                uow.Save();
            }

        }

        public void ValidarSalvar(Usuario usuario, Leilao leilao, Leilao salvo)
        {
            if ((salvo == null && leilao.DataFinalFormacao <= DateTime.Today) || (salvo != null && salvo.DataFinalFormacao.Date != leilao.DataFinalFormacao.Date && leilao.DataFinalFormacao <= DateTime.Today))
                throw new ArgumentException("A [Data de Formação] é inválida! O Leilão precisa ser ao menos 1 dias após a data de criação");

            if (leilao.DataFinalFormacao >= leilao.DataAbertura)
                throw new ArgumentException("O Leião precisa encerrar sua [Formação] antes da [Data de Abertura].");

            if (leilao.Fornecedores != null)
            {
                if (leilao.Fornecedores.Exists(e => e.QtdMinima > e.QtdMaxima))
                    throw new Exception("Quantidade minima do fornecedor não pode ser maior que a sua quantidade máxima");

                if (leilao.Fornecedores.Exists(e => e.QtdMinima <= 0 || e.QtdMaxima <= 0))
                    throw new Exception("A Quantidade do fornecedor não pode ser igual ou menor que zero.");
            }

            if (leilao.Compradores != null && leilao.Compradores.Exists(e => e.QtdDesejada <= 0))
                throw new Exception("A Quantidade do Desejada pelo Comprador não pode ser igual ou menor que zero.");

            if (!usuario.ehAdministrador && leilao.RepresentanteID != usuario.ID)
            {
                throw new ArgumentException("Somente Administradores podem escolher um [Representante Comercial] para o Leilão.");
            }

            // INSERÇÃO
            if (salvo == null)
            {

            }
            // ATUALIZAÇÃO
            else
            {
                bool ehResponsavel = usuario.ehAdministrador || salvo.RepresentanteID == usuario.ID || salvo.CriadorID == usuario.ID;

                if (!ehResponsavel)
                    throw new ArgumentException("Somente Administradores e o Representante Comercial Responsável tem permissão para alterar o Leilão.");

                if (!usuario.ehAdministrador)
                {
                    if (salvo.DuracaoRodadasDias != leilao.DuracaoRodadasDias)
                        throw new ArgumentException("Somente Administradores podem alterar a [Duração das Rodadas] para o Leilão.");
                }

                if (salvo.temParticipantes)
                {
                    if (!usuario.ehAdministrador && salvo.Ativo && !leilao.Ativo)
                        throw new ArgumentException("Somente Administradores podem [Inativar] um Leilão após a adesão de algum Fornecedor/Comprador.");

                    if (!usuario.ehAdministrador && salvo.DataAbertura.Date != leilao.DataAbertura.Date)
                        throw new ArgumentException("Somente Administradores podem alterar a [Data de Abertura] após a adesão de algum Fornecedor/Comprador.");

                    if (salvo.ProdutoID != leilao.ProdutoID)
                        throw new ArgumentException("Não é permitido alterar o [Produto] após a adesão de algum Fornecedor/Comprador.");

                    if (salvo.Fornecedores.Exists(s => s.Participando && !leilao.Fornecedores.Exists(a => a.ParceiroNegocioID == s.ParceiroNegocioID)))
                        throw new ArgumentException("Não é possível remover um Fornecedor que irá participar! É necessário cancelar a participação antes.");

                    if (salvo.Compradores.Exists(s => s.Participando && !leilao.Compradores.Exists(a => a.ParceiroNegocioID == s.ParceiroNegocioID)))
                        throw new ArgumentException("Não é possível remover um Comprador que irá participar! É necessário cancelar a participação antes.");
                }
            }


        }

        public void SalvarCompradoresFornecedores(UnitOfWork uow, Leilao leilao, Leilao salvo)
        {
            if (leilao.Compradores == null)
                leilao.Compradores = new List<LeilaoComprador>();

            if (leilao.Fornecedores == null)
                leilao.Fornecedores = new List<LeilaoFornecedor>();

            if (salvo != null)
            {
                List<LeilaoComprador> lstExcluir = salvo.Compradores.Where(w => !leilao.Compradores.Any(a => a.ID == w.ID)).ToList();

                foreach (var item in lstExcluir)
                    uow.LeilaoCompradorRepository.Excluir(item.ID);
            }

            if (salvo != null)
            {
                List<LeilaoFornecedor> lstExcluir = salvo.Fornecedores.Where(w => !leilao.Fornecedores.Any(a => a.ID == w.ID)).ToList();

                foreach (var item in lstExcluir)
                    uow.LeilaoFornecedorRepository.Excluir(item.ID);
            }



            leilao.Compradores.ForEach(f =>
            {
                f.ParceiroNegocio = null;
                f.Leilao = null;

                if (f.ID > 0)
                    uow.LeilaoCompradorRepository.Alterar(f, "ID");
                else
                    uow.LeilaoCompradorRepository.Inserir(f);
            });



            leilao.Fornecedores.ForEach(f =>
            {
                f.ParceiroNegocio = null;
                f.Leilao = null;

                if (f.ID > 0)
                    uow.LeilaoFornecedorRepository.Alterar(f, "ID");
                else
                    uow.LeilaoFornecedorRepository.Inserir(f);
            });
        }


    }
}