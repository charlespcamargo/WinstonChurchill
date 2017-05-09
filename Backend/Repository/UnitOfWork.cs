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


        private BaseRepository<Categoria> categoriaRepository;
        public BaseRepository<Categoria> CategoriaRepository
        {
            get
            {
                if (this.categoriaRepository == null)
                {
                    this.categoriaRepository = new BaseRepository<Categoria>(context);
                }
                return categoriaRepository;
            }
        }


        private BaseRepository<CategoriaProduto> categoriaProdutoRepository;
        public BaseRepository<CategoriaProduto> CategoriaProdutoRepository
        {
            get
            {
                if (this.categoriaProdutoRepository == null)
                {
                    this.categoriaProdutoRepository = new BaseRepository<CategoriaProduto>(context);
                }
                return categoriaProdutoRepository;
            }
        }

        private BaseRepository<CategoriaImagem> categoriaImagemRepository;
        public BaseRepository<CategoriaImagem> CategoriaImagemRepository
        {
            get
            {
                if (this.categoriaImagemRepository == null)
                {
                    this.categoriaImagemRepository = new BaseRepository<CategoriaImagem>(context);
                }
                return categoriaImagemRepository;
            }
        }

        private BaseRepository<Produto> produtosRepository;
        public BaseRepository<Produto> ProdutosRepository
        {
            get
            {
                if (this.produtosRepository == null)
                {
                    this.produtosRepository = new BaseRepository<Produto>(context);
                }
                return produtosRepository;
            }
        }


        private BaseRepository<ProdutoImagem> produtoImagensRepository;
        public BaseRepository<ProdutoImagem> ProdutoImagensRepository
        {
            get
            {
                if (this.produtoImagensRepository == null)
                {
                    this.produtoImagensRepository = new BaseRepository<ProdutoImagem>(context);
                }
                return produtoImagensRepository;
            }
        }

        private BaseRepository<CaracteristicaProduto> caracteristicaProdutoRepository;
        public BaseRepository<CaracteristicaProduto> CaracteristicaProdutoRepository
        {
            get
            {
                if (this.caracteristicaProdutoRepository == null)
                {
                    this.caracteristicaProdutoRepository = new BaseRepository<CaracteristicaProduto>(context);
                }
                return caracteristicaProdutoRepository;
            }
        }

        private BaseRepository<Imagem> imagemRepository;
        public BaseRepository<Imagem> ImagemRepository
        {
            get
            {
                if (this.imagemRepository == null)
                {
                    this.imagemRepository = new BaseRepository<Imagem>(context);
                }
                return imagemRepository;
            }
        }

        private BaseRepository<Parametro> parametroRepository;
        public BaseRepository<Parametro> ParametroRepository
        {
            get
            {
                if (this.parametroRepository == null)
                {
                    this.parametroRepository = new BaseRepository<Parametro>(context);
                }
                return parametroRepository;
            }
        }
    }
}
