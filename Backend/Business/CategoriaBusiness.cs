using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;
using WinstonChurchill.Backend.Utils;

namespace WinstonChurchill.Backend.Business
{
    public class CategoriaBusiness
    {
        Expression<Func<Categoria, bool>> predicate;

        public static CategoriaBusiness New
        {
            get
            {
                return new CategoriaBusiness();
            }
        }

        public Categoria Salvar(Categoria entidade, Usuario usuario)
        {
            if (entidade == null)
                throw new ArgumentException("Erro: nenhuma informação foi gerado");

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (entidade.ID == 0)
                {
                    entidade.UsuarioID = usuario.ID;
                    entidade.DataCadastro = DateTime.Now;
                    uow.CategoriaRepository.Inserir(entidade);
                }
                else
                {
                    Categoria objSalvo = uow.CategoriaRepository.Carregar(p => p.ID == entidade.ID, ord => ord.OrderBy(p => p.ID));
                    if (objSalvo != null)
                    {
                        objSalvo.Descricao = entidade.Descricao;
                        objSalvo.Ativo = entidade.Ativo;
                        objSalvo.Nome = entidade.Nome;
                        objSalvo.UsuarioID = usuario.ID;
                        objSalvo.Descricao = entidade.Descricao;
                        uow.CategoriaRepository.Alterar(objSalvo);
                    }
                }

                uow.Save();
                return entidade;
            }
        }

        public List<Categoria> Listar(Categoria filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                return uow.CategoriaRepository.Listar(predicate).ToList();
            }

        }

        public Categoria Carregar(Categoria filtro)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                Categoria objeto = uow.CategoriaRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID));
                return objeto;
            }
        }

        public void Excluir(Categoria filtro)
        {
            if (filtro == null)
                throw new ArgumentException("Informe o Categoria para realizar a exclusão");

            using (UnitOfWork uow = new UnitOfWork())
            {
                MontarFiltro(filtro);
                Categoria CategoriaExcluir = uow.CategoriaRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID), "CategoriaImagem");
                if (CategoriaExcluir == null)
                    throw new ArgumentException("Nenhum Categoria encontrado");

                List<Imagem> listaImagens = new List<Imagem>();
                if (CategoriaExcluir.CategoriaImagem != null)
                {
                    foreach (var item in CategoriaExcluir.CategoriaImagem)
                    {
                        Imagem imagem = uow.ImagemRepository.Carregar(p => p.ID == item.ImagemID, ord => ord.OrderBy(p => p.ID));
                        if (imagem != null)
                        {
                            listaImagens.Add(imagem);
                        }
                    }
                }

                uow.CategoriaRepository.Excluir(CategoriaExcluir);

                if (listaImagens != null)
                {
                    foreach (var imagem in listaImagens)
                    {
                        uow.ImagemRepository.Excluir(imagem);
                        AnexoBusiness.New.ExcluirArquivoFisico<Imagem>(imagem);
                    }
                }

                uow.Save();
            }
        }

        private void MontarFiltro(Categoria filtro)
        {
            predicate = UtilEntity.True<Categoria>();

            if (!string.IsNullOrEmpty(filtro.Nome))
                predicate = predicate.And(p => p.Nome.Contains(filtro.Nome));

            if (filtro.Ativo.HasValue)
                predicate = predicate.And(p => p.Ativo == filtro.Ativo);

            if (filtro.ID > 0)
                predicate = predicate.And(p => p.ID == filtro.ID);

            predicate = predicate.And(p => p.UsuarioID == filtro.UsuarioID);
        }
    }
}
