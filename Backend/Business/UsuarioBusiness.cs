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
    public class UsuarioBusiness
    {
        private Expression<Func<Usuario, bool>> predicate;

        public static UsuarioBusiness New
        {
            get
            {
                return new UsuarioBusiness();
            }
        }

        public int Contar(Usuario entidade)
        {
            MontarFiltro(entidade);

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.UsuarioRepository.Contar(predicate);
            }
        }

        public Usuario Autenticar(Usuario usuario)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(usuario);
                predicate = predicate.And(o => o.Ativo == true);
                usuario = uow.UsuarioRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID), "Grupos.GrupoUsuario");
                return usuario;
            }
        }

        public List<Usuario> Listar(Usuario filtro)
        {
            MontarFiltro(filtro);

            List<Usuario> data = new List<Usuario>();

            using (UnitOfWork UoW = new UnitOfWork())
            {
                filtro.Grupos = UoW.UsuarioXGrupoUsuarioRepository.Listar(p => p.UsuarioID == filtro.ID, null, "GrupoUsuario");

                if (filtro.Grupos != null && filtro.Grupos.Any(a => a.GrupoUsuario.ID == 1001)) //Lista apenas os usuários de responsabilidade de cada admin
                    predicate = predicate.And(p => p.Grupos.Any(w => w.ResponsavelID == filtro.ID));
                else if (filtro.Grupos != null && !filtro.Grupos.Any(a => a.GrupoUsuario.ID == 1000))   //Se for usuário comum lista apenas informações dele
                    predicate = predicate.And(p => p.ID == filtro.ID);
                data = UoW.UsuarioRepository.Listar(predicate);
            }

            if (data != null && data.Any()) { data.ForEach(f => f.Senha = null); };

            return data;
        }

        public Usuario Carregar(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Usuario usuario = uow.UsuarioRepository.Carregar(p => p.ID == id, ord => ord.OrderBy(p => p.ID), "Grupos");
                if (usuario == null)
                    throw new ArgumentException("Usuário não encontrado!");

                usuario.Senha = null;
                return usuario;
            }
        }

        public void Excluir(int id)
        {
            Usuario talento = new Usuario();

            using (UnitOfWork uow = new UnitOfWork())
            {
                talento = uow.UsuarioRepository.Carregar(c => c.ID == id, o => o.OrderBy(by => by.ID));

                uow.UsuarioRepository.Excluir(talento.ID);

                uow.Save();
            }
        }

        private void MontarFiltro(string busca)
        {
            predicate = UtilEntity.True<Usuario>();

            if (!string.IsNullOrEmpty(busca))
                predicate = predicate.And(a => a.Nome.Contains(busca));
            else
                predicate = predicate.And(a => a.ID >= 0);
        }

        private void MontarFiltro(Usuario entidade)
        {
            predicate = UtilEntity.True<Usuario>();

            if (!string.IsNullOrEmpty(entidade.Email))
                predicate = predicate.And(o => o.Email == entidade.Email);

            if (!string.IsNullOrEmpty(entidade.Senha))
            {
                string senha = Encrypting.sha512encrypt(entidade.Senha);
                predicate = predicate.And(o => o.Senha == senha);
            }
        }

        public void Salvar(Usuario usuario)
        {
            if (String.IsNullOrEmpty(usuario.Senha))
                throw new ArgumentException("Informe a Senha!");


            if (usuario.Grupos == null || usuario.Grupos.Count == 0)
                throw new ArgumentException("Informe pelo menos 1 grupo de acesso!");

            usuario.Senha = Encrypting.sha512encrypt(usuario.Senha);

            foreach (var item in usuario.Grupos)
            {
                item.DataCadastro = DateTime.Now;
                item.ResponsavelID = usuario.ResponvelID;
                item.Ativo = true;
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (usuario.ID == 0)
                {
                    usuario.DataCadastro = DateTime.Now;
                    uow.UsuarioRepository.Inserir(usuario);
                }
                else
                {
                    if (String.IsNullOrEmpty(usuario.SenhaNova) != String.IsNullOrEmpty(usuario.SenhaNovaConfirmar))
                        throw new ArgumentException("Informe a Nova Senha e Confirme-a!");
                    else
                        if (usuario.SenhaNova != usuario.SenhaNovaConfirmar)
                        throw new ArgumentException("A confirmação da nova senha não esta correta!");

                    usuario.SenhaNova = Encrypting.sha512encrypt(usuario.SenhaNova);

                    Usuario usuarioSalvo = uow.UsuarioRepository.Carregar(c => c.ID == usuario.ID && c.Senha == usuario.Senha,
                                                                  o => o.OrderBy(by => by.ID));

                    if (usuarioSalvo == null)
                        throw new ArgumentException("Senha inválida!");

                    usuarioSalvo.Ativo = true;
                    usuarioSalvo.Senha = usuario.SenhaNova;

                    AtivarDesativarGrupoAcesso(usuario, uow);
                    uow.UsuarioRepository.Alterar(usuarioSalvo);
                }

                uow.Save();
            }
        }

        private void AtivarDesativarGrupoAcesso(Usuario usuario, UnitOfWork uow)
        {
            List<UsuarioXGrupoUsuario> listaSalva = uow.UsuarioXGrupoUsuarioRepository.Listar(p => p.UsuarioID == usuario.ID);
            if (listaSalva != null && listaSalva.Any())
            {
                List<UsuarioXGrupoUsuario> listaExcluir = listaSalva.Where(w => !usuario.Grupos.Any(a => a.GrupoUsuarioID == w.GrupoUsuarioID)).ToList();

                foreach (var item in listaExcluir)
                {
                    item.Ativo = false;
                    uow.UsuarioXGrupoUsuarioRepository.Alterar(item, "ID");
                }
            }
            foreach (var item in usuario.Grupos)
            {
                UsuarioXGrupoUsuario grupoSalvo = uow.UsuarioXGrupoUsuarioRepository.Carregar(p => p.UsuarioID == usuario.ID && p.GrupoUsuarioID == item.GrupoUsuarioID, ord => ord.OrderBy(p => p.ID));
                if (grupoSalvo == null)
                {
                    item.Ativo = true;
                    item.UsuarioID = usuario.ID;
                    item.DataCadastro = DateTime.Now;
                    uow.UsuarioXGrupoUsuarioRepository.Inserir(item);
                }
                else
                {
                    grupoSalvo.Ativo = true;
                    uow.UsuarioXGrupoUsuarioRepository.Alterar(grupoSalvo, "ID");
                }
            }
        }
    }
}
