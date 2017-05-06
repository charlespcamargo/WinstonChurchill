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


        private BaseRepository<Categorias> categoriaRepository;
        public BaseRepository<Categorias> CategoriaRepository
        {
            get
            {
                if (this.categoriaRepository == null)
                {
                    this.categoriaRepository = new BaseRepository<Categorias>(context);
                }
                return categoriaRepository;
            }
        }


        private BaseRepository<CategoriasProdutos> categoriasProdutosRepository;
        public BaseRepository<CategoriasProdutos> CategoriasProdutosRepository
        {
            get
            {
                if (this.categoriasProdutosRepository == null)
                {
                    this.categoriasProdutosRepository = new BaseRepository<CategoriasProdutos>(context);
                }
                return categoriasProdutosRepository;
            }
        }

        private BaseRepository<CategoriaImagens> categoriaImagensRepository;
        public BaseRepository<CategoriaImagens> CategoriaImagensRepository
        {
            get
            {
                if (this.categoriaImagensRepository == null)
                {
                    this.categoriaImagensRepository = new BaseRepository<CategoriaImagens>(context);
                }
                return categoriaImagensRepository;
            }
        }

        private BaseRepository<Produtos> produtosRepository;
        public BaseRepository<Produtos> ProdutosRepository
        {
            get
            {
                if (this.produtosRepository == null)
                {
                    this.produtosRepository = new BaseRepository<Produtos>(context);
                }
                return produtosRepository;
            }
        }


        private BaseRepository<ProdutosImagens> produtoImagensRepository;
        public BaseRepository<ProdutosImagens> ProdutoImagensRepository
        {
            get
            {
                if (this.produtoImagensRepository == null)
                {
                    this.produtoImagensRepository = new BaseRepository<ProdutosImagens>(context);
                }
                return produtoImagensRepository;
            }
        }

        private BaseRepository<CaracteristicasProduto> caracteristicasProdutoRepository;
        public BaseRepository<CaracteristicasProduto> CaracteristicasProdutoRepository
        {
            get
            {
                if (this.caracteristicasProdutoRepository == null)
                {
                    this.caracteristicasProdutoRepository = new BaseRepository<CaracteristicasProduto>(context);
                }
                return caracteristicasProdutoRepository;
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
    }
}
