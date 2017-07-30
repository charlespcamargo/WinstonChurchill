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
            MontarFiltro(usuario, filtro);

            List<Leilao> data = new List<Leilao>();

            using (UnitOfWork UoW = new UnitOfWork())
            {
                data = UoW.LeilaoRepository.Listar(predicate, null, "Produto,Representante");
            }

            return data;
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

            if (!usuario.ehAdministrador)
                predicate = predicate.And(o => o.RepresentanteID == usuario.ID);

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

                leilao.DataAbertura = DateTime.Now;
                leilao.CriadorID = usuario.ID;
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                SalvarCompradoresFornecedores(uow, leilao, salvo);

                if (leilao.ID > 0)
                    uow.LeilaoRepository.Alterar(salvo, "ID");
                else
                    uow.LeilaoRepository.Inserir(leilao);

                uow.Save();
            }

        }

        public void ValidarSalvar(Usuario usuario, Leilao leilao, Leilao salvo)
        {
            // INSERÇÃO
            if (salvo == null)
            {
                if (!usuario.ehAdministrador)
                {
                    if (leilao.RepresentanteID != usuario.ID)
                        throw new ArgumentException("Somente Administradores podem escolher um [Representante Comercial] para o Leilão.");
                }

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

                    if (!usuario.ehAdministrador && salvo.DataAbertura != leilao.DataAbertura)
                        throw new ArgumentException("Somente Administradores podem alterar a [Data de Abertura] após a adesão de algum Fornecedor/Comprador.");

                    if (salvo.ProdutoID != leilao.ProdutoID)
                        throw new ArgumentException("Não é permitido alterar o [Produto] após a adesão de algum Fornecedor/Comprador.");

                    if (salvo.Fornecedores.Exists(e => !leilao.Fornecedores.Any(a => e.Participando && a.ParceiroNegocioID == e.ParceiroNegocioID)))
                        throw new ArgumentException("Removeu um Fornecedor que iria participar!!!");

                    if (salvo.Compradores.Exists(e => !leilao.Compradores.Any(a => e.Participando && a.ParceiroNegocioID == e.ParceiroNegocioID)))
                        throw new ArgumentException("Removeu um Comprador que iria participar!!!");
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
                List<LeilaoComprador> lstExcluir = salvo.Compradores.Where(w => !leilao.Compradores.Any(a => a.ParceiroNegocioID == w.ParceiroNegocioID)).ToList();

                foreach (var item in lstExcluir)
                    uow.LeilaoCompradorRepository.Excluir(item.ID);
            }

            if (salvo != null)
            {
                List<LeilaoFornecedor> lstExcluir = salvo.Fornecedores.Where(w => !leilao.Fornecedores.Any(a => a.ParceiroNegocioID == w.ParceiroNegocioID)).ToList();

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