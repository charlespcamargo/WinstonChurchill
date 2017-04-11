using WinstonChurchill.Backend.Models;
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
           

    }
}
