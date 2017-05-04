using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinstonChurchill.Backend.Business;
using WinstonChurchill.Backend.Model;
using System.Collections.Generic;

namespace WinstonChurchill.BackEnd.Test
{
    [TestClass]
    public class UnitTestProduto
    {
        [TestMethod]
        public void Inserir()
        {
            try
            {
                Produtos produto = new Produtos();
                produto.Ativo = true;
                produto.Descricao = "Produto teste para leilão";
                produto.Nome = "Produto teste 1";
                produto.Imagens = ListarImagens();
                produto.Caracteristicas = new List<CaracteristicasProduto>();

                Usuario usuario = UsuarioBusiness.New.Carregar(1);

                ProdutoBusiness.New.Salvar(produto, usuario);
                Assert.IsTrue(true, "Operação executada com sucesso", produto);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ocorreu um erro ao tentar inserir o produto. Erro = {ex}");
            }
        }

        public System.Collections.Generic.List<ProdutosImagens> ListarImagens()
        {
            List<ProdutosImagens> lista = new List<ProdutosImagens>();
            lista.Add(new ProdutosImagens
            {
                Imagem = new Imagem
                {
                    DataCadastro = DateTime.Now,
                    NomeArquivo = "Arquivo 1.jpg",
                    TamanhoBytes = 34343,
                    Tipo = "JPG",
                    DiretorioFisico= @"c:\"
                }
            });

            lista.Add(new ProdutosImagens
            {
                Imagem = new Imagem
                {
                    DataCadastro = DateTime.Now,
                    NomeArquivo = "Arquivo 2.jpg",
                    TamanhoBytes = 34343,
                    Tipo = "JPG",
                    DiretorioFisico = @"c:\"
                }
            });
            return lista;
        }
    }
}
