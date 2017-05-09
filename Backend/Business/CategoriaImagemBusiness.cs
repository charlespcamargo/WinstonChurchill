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

        public CategoriaImagem Salvar(byte[] data, HttpPostedFile arquivo, Usuario usuario, int IdCategoria)
        {
            CategoriaImagem CategoriaImagem = new CategoriaImagem();
            CategoriaImagem.CategoriaID = IdCategoria;
            CategoriaImagem.Imagem = new Imagem();
            CategoriaImagem.Imagem.DataCadastro = DateTime.Now;
            CategoriaImagem.Imagem.DiretorioFisico = "/Categoria";
            CategoriaImagem.Imagem.NomeArquivo = arquivo.FileName;
            CategoriaImagem.Imagem.TamanhoBytes = arquivo.ContentLength;
            CategoriaImagem.Imagem.Tipo = arquivo.ContentType;
            CategoriaImagem.Imagem.UsuarioID = usuario.ID;

            AnexoBusiness.New.GravarArquivoFisico<Imagem>(CategoriaImagem.Imagem, data);
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.CategoriaImagemRepository.Inserir(CategoriaImagem);
                    uow.Save();
                }
            }
            catch
            {
                AnexoBusiness.New.ExcluirArquivoFisico<Imagem>(CategoriaImagem.Imagem);
            }
            return CategoriaImagem;
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

            predicate = predicate.And(p => p.Imagem.UsuarioID == filtro.Imagem.UsuarioID);
        }
    }
}