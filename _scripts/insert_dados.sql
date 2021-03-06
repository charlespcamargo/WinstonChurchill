

	DELETE FROM UsuarioxGrupoUsuario WHERE ID > 0;
	DELETE FROM CaracteristicaProduto WHERE ID > 0;
    DELETE FROM CategoriaProduto WHERE ID > 0;
	DELETE FROM GrupoCategoria WHERE ID > 0;
    DELETE FROM Categoria WHERE ID > 0;
    DELETE FROM CompradorProduto WHERE ID > 0;
    DELETE FROM FornecedorProduto WHERE ID > 0;
    DELETE FROM LeilaoComprador WHERE ID > 0;
    DELETE FROM LeilaoFornecedorRodada WHERE ID > 0;
    DELETE FROM LeilaoFornecedor WHERE ID > 0;
    DELETE FROM LeilaoRodada WHERE ID > 0;
    DELETE FROM Leilao WHERE ID > 0;
    DELETE FROM Produto WHERE ID > 0;
    DELETE FROM Contato WHERE ID > 0;
    DELETE FROM ParceiroNegocioGrupo WHERE ID > 0;
    DELETE FROM ParceiroNegocioUsuario WHERE ID > 0;       
    DELETE FROM ParceiroNegocio WHERE ID > 0;       
    DELETE FROM Endereco WHERE ID > 0;
    DELETE FROM Grupo WHERE ID > 0;
	DELETE FROM Usuario WHERE ID > 0;
       
	ALTER TABLE UsuarioxGrupoUsuario AUTO_INCREMENT = 1;
	ALTER TABLE CaracteristicaProduto AUTO_INCREMENT = 1;
	ALTER TABLE CategoriaProduto AUTO_INCREMENT = 1;
	ALTER TABLE GrupoCategoria AUTO_INCREMENT = 1;
	ALTER TABLE Categoria AUTO_INCREMENT = 1;
	ALTER TABLE CompradorProduto AUTO_INCREMENT = 1;
	ALTER TABLE FornecedorProduto AUTO_INCREMENT = 1;
	ALTER TABLE LeilaoComprador AUTO_INCREMENT = 1;
    ALTER TABLE LeilaoFornecedorRodada AUTO_INCREMENT = 1;
	ALTER TABLE LeilaoFornecedor AUTO_INCREMENT = 1;
	ALTER TABLE LeilaoRodada AUTO_INCREMENT = 1;
	ALTER TABLE Leilao AUTO_INCREMENT = 1;
	ALTER TABLE Produto AUTO_INCREMENT = 1;
	ALTER TABLE Contato AUTO_INCREMENT = 1;
	ALTER TABLE ParceiroNegocioGrupo AUTO_INCREMENT = 1;
	ALTER TABLE ParceiroNegocioUsuario AUTO_INCREMENT = 1;       
	ALTER TABLE ParceiroNegocio AUTO_INCREMENT = 1;       
	ALTER TABLE Endereco AUTO_INCREMENT = 1;
	ALTER TABLE Grupo AUTO_INCREMENT = 1;
	ALTER TABLE Usuario AUTO_INCREMENT = 1;
    

INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Super Usuário 2', 'superusuario@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');

INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Adm 1', 'adm1@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Adm 2', 'adm2@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');

INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Representante Comercial 1', 'representantecomercial1@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Representante Comercial 2', 'representantecomercial2@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Representante Comercial 3', 'representantecomercial3@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');

INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Comprador 1', 'comprador1@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Comprador 2', 'comprador2@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Comprador 3', 'comprador3@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Comprador 4', 'comprador4@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Comprador 5', 'comprador5@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Comprador 6', 'comprador6@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Comprador 7', 'comprador7@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');

INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Fornecedor 1', 'fornecedor1@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Fornecedor 2', 'fornecedor2@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Fornecedor 3', 'fornecedor3@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Fornecedor 4', 'fornecedor4@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Fornecedor 5', 'fornecedor5@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Fornecedor 6', 'fornecedor6@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Fornecedor 7', 'fornecedor7@gmail.com', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');


INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Super Usuário', 'eduardo@v3x.com.br', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Administrador', 'admin@v3x.com.br', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Fornecedor WebRebate', 'fornecedor@v3x.com.br', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Comprador WebRebate', 'comprador@v3x.com.br', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');
INSERT INTO Usuario(Nome, Email, Ativo, DataCadastro, Senha) VALUES('Representante Comercial WebRebate', 'representantecomercial@v3x.com.br', 1, Now(), '3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2');

                       
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(1, 1000, 1, Now(), 2);

INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(2, 1001, 1, Now(), 2);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(3, 1001, 1, Now(), 2);

INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(4, 3, 1, Now(), 3);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(5, 3, 1, Now(), 3);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(6, 3, 1, Now(), 3);

INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(07, 2, 1, Now(), 22);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(08, 2, 1, Now(), 22);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(09, 2, 1, Now(), 22);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(10, 2, 1, Now(), 2);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(11, 2, 1, Now(), 2);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(12, 2, 1, Now(), 1);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(13, 2, 1, Now(), 1);


INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(14, 1, 1, Now(), 2);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(15, 1, 1, Now(), 2);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(16, 1, 1, Now(), 2);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(17, 1, 1, Now(), 2);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(18, 1, 1, Now(), 3);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(19, 1, 1, Now(), 3);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(20, 1, 1, Now(), 3);

-- Web Rebate
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(21, 1000, 1, Now(), 2);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(22, 1001, 1, Now(), 2);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(23, 1, 1, Now(), 2);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(24, 2, 1, Now(), 2);
INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(25, 3, 1, Now(), 2);





INSERT INTO Categoria(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Categoria A', 'Categoria A blá, blá, blá', 1, Now(), 2);
INSERT INTO Categoria(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Categoria B', 'Categoria B blá, blá, blá', 1, Now(), 2);
INSERT INTO Categoria(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Categoria C', 'Categoria C blá, blá, blá', 1, Now(), 2);
INSERT INTO Categoria(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Categoria D', 'Categoria D blá, blá, blá', 1, Now(), 2);
INSERT INTO Categoria(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Categoria E', 'Categoria E blá, blá, blá', 1, Now(), 2);


INSERT INTO Produto(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Farinha Branca', 'A Farinha Branca blá, blá, blá', 1, Now(), 2);
INSERT INTO Produto(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Farinha Escura', 'A Farinha Escura blá, blá, blá', 1, Now(), 2);
INSERT INTO Produto(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Trigo', 'O Trigo blá, blá, blá', 1, Now(), 2);
INSERT INTO Produto(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Pão Fracês', 'O pão', 1, Now(), 2);
INSERT INTO Produto(Nome, Descricao, Ativo, DataCadastro, UsuarioID) VALUES('Baguete', 'O pão', 1, Now(), 2);

INSERT INTO CaracteristicaProduto(Nome, ProdutoID) VALUES ('Branca', 1);
INSERT INTO CaracteristicaProduto(Nome, ProdutoID) VALUES ('Escura', 2);
INSERT INTO CaracteristicaProduto(Nome, ProdutoID) VALUES ('50g', 3);
INSERT INTO CaracteristicaProduto(Nome, ProdutoID) VALUES ('110g', 4);


INSERT INTO CategoriaProduto (CategoriaID, ProdutoID) VALUES (1, 1);
INSERT INTO CategoriaProduto (CategoriaID, ProdutoID) VALUES (2, 1);
INSERT INTO CategoriaProduto (CategoriaID, ProdutoID) VALUES (3, 1);
INSERT INTO CategoriaProduto (CategoriaID, ProdutoID) VALUES (4, 2);
INSERT INTO CategoriaProduto (CategoriaID, ProdutoID) VALUES (5, 3);


INSERT INTO Endereco (Logradouro, Bairro, Cidade, Estado, CEP) VALUES ('Celso Miguel dos Santos','Vossoroca','Votorantim','SP','18.116-000');
INSERT INTO Endereco (Logradouro, Bairro, Cidade, Estado, CEP) VALUES ('Antônio Carlos Comitre 1', 'Parque Campolim', 'Sorocaba', 'SP', '18.047-620');
INSERT INTO Endereco (Logradouro, Bairro, Cidade, Estado, CEP) VALUES ('Antônio Carlos Comitre 2', 'Parque Campolim', 'Sorocaba', 'SP', '18.047-620');
INSERT INTO Endereco (Logradouro, Bairro, Cidade, Estado, CEP) VALUES ('Antônio Carlos Comitre 3', 'Parque Campolim', 'Sorocaba', 'SP', '18.047-620');



INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('45.138.277/0001-13', 'Compradora A','Compradora A', '(15)3333-3333', '(15)99999-9999', 'contato@empresaA.com.br',  1, 3, 1, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Comprador A', 'contato@comprador-a.com.br', '(15)9999-9999', 1);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (1, 7, 1, Now());


INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('57.850.568/0001-19', 'Compradora B','Compradora B', '(15)3333-3333', '(15)99999-9999', 'contato@empresa-b.com.br',  1, 3, 1, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Comprador B', 'contato@comprador-b.com.br', '(15)9999-9999', 2);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (2, 8, 1, Now());


INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('01.442.356/0001-48', 'Compradora C','Compradora C', '(15)3333-3333', '(15)99999-9999', 'contato@empresa-c.com.br',  1, 3, 1, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Comprador C', 'contato@comprador-c.com.br', '(15)9999-9999', 3);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (3, 9, 1, Now());

INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('85.885.829/0001-71', 'Compradora D','Compradora D', '(15)3333-3333', '(15)99999-9999', 'contato@empresa-d.com.br',  1, 2, 1, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Comprador D', 'contato@comprador-d.com.br', '(15)9999-9999', 4);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (4, 10, 1, Now());

INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('85.885.829/0001-71', 'Compradora E','Compradora E', '(15)3333-3333', '(15)99999-9999', 'contato@empresa-e.com.br',  1, 2, 1, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Comprador E', 'contato@comprador-e.com.br', '(15)9999-9999', 5);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (5, 11, 1, Now());

INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('62.753.968/0001-46', 'Compradora F','Compradora F', '(15)3333-3333', '(15)99999-9999', 'contato@empresa-f.com.br',  1, 4, 1, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Comprador F', 'contato@comprador-f.com.br', '(15)9999-9999', 6);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (6, 12, 1, Now());

INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('63.413.446/0001-68', 'Compradora G','Compradora G', '(15)3333-3333', '(15)99999-9999', 'contato@empresa-g.com.br',  1, 4, 1, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Comprador G', 'contato@comprador-G.com.br', '(15)9999-9999', 7);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (7, 13, 1, Now());



INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('06.257.727/0001-35', 'Fornecedor A','Fornecedor A', '(15)3333-3333', '(15)99999-9999', 'contato@fornecedorA.com.br',  1, 3, 2, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Fornecedor A', 'contato@fornecedor-a.com.br', '(15)9999-9999', 8);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (8, 14, 1, Now());

INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('74.068.244/0001-42', 'Fornecedor B','Fornecedor B', '(15)3333-3333', '(15)99999-9999', 'contato@fornecedorB.com.br',  1, 3, 2, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Fornecedor B', 'contato@fornecedor-b.com.br', '(15)9999-9999', 9);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (9, 15, 1, Now());

INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('54.388.337/0001-47', 'Fornecedor C','Fornecedor C', '(15)3333-3333', '(15)99999-9999', 'contato@fornecedorC.com.br',  1, 4, 2, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Fornecedor C', 'contato@fornecedor-c.com.br', '(15)9999-9999', 10);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (10, 16, 1, Now());

INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('21.305.353/0001-70', 'Fornecedor D','Fornecedor D', '(15)3333-3333', '(15)99999-9999', 'contato@fornecedorD.com.br',  1, 4, 2, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Fornecedor D', 'contato@fornecedor-d.com.br', '(15)9999-9999', 11);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (11, 17, 1, Now());

INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('31.231.806/0001-14', 'Fornecedor E','Fornecedor E', '(15)3333-3333', '(15)99999-9999', 'contato@fornecedorE.com.br',  1, 5, 2, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Fornecedor E', 'contato@fornecedor-e.com.br', '(15)9999-9999', 12);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (12, 18, 1, Now());

INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('99.020.403/0001-60', 'Fornecedor F','Fornecedor F', '(15)3333-3333', '(15)99999-9999', 'contato@fornecedorF.com.br',  1, 5, 2, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Fornecedor F', 'contato@fornecedor-f.com.br', '(15)9999-9999', 13);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (13, 19, 1, Now());

INSERT INTO ParceiroNegocio(CNPJ, RazaoSocial, NomeFantasia, Telefone, Celular, Email, EnderecoID, UsuarioID, TipoParceiro, DataCadastro)
VALUES ('68.175.536/0001-81', 'Fornecedor G','Fornecedor G', '(15)3333-3333', '(15)99999-9999', 'contato@fornecedorG.com.br',  1, 25, 2, Now());
INSERT INTO Contato (Nome, Email, Telefone, ParceiroID) 
VALUES ('Contato Fornecedor G', 'contato@fornecedor-g.com.br', '(15)9999-9999', 14);
INSERT INTO ParceiroNegocioUsuario(ParceiroNegocioID, UsuarioID, CriadorID, DataCadastro) VALUES (14, 20, 1, Now());




INSERT INTO Grupo(Nome, TipoGrupo, UsuarioID) VALUES ('Grupo Compradores A', 1, 4);
INSERT INTO Grupo(Nome, TipoGrupo, UsuarioID) VALUES ('Grupo Compradores B', 1, 4);
INSERT INTO Grupo(Nome, TipoGrupo, UsuarioID) VALUES ('Grupo Compradores B', 1, 5);

INSERT INTO Grupo(Nome, TipoGrupo, UsuarioID) VALUES ('Grupo Fornecedores A', 2, 6);
INSERT INTO Grupo(Nome, TipoGrupo, UsuarioID) VALUES ('Grupo Fornecedores B', 2, 6);
INSERT INTO Grupo(Nome, TipoGrupo, UsuarioID) VALUES ('Grupo Fornecedores C', 2, 4);

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


INSERT INTO CompradorProduto(ParceiroID, ProdutoID, ValorMedioCompra, Quantidade, Frequencia) VALUES (1, 1, 320.10, 600, 3);
INSERT INTO FornecedorProduto(Valor, Volume, CapacidadeMaxima, ParceiroID, ProdutoID) VALUES (100.00, 32, 1100, 2, 1);





/* ---------------------- LEILAO ---------------------- */
INSERT INTO Leilao (Nome, ProdutoID, DataFinalFormacao, QtdDesejada, DataAbertura, RodadasLeilao, DiasCadaRodada, CriadorID, RepresentanteID, Ativo, MargemGarantiaPreco, SegundaMargemGarantiaPreco)
			VALUES ('Leilão de Trigo', 3, DATE_ADD(Now(), INTERVAL -3 DAY), 6350.00, DATE_ADD(Now(), INTERVAL -1 DAY), 2, 1, 5, 5, 1, 10.00, 20.00);

INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) 		  VALUES (1, 1, 1, 150);
INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) 		  VALUES (1, 2, 1, 300);
INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) 		  VALUES (1, 3, 1,  50);
INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) 		  VALUES (1, 4, 1, 650);
INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) 		  VALUES (1, 5, 1, 1200);
INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) 		  VALUES (1, 6, 1, 3350);
INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) 		  VALUES (1, 7, 1,  500);

INSERT INTO LeilaoFornecedor(LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima) VALUES (1, 8,  1,  50,  300);
INSERT INTO LeilaoFornecedor(LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima) VALUES (1, 9,  1, 100, 1500);
INSERT INTO LeilaoFornecedor(LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima) VALUES (1, 10, 1, 200, 1000);
INSERT INTO LeilaoFornecedor(LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima) VALUES (1, 11, 1,  75, 1200);
INSERT INTO LeilaoFornecedor(LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima) VALUES (1, 12, 1, 1000, 10000);
INSERT INTO LeilaoFornecedor(LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima) VALUES (1, 13, 1, 200, 3000);
INSERT INTO LeilaoFornecedor(LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima) VALUES (1, 14, 1, 100, 800);

INSERT INTO LeilaoRodada (LeilaoID, Numero, DataEncerramento) VALUES (1, 1, DATE_ADD(Now(), INTERVAL -2 + 2 DAY));
INSERT INTO LeilaoRodada (LeilaoID, Numero, DataEncerramento) VALUES (1, 2, DATE_ADD(Now(), INTERVAL -2 + 4 DAY));


INSERT INTO LeilaoFornecedorRodada(LeilaoFornecedorID, LeilaoRodadaID, ValorPrimeiraMargem, ValorSegundaMargem, DataLance) 
							VALUES(1, 				   1, 			   12700, 				13000, 					now());
INSERT INTO LeilaoFornecedorRodada(LeilaoFornecedorID, LeilaoRodadaID, ValorPrimeiraMargem, ValorSegundaMargem, DataLance) 
							VALUES(2, 				   1, 			   12500, 				13500, 					now());
INSERT INTO LeilaoFornecedorRodada(LeilaoFornecedorID, LeilaoRodadaID, ValorPrimeiraMargem, ValorSegundaMargem, DataLance) 
							VALUES(3, 				   1, 			   12000, 				12000, 					now());
INSERT INTO LeilaoFornecedorRodada(LeilaoFornecedorID, LeilaoRodadaID, ValorPrimeiraMargem, ValorSegundaMargem, DataLance) 
							VALUES(4, 				   1, 			   15000, 				16000, 					now());
INSERT INTO LeilaoFornecedorRodada(LeilaoFornecedorID, LeilaoRodadaID, ValorPrimeiraMargem, ValorSegundaMargem, DataLance) 
							VALUES(5, 				   1, 			   14000, 				14200, 					now());
INSERT INTO LeilaoFornecedorRodada(LeilaoFornecedorID, LeilaoRodadaID, ValorPrimeiraMargem, ValorSegundaMargem, DataLance) 
							VALUES(6, 				   1, 			    9500, 				10000, 					now());
                            
INSERT INTO LeilaoFornecedorRodada(LeilaoFornecedorID, LeilaoRodadaID, ValorPrimeiraMargem, ValorSegundaMargem, DataLance) 
							VALUES(3, 				   2, 			   11500, 				11500, 					DATE_ADD(Now(), INTERVAL 2 DAY));
INSERT INTO LeilaoFornecedorRodada(LeilaoFornecedorID, LeilaoRodadaID, ValorPrimeiraMargem, ValorSegundaMargem, DataLance) 
							VALUES(2, 				   2, 			   11500, 				12000, 					DATE_ADD(Now(), INTERVAL 2 DAY));
INSERT INTO LeilaoFornecedorRodada(LeilaoFornecedorID, LeilaoRodadaID, ValorPrimeiraMargem, ValorSegundaMargem, DataLance) 
							VALUES(6, 				   2, 			   9400, 				10000, 					DATE_ADD(Now(), INTERVAL 2 DAY));
/* ---------------------- LEILAO ---------------------- */





/* ---------------------- LEILAO ---------------------- */
INSERT INTO Leilao (Nome, ProdutoID, DataFinalFormacao, QtdDesejada, DataAbertura, RodadasLeilao, DiasCadaRodada, CriadorID, RepresentanteID, Ativo, MargemGarantiaPreco, SegundaMargemGarantiaPreco)
			VALUES ('Leilão de Farinha Escura', 2, DATE_ADD(Now(), INTERVAL -1 DAY), 950.00, DATE_ADD(Now(), INTERVAL 4 DAY), 2, 1, 5, 5, 1, 10.00, 20.00);

INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) 		  VALUES (2, 1, 1, 150);
INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) 		  VALUES (2, 6, 0, 300);
INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) 		  VALUES (2, 7, 1, 500);

INSERT INTO LeilaoFornecedor(LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima) VALUES (2, 8,  1,   50, 1000);
INSERT INTO LeilaoFornecedor(LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima) VALUES (2, 10, 0, 300, 1500);
INSERT INTO LeilaoFornecedor(LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima) VALUES (2, 11, 1, 500, 5000);

INSERT INTO LeilaoRodada (LeilaoID, Numero, DataEncerramento) VALUES (2, 1, DATE_ADD(Now(), INTERVAL 4 + 2 DAY));
INSERT INTO LeilaoRodada (LeilaoID, Numero, DataEncerramento) VALUES (2, 2, DATE_ADD(Now(), INTERVAL 4 + 4 DAY));
/* ---------------------- LEILAO ---------------------- */



/* ---------------------- LEILAO ---------------------- */
INSERT INTO Leilao (Nome, ProdutoID, DataFinalFormacao, QtdDesejada, DataAbertura, RodadasLeilao, DiasCadaRodada, CriadorID, RepresentanteID, Ativo, MargemGarantiaPreco, SegundaMargemGarantiaPreco)
			VALUES ('Farinha Branca', 1, DATE_ADD(Now(), INTERVAL -1 DAY), 400.00, DATE_ADD(Now(), INTERVAL 2 DAY), 2, 1, 3, 4, 1, 10.00, 20.00);

INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) 		  VALUES (3, 1, 1, 200);
INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) 		  VALUES (3, 6, 0, 130);
INSERT INTO LeilaoComprador (LeilaoID, ParceiroNegocioID, Participando, QtdDesejada) 		  VALUES (3, 7, 1,  70);

INSERT INTO LeilaoFornecedor(LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima) VALUES (3, 8, 1,  100, 3000);
INSERT INTO LeilaoFornecedor(LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima) VALUES (3, 10, 0, 300, 3000);
INSERT INTO LeilaoFornecedor(LeilaoID,ParceiroNegocioID, Participando, QtdMinima, QtdMaxima) VALUES (3, 11, 1, 100, 3000);

INSERT INTO LeilaoRodada (LeilaoID, Numero, DataEncerramento) VALUES (3, 1, DATE_ADD(Now(), INTERVAL 2 + 2 DAY));
INSERT INTO LeilaoRodada (LeilaoID, Numero, DataEncerramento) VALUES (3, 2, DATE_ADD(Now(), INTERVAL 2 + 4 DAY));
/* ---------------------- LEILAO ---------------------- */

                      
                      
                      
                      
                      select * from LeilaoRodada
                      
                      
                      
                      