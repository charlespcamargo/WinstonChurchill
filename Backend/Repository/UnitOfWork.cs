using WinstonChurchill.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.Backend.Repository
{
    public partial class UnitOfWork : UnitOfWorkBase
    {
        private BaseRepository<Usuario> usuarioRepository;
        public BaseRepository<Usuario> UsuarioRepository
        {
            get
            {
                if (this.usuarioRepository == null)
                {
                    this.usuarioRepository = new BaseRepository<Usuario>(context);
                }
                return usuarioRepository;
            }
        }

        private BaseRepository<UsuarioXGrupoUsuario> usuarioXGrupoUsuarioRepository;
        public BaseRepository<UsuarioXGrupoUsuario> UsuarioXGrupoUsuarioRepository
        {
            get
            {
                if (this.usuarioXGrupoUsuarioRepository == null)
                {
                    this.usuarioXGrupoUsuarioRepository = new BaseRepository<UsuarioXGrupoUsuario>(context);
                }
                return usuarioXGrupoUsuarioRepository;
            }
        }

        private BaseRepository<GrupoUsuario> grupoUsuarioRepository;
        public BaseRepository<GrupoUsuario> GrupoUsuarioRepository
        {
            get
            {
                if (this.grupoUsuarioRepository == null)
                {
                    this.grupoUsuarioRepository = new BaseRepository<GrupoUsuario>(context);
                }
                return grupoUsuarioRepository;
            }
        }

    }
}
