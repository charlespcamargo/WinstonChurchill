

INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Manuela Camargo', 'manuela@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) 
                           VALUES(2, 3, 1, Now(), 1);

INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Mariana Camargo', 'mariana@gmail.com', 0, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');


INSERT INTO Categoria(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Categoria A', 'Categoria A blá, blá, blá', 1, Now(), 1);
INSERT INTO Categoria(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Categoria B', 'Categoria B blá, blá, blá', 1, Now(), 1);
INSERT INTO Categoria(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Categoria C', 'Categoria C blá, blá, blá', 1, Now(), 1);
INSERT INTO Categoria(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Categoria D', 'Categoria D blá, blá, blá', 1, Now(), 1);
INSERT INTO Categoria(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Categoria E', 'Categoria E blá, blá, blá', 1, Now(), 1);


INSERT INTO Produto(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Farinha Branca', 'A Farinha Branca blá, blá, blá', 1, Now(), 1);
INSERT INTO Produto(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Farinha Escura', 'A Farinha Escura blá, blá, blá', 1, Now(), 1);
INSERT INTO Produto(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Trigo', 'O Trigo blá, blá, blá', 1, Now(), 1);
INSERT INTO Produto(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Pão Fracês', 'O pão', 1, Now(), 1);
INSERT INTO Produto(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Baguete', 'O pão', 1, Now(), 1);

INSERT INTO CaracteristicaProduto(Nome, ProdutoID) VALUES ('Branca', 1);
INSERT INTO CaracteristicaProduto(Nome, ProdutoID) VALUES ('Escura', 2);
INSERT INTO CaracteristicaProduto(Nome, ProdutoID) VALUES ('50g', 4);
INSERT INTO CaracteristicaProduto(Nome, ProdutoID) VALUES ('110g', 5);


INSERT INTO CategoriaProduto (CategoriaID, ProdutoID) VALUES (1, 1);
INSERT INTO CategoriaProduto (CategoriaID, ProdutoID) VALUES (2, 1);
INSERT INTO CategoriaProduto (CategoriaID, ProdutoID) VALUES (3, 1);
INSERT INTO CategoriaProduto (CategoriaID, ProdutoID) VALUES (4, 2);
INSERT INTO CategoriaProduto (CategoriaID, ProdutoID) VALUES (5, 3);


INSERT INTO Endereco (Logradouro, Bairro, Cidade, Estado, CEP) VALUES ('Celso Miguel dos Santos','Vossoroca','Votorantim','SP','18.116-000');
INSERT INTO Endereco (Logradouro, Bairro, Cidade, Estado, CEP) VALUES ('Antônio Carlos Comitre', 'Parque Campolim', 'Sorocaba', 'SP', '18.047-620');

INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('45.138.277/0001-13', 'Compradora A Desenvolvimento de Sistemas','Empresa A', '(15)3333-3333', '(15)99999-9999', 'contato@empresaA.com.br',  1, 1, 1, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) VALUES ('Contato Comprador', 'contato@compradora.com.br', '(15)9999-9999', 1);


INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('06.257.727/0001-35', 'Fornecedor A','Fornecedor A', '(15)3333-3333', '(15)99999-9999', 'contato@fornecedorA.com.br',  1, 1, 2, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) VALUES ('Contato Fornecedor', 'contato@fornecedor.com.br', '(15)9999-9999', 2);


INSERT INTO Grupo(Nome, TipoGrupo, UsuarioID) VALUES ('Grupo Compradores A', 1, 1);
INSERT INTO Grupo(Nome, TipoGrupo, UsuarioID) VALUES ('Grupo Compradores B', 1, 1);
INSERT INTO Grupo(Nome, TipoGrupo, UsuarioID) VALUES ('Grupo Fornecedores A', 2, 1);

INSERT INTO GrupoCategoria (GrupoID, CategoriaID) VALUES (1, 1);
INSERT INTO GrupoCategoria (GrupoID, CategoriaID) VALUES (1, 2);
INSERT INTO GrupoCategoria (GrupoID, CategoriaID) VALUES (2, 1);
INSERT INTO GrupoCategoria (GrupoID, CategoriaID) VALUES (2, 2);
	
INSERT INTO GrupoCategoria (GrupoID, CategoriaID) VALUES (3, 1);
INSERT INTO GrupoCategoria (GrupoID, CategoriaID) VALUES (3, 2);
INSERT INTO GrupoCategoria (GrupoID, CategoriaID) VALUES (3, 3);
INSERT INTO GrupoCategoria (GrupoID, CategoriaID) VALUES (3, 4);

INSERT INTO ParceiroNegocioGrupo (ParceiroID, GrupoID) VALUES (1, 1);
INSERT INTO ParceiroNegocioGrupo (ParceiroID, GrupoID) VALUES (1, 2);
INSERT INTO ParceiroNegocioGrupo (ParceiroID, GrupoID) VALUES (2, 3);


INSERT INTO CompradorProduto(ParceiroID, ProdutoID, ValorMedioCompra, Quantidade, Frequencia)
					 VALUES (1, 1, 320.10, 600, 3);
                     
                     
INSERT INTO FornecedorProduto(Valor, Volume, CapacidadeMaxima, ParceiroID, ProdutoID)
					  VALUES (100.00, 	 32, 			 1100, 		    2, 1);


INSERT INTO Leilao (Nome, ProdutoID, DataFinalFormacao, QtdDesejada, DataAbertura, CriadorID, RepresentanteID, Ativo)
			VALUES ('Leilão de Farinha Branca', 1, Now() + 3, 400.00, Now() + 5,  1, 2, 1);

INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) VALUES (1, 1, 1, 113);
INSERT INTO LeilaoFornecedor (LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima)
					  VALUES (1, 2, 1, 100, 3000);parceironegocio
                      
                       
                      
                                           
                      
                      
                      
                      
                      
                      
                      