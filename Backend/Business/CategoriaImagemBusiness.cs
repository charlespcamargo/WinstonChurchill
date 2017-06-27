using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    public class CategoriaImagemBusiness
    {
        Expression<Func<CategoriaImagem, bool>> predicate;
        public static CategoriaImagemBusiness New
        {
            get
            {
                return new CategoriaImagemBusiness();
            }
        }

        public CategoriaImagem Salvar(byte[] data, HttpPostedFile arquivo, int idCategoria)
        {
            CategoriaImagem categoriaImagem = new CategoriaImagem();
            categoriaImagem.CategoriaID = idCategoria;
            categoriaImagem.Imagem = new Imagem();
            categoriaImagem.Imagem.DataCadastro = DateTime.Now;
            categoriaImagem.Imagem.DiretorioFisico = "/Categoria";
            categoriaImagem.Imagem.NomeArquivo = arquivo.FileName;
            categoriaImagem.Imagem.TamanhoBytes = arquivo.ContentLength;
            categoriaImagem.Imagem.Tipo = arquivo.ContentType;

            AnexoBusiness.New.GravarArquivoFisico<Imagem>(categoriaImagem.Imagem, data);
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Categoria categoria = uow.CategoriaRepository.Carregar(p => p.ID == idCategoria, ord => ord.OrderBy(p => p.ID));
                    categoriaImagem.Imagem.UsuarioID = categoria.UsuarioID;
                    uow.CategoriaImagemRepository.Inserir(categoriaImagem);
                    uow.Save();
                }
            }
            catch
            {
                AnexoBusiness.New.ExcluirArquivoFisico<Imagem>(categoriaImagem.Imagem);
            }
            return categoriaImagem;
        }

        public List<CategoriaImagem> Carregar(CategoriaImagem filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                List<CategoriaImagem> lista = uow.CategoriaImagemRepository.Listar(predicate, ord => ord.OrderBy(p => p.ID), "Imagem");
                return lista;
            }
        }

        public void Excluir(CategoriaImagem filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                CategoriaImagem objCategoriaImagem = uow.CategoriaImagemRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID));
                if (objCategoriaImagem == null)
                    throw new ArgumentException("Imagem não encontrada!");

                uow.CategoriaImagemRepository.Excluir(objCategoriaImagem);
                Imagem objImagem = uow.ImagemRepository.Carregar(p => p.ID == objCategoriaImagem.ImagemID, ord => ord.OrderBy(p => p.ID));
                uow.ImagemRepository.Excluir(objImagem);
                uow.Save();

                AnexoBusiness.New.ExcluirArquivoFisico<Imagem>(objImagem);
            }
        }

        private void MontarFiltro(CategoriaImagem filtro)
        {
            predicate = UtilEntity.True<CategoriaImagem>();

            if (filtro.ID != 0)
                predicate = predicate.And(p => p.ID == filtro.ID);

            if (filtro.CategoriaID > 0)
                predicate = predicate.And(p => p.CategoriaID == filtro.CategoriaID);

        }
    }
}