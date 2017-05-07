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
                Produto produto = new Produto();
                produto.Ativo = true;
                produto.Descricao = "Produto teste para leilão";
                produto.Nome = "Produto teste 1";
                produto.ProdutosImagens = ListarImagens();
                produto.Caracteristicas = ListarCaracteristicas();

                Usuario usuario = UsuarioBusiness.New.Carregar(1);

                ProdutoBusiness.New.Salvar(produto, usuario);
                Assert.IsTrue(true, "Operação executada com sucesso", produto);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ocorreu um erro ao tentar inserir o produto. Erro = {ex}");
            }
        }

        [TestMethod]
        public void Alterar()
        {
            try
            {
                Produto produto = new Produto();
                produto.ID = 1;
                produto.Ativo = true;
                produto.Descricao = "Produto teste para leilão - alteração";
                produto.Nome = "Produto teste 1 - alterado";
                produto.ProdutosImagens = ListarImagens();
                produto.Caracteristicas = ListarCaracteristicas();
                Usuario usuario = UsuarioBusiness.New.Carregar(1);

                ProdutoBusiness.New.Salvar(produto, usuario);
                Assert.IsTrue(true, "Operação executada com sucesso", produto);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ocorreu um erro ao tentar alterar o produto. Erro = {ex}");
            }
        }


        private List<CaracteristicaProduto> ListarCaracteristicas()
        {
            List<CaracteristicaProduto> lista = new List<CaracteristicaProduto>();
            lista.Add(new CaracteristicaProduto { Nome = "Produto de plásticio" });
            lista.Add(new CaracteristicaProduto { Nome = "Elétrico" });
            lista.Add(new CaracteristicaProduto { Nome = "Cor branca" });
            return lista;
        }

        public System.Collections.Generic.List<ProdutoImagem> ListarImagens()
        {
            List<ProdutoImagem> lista = new List<ProdutoImagem>();
            lista.Add(new ProdutoImagem
            {
                Imagem = new Imagem
                {
                    DataCadastro = DateTime.Now,
                    NomeArquivo = "Arquivo 1.jpg",
                    TamanhoBytes = 34343,
                    Tipo = "JPG",
                    DiretorioFisico = @"c:\"
                }
            });

            lista.Add(new ProdutoImagem
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
