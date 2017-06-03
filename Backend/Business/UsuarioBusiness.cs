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

                if (usuario == null || usuario.ID == 0)
                    throw new UnauthorizedAccessException("Acesso não autorizado. Usuário ou senhas inválidos!");

                return usuario;
            }
        }

        public List<Usuario> Listar(Usuario filtro)
        {
            MontarFiltro(filtro);

            List<Usuario> data = new List<Usuario>();

            using (UnitOfWork UoW = new UnitOfWork())
            {
                data = UoW.UsuarioRepository.Listar(predicate);
            }

            if (data != null && data.Any()) { data.ForEach(f => f.Senha = null); };

            return data;
        }

        public Usuario Carregar(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Usuario usuario = uow.UsuarioRepository.Carregar(p => p.ID == id, ord => ord.OrderBy(p => p.ID));
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

            if (!string.IsNullOrEmpty(entidade.Nome))
                predicate = predicate.And(o => o.Nome.Contains(entidade.Nome));

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

            if (String.IsNullOrEmpty(usuario.SenhaNova) != String.IsNullOrEmpty(usuario.SenhaNovaConfirmar))
                throw new ArgumentException("Informe a Nova Senha e Confirme-a!");
            else
            {
                if (usuario.SenhaNova != usuario.SenhaNovaConfirmar)
                    throw new ArgumentException("A confirmação da nova senha não esta correta!");

                usuario.Senha = Encrypting.sha512encrypt(usuario.Senha);
                usuario.SenhaNova = Encrypting.sha512encrypt(usuario.SenhaNova);
            }

            Usuario usuarioSalvo;

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (usuario.ID == 0)
                    uow.UsuarioRepository.Inserir(usuario);
                else
                {
                    usuarioSalvo = uow.UsuarioRepository.Carregar(c => c.ID == usuario.ID && c.Senha == usuario.Senha,
                                                                  o => o.OrderBy(by => by.ID));

                    if (usuarioSalvo == null)
                        throw new ArgumentException("Senha inválida!");


                    usuario.Senha = usuario.SenhaNova;
                    uow.UsuarioRepository.Alterar(usuario);
                }

                uow.Save();
            }
        }
    }
}
