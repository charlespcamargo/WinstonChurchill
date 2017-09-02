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

        public List<Usuario> Listar(Usuario usuario, Usuario filtro)
        {
            MontarFiltro(filtro);

            List<Usuario> data = new List<Usuario>();

            using (UnitOfWork UoW = new UnitOfWork())
            {
                usuario.Grupos = UoW.UsuarioXGrupoUsuarioRepository.Listar(p => p.UsuarioID == usuario.ID, null, "GrupoUsuario");

                if (usuario.Grupos != null)
                {
                    // SE FOR SUPER USUARIO, LISTO EU E TODOS OS OUTROS USUÁRIO QUE NÃO FOREM SUPER USUARIO...
                    if (usuario.Grupos.Any(a => a.GrupoUsuario.ID == (int)eTipoGrupoUsuario.SuperUsuario))
                        predicate = predicate.And(p => p.Grupos.Any(a => a.GrupoUsuario.ID != (int)eTipoGrupoUsuario.SuperUsuario) || p.ID == usuario.ID);

                    // SE ADMINISTRADOR, LISTO EU E TODOS OS OUTROS USUÁRIO QUE NÃO FOREM SUPER USUARIO E ADMINISTRADOR...
                    else if (usuario.Grupos.Any(a => a.GrupoUsuario.ID == (int)eTipoGrupoUsuario.Administrador))
                        predicate = predicate.And(p => p.Grupos.Any(w => w.GrupoUsuario.ID != (int)eTipoGrupoUsuario.SuperUsuario && w.GrupoUsuario.ID != (int)eTipoGrupoUsuario.Administrador) || p.ID == usuario.ID);

                    // SE FOR usuário Representante
                    else if (usuario.Grupos.Any(a => a.GrupoUsuario.ID != (int)eTipoGrupoUsuario.RepresentanteComercial))
                        predicate = predicate.And(p => p.ID == usuario.ID || p.ResponsavelID == usuario.ID);

                    // SE FOR usuário comum lista apenas informações dele
                    else if (usuario.Grupos.Any(a => a.GrupoUsuario.ID != (int)eTipoGrupoUsuario.SuperUsuario && a.GrupoUsuario.ID != (int)eTipoGrupoUsuario.Administrador))
                        predicate = predicate.And(p => p.ID == usuario.ID);
                }

                data = UoW.UsuarioRepository.Listar(predicate, null, "Grupos.GrupoUsuario");
            }

            if (data != null && data.Any()) { data.ForEach(f => f.Senha = null); };

            return data;
        }

        public List<Usuario> ListarRepresentantes(Usuario usuario, Usuario filtro)
        {
            List<Usuario> data = new List<Usuario>();

            using (UnitOfWork UoW = new UnitOfWork())
            {
                MontarFiltro(filtro);
                predicate = predicate.And(o => o.Ativo == true);

                data = UoW.UsuarioRepository.Listar(predicate, null, "Grupos");
            }

            if (data != null && data.Any())
            {
                data.ForEach(f =>
                {
                    f.Senha = null;
                });


                // removo os super usuários da lista
                data = data.Where(w => !w.Grupos.Exists(a => a.GrupoUsuarioID == (int)Backend.Model.Enumeradores.eTipoGrupoUsuario.SuperUsuario)).ToList();

                if (!usuario.ehAdministrador)
                    data = data.Where(w => w.Grupos.Any(a => a.GrupoUsuarioID == (int)Backend.Model.Enumeradores.eTipoGrupoUsuario.RepresentanteComercial)).ToList();
                else
                    data = data.Where(w => w.Grupos.Any(a => a.GrupoUsuarioID == (int)Backend.Model.Enumeradores.eTipoGrupoUsuario.RepresentanteComercial ||
                                                             a.GrupoUsuarioID == (int)Backend.Model.Enumeradores.eTipoGrupoUsuario.Administrador)).ToList();
            };

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

                if (talento == null)
                    throw new ArgumentException("Usuário não encontrado.");

                talento.Grupos = uow.UsuarioXGrupoUsuarioRepository.Listar(p => p.UsuarioID == talento.ID);

                if (talento.Grupos != null && talento.Grupos.Any())
                    foreach (var item in talento.Grupos)
                    {
                        uow.UsuarioXGrupoUsuarioRepository.Excluir(item);
                    }

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
            if (usuario.Grupos == null || usuario.Grupos.Count == 0)
                throw new ArgumentException("Informe pelo menos 1 grupo de acesso!");


            foreach (var item in usuario.Grupos)
            {
                item.DataCadastro = DateTime.Now;
                item.ResponsavelID = usuario.ResponsavelID;
                item.Ativo = true;
            }

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (usuario.ID == 0)
                {
                    if (String.IsNullOrEmpty(usuario.Senha))
                        throw new ArgumentException("Informe a Senha!");

                    usuario.Senha = Encrypting.sha512encrypt(usuario.Senha);
                    usuario.DataCadastro = DateTime.Now;
                    uow.UsuarioRepository.Inserir(usuario);
                }
                else
                {
                    Usuario usuarioSalvo = uow.UsuarioRepository.Carregar(c => c.ID == usuario.ID,
                                                                          o => o.OrderBy(by => by.ID));

                    if (usuarioSalvo == null)
                        throw new ArgumentException("Senha inválida!");

                    if (!String.IsNullOrEmpty(usuario.Senha))
                    {
                        if (String.IsNullOrEmpty(usuario.SenhaNova) || String.IsNullOrEmpty(usuario.SenhaNovaConfirmar))
                            throw new ArgumentException("Para alterar a sua Senha, informe a senha atual, além da Nova Senha e sua confirmação!");
                        else if (usuario.SenhaNova != usuario.SenhaNovaConfirmar)
                            throw new ArgumentException("A confirmação da nova senha não esta correta!");

                        usuario.SenhaNova = Encrypting.sha512encrypt(usuario.SenhaNova);
                        usuarioSalvo.Senha = usuario.SenhaNova;
                    }

                    usuarioSalvo.Nome = usuario.Nome;
                    usuarioSalvo.Email = usuario.Email;
                    usuarioSalvo.Ativo = true;
                    AtivarDesativarGrupoAcesso(usuario, uow);
                    uow.UsuarioRepository.Alterar(usuarioSalvo);
                }

                uow.Save();
            }
        }

        public List<Usuario> ListarResponsavel(Usuario usuario, Usuario filtro, int tipo)
        {
            List<Usuario> lst = new List<Usuario>();

            using (UnitOfWork UoW = new UnitOfWork())
            {
                predicate = UtilEntity.True<Usuario>();

                if (filtro.ID > 0)
                    predicate = predicate.And(o => o.ID == filtro.ID);

                if (!String.IsNullOrEmpty(filtro.Nome))
                    predicate = predicate.And(o => o.Nome.ToUpper().Contains(filtro.Nome));

                predicate = predicate.And(o => o.Ativo == true);

                // exceto eu
                predicate = predicate.And(o => o.ID != usuario.ID);

                // não exibo super usuario e admin
                predicate = predicate.And(p => !p.Grupos.Any(a => a.GrupoUsuarioID == (int)eTipoGrupoUsuario.SuperUsuario || a.GrupoUsuarioID == (int)eTipoGrupoUsuario.Administrador));

                // somente representante comercial e comprador
                if (tipo == 1)
                    predicate = predicate.And(p => p.Grupos.Any(a => a.GrupoUsuarioID == (int)eTipoGrupoUsuario.RepresentanteComercial || a.GrupoUsuarioID == (int)eTipoGrupoUsuario.Comprador));
                // somente representante comercial e fornecedor
                else if (tipo == 2)
                    predicate = predicate.And(p => p.Grupos.Any(a => a.GrupoUsuarioID == (int)eTipoGrupoUsuario.RepresentanteComercial || a.GrupoUsuarioID == (int)eTipoGrupoUsuario.Fornecedor));
                // representante comercial, comprador e comprador
                else if (tipo == 3)
                    predicate = predicate.And(p => p.Grupos.Any(a => a.GrupoUsuarioID == (int)eTipoGrupoUsuario.RepresentanteComercial || a.GrupoUsuarioID == (int)eTipoGrupoUsuario.Comprador || a.GrupoUsuarioID == (int)eTipoGrupoUsuario.Fornecedor));


                lst = UoW.UsuarioRepository.Listar(predicate, null, "Grupos");
            }

            if (lst != null && lst.Any())
            {
                lst.ForEach(f =>
                {
                    f.Senha = null;
                });
            };

            return lst;
        }

        private void AtivarDesativarGrupoAcesso(Usuario usuario, UnitOfWork uow)
        {
            List<UsuarioXGrupoUsuario> listaSalva = uow.UsuarioXGrupoUsuarioRepository.Listar(p => p.UsuarioID == usuario.ID);
            if (listaSalva != null && listaSalva.Any())
            {
                List<UsuarioXGrupoUsuario> listaExcluir = listaSalva.Where(w => !usuario.Grupos.Any(a => a.GrupoUsuarioID == w.GrupoUsuarioID)).ToList();

                foreach (var item in listaExcluir)
                {
                    if (item.GrupoUsuarioID != (int)eTipoGrupoUsuario.SuperUsuario)
                    {
                        item.Ativo = false;
                        uow.UsuarioXGrupoUsuarioRepository.Alterar(item, "ID");
                    }
                }
            }


            if (usuario.Grupos.Exists(e => e.GrupoUsuarioID == (int)eTipoGrupoUsuario.RepresentanteComercial && (e.GrupoUsuarioID == (int)eTipoGrupoUsuario.Comprador || e.GrupoUsuarioID == (int)eTipoGrupoUsuario.Fornecedor)))
                throw new ArgumentException("O usuário não pode ser [Representante Comercial] e [Comprador/Fornecedor] ao mesmo tempo!");

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
