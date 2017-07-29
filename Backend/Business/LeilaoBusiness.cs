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

        public int Contar(Leilao entidade)
        {
            MontarFiltro(entidade);

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.LeilaoRepository.Contar(predicate);
            }
        }

        public List<Leilao> Listar(Leilao filtro)
        {
            MontarFiltro(filtro);

            List<Leilao> data = new List<Leilao>();

            using (UnitOfWork UoW = new UnitOfWork())
            {
                //filtro.Grupos = UoW.LeilaoRepository.Listar(p => p.UsuarioID == filtro.ID, null, "GrupoUsuario");

                //if (filtro.Grupos != null && filtro.Grupos.Any(a => a.GrupoUsuario.ID == 1001)) //Lista apenas os usuários de responsabilidade de cada admin
                //    predicate = predicate.And(p => p.Grupos.Any(w => w.ResponsavelID == filtro.ID));

                //else if (filtro.Grupos != null && !filtro.Grupos.Any(a => a.GrupoUsuario.ID == 1000))   //Se for usuário comum lista apenas informações dele
                //    predicate = predicate.And(p => p.ID == filtro.ID);

                data = UoW.LeilaoRepository.Listar(predicate, null, "Produto,Representante");
            }

            return data;
        }

        public Leilao Carregar(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Leilao leilao = uow.LeilaoRepository.Carregar(p => p.ID == id, ord => ord.OrderBy(p => p.ID));
                if (leilao == null)
                    throw new ArgumentException("Usuário não encontrado!");

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

        private void MontarFiltro(Leilao entidade)
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

                salvo.Ativo = leilao.Ativo;
                salvo.Compradores = leilao.Compradores;
                salvo.DataAbertura = leilao.DataAbertura;
                salvo.DataFinalFormacao = leilao.DataFinalFormacao;
                salvo.DuracaoRodadasDias = leilao.DuracaoRodadasDias;
                salvo.Fornecedores = leilao.Fornecedores;
                salvo.Nome = leilao.Nome;
                salvo.ProdutoID = leilao.ProdutoID;
                salvo.RepresentanteID = leilao.RepresentanteID;
            }
            else
            {
                leilao.DataAbertura = DateTime.Now;
                leilao.CriadorID = usuario.ID;
                leilao.RepresentanteID = usuario.ID;
                leilao.Ativo = true;
            }

            ValidarSalvar(usuario, leilao, salvo);

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (leilao.ID > 0)
                    uow.LeilaoRepository.Alterar(salvo);
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



    }


}

