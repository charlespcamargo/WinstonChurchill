CREATE DATABASE IF NOT EXISTS dbWinstonChurchill;

USE dbWinstonChurchill;


/** CUIDADO - DROPA TUDO 
	
    DROP TABLE dbWinstonChurchill.CategoriaProduto;
	DROP TABLE dbWinstonChurchill.ProdutoImagem;
	DROP TABLE dbWinstonChurchill.CategoriaImagem;
	DROP TABLE dbWinstonChurchill.Imagem;
	DROP TABLE dbWinstonChurchill.CaracteristicaProduto;
	DROP TABLE dbWinstonChurchill.Categoria;
	DROP TABLE dbWinstonChurchill.Produto;
	DROP TABLE dbWinstonChurchill.UsuarioXGrupoUsuario;
	DROP TABLE dbWinstonChurchill.GrupoUsuario;
	DROP TABLE dbWinstonChurchill.Usuario;
    
CUIDADO - DROPA TUDO ***/

CREATE TABLE IF NOT EXISTS dbWinstonChurchill.Usuario
(
	ID						INT					NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Nome					VARCHAR(50)			NOT NULL,
    Email					VARCHAR(50)			NOT NULL,
    Ativo					BIT					NOT NULL,    
    DataCadastro			DATETIME			NOT NULL,
    Senha					VARCHAR(128)		NOT NULL
);


CREATE TABLE IF NOT EXISTS dbWinstonChurchill.GrupoUsuario
(
	ID						INT					NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Nome					VARCHAR(50)			NOT NULL,
    Descricao				VARCHAR(500)		NOT NULL,
    Ativo					BIT					NOT NULL,    
    DataCadastro			DATETIME			NOT NULL
);

CREATE TABLE IF NOT EXISTS dbWinstonChurchill.UsuarioXGrupoUsuario
(
	ID						INT					NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UsuarioID				INT					NOT NULL,
    GrupoUsuarioID			INT					NOT NULL,
    Ativo					BIT					NOT NULL,    
    DataCadastro			DATETIME			NOT NULL,
    ResponsavelID			INT					NOT NULL,
    
    FOREIGN KEY FK_UsuarioXGrupoUsuario_UsuarioID (UsuarioID) REFERENCES Usuario(ID),
    FOREIGN KEY FK_UsuarioXGrupoUsuario_GrupoUsuarioID (GrupoUsuarioID) REFERENCES GrupoUsuario(ID),
	CONSTRAINT UC_Usuario_Grupo UNIQUE (UsuarioID,GrupoUsuarioID)
);

 
CREATE TABLE IF NOT EXISTS dbWinstonChurchill.Categoria 
(
  ID 					INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Nome 					VARCHAR(50) NOT NULL,
  Descricao				VARCHAR(255) NULL,
  Ativo 				BIT NOT NULL,
  DataCadastro 			DATETIME NOT NULL,
  UsuarioID 			INT NULL,
  
  INDEX 		fk_Categoria_Usuario_idx (UsuarioID ASC),
  CONSTRAINT 	fk_Categoria_Usuario FOREIGN KEY (UsuarioID) REFERENCES dbWinstonChurchill.Usuario(ID)
);

CREATE TABLE IF NOT EXISTS dbWinstonChurchill.Produto 
(
  ID 			INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Nome 			VARCHAR(50) NOT NULL,
  Descricao 	VARCHAR(255) NULL,
  Ativo 		BIT NOT NULL,
  DataCadastro 	DATETIME NOT NULL,
  UsuarioID 	INT NOT NULL,
  
  
  INDEX fk_Produto_Usuario_idx (UsuarioID ASC),
	
  CONSTRAINT fk_Produto_Usuario FOREIGN KEY (UsuarioID) REFERENCES dbWinstonChurchill.Usuario(ID)
);


CREATE TABLE IF NOT EXISTS dbWinstonChurchill.CaracteristicaProduto 
(
  ID 			INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Nome 			VARCHAR(50) NOT NULL,
  ProdutoID 	INT NOT NULL,
  
  INDEX fk_CaracteristicaProduto_Produto_idx (ProdutoID ASC),
  CONSTRAINT fk_CaracteristicaProduto_Produto FOREIGN KEY (ProdutoID) REFERENCES dbWinstonChurchill.Produto(ID)
);


CREATE TABLE IF NOT EXISTS dbWinstonChurchill.Imagem 
(
  ID 					INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  NomeArquivo 			VARCHAR(255) NOT NULL,
  DiretorioFisico 		VARCHAR(255) NOT NULL,
  TamanhoBytes 			INT NOT NULL,
  DataCadastro 			VARCHAR(45) NOT NULL,
  UsuarioID 			INT NOT NULL,
  Tipo 					VARCHAR(45) NOT NULL,
  
	INDEX fk_Imagem_usuario_idx (UsuarioID ASC),
	CONSTRAINT fk_Imagem_usuario FOREIGN KEY (UsuarioID) REFERENCES dbWinstonChurchill.Usuario(ID)
);

CREATE TABLE IF NOT EXISTS dbWinstonChurchill.CategoriaImagem 
(
  ID 					INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  CategoriaID 			INT NOT NULL,
  ImagemID 				INT NOT NULL,
  
  
  INDEX fk_CategoriaImagem_Categoria_idx(CategoriaID ASC),
  INDEX fk_CategoriaImagem_Imagem_idx (ImagemID ASC),
  
  CONSTRAINT fk_CategoriaImagem_Categoria FOREIGN KEY (CategoriaID) REFERENCES dbWinstonChurchill.Categoria(ID),
  CONSTRAINT fk_CategoriaImagem_Imagem 	  FOREIGN KEY (ImagemID)    REFERENCES dbWinstonChurchill.Imagem(ID)
);

CREATE TABLE IF NOT EXISTS dbWinstonChurchill.ProdutoImagem
(
  ID 			INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  ProdutoID 	INT NOT NULL,
  ImagemID 		INT NOT NULL,
  
  INDEX fk_ProdutoImagem_Produto_idx(ProdutoID ASC),
  INDEX fk_ProdutoImagem_Imagem_idx(ImagemID ASC),
  
  CONSTRAINT fk_ProdutoImagem_Produto FOREIGN KEY(ProdutoID) REFERENCES dbWinstonChurchill.Produto(ID),
  CONSTRAINT fk_ProdutoImagem_Imagem FOREIGN KEY(ImagemID)  REFERENCES dbWinstonChurchill.Imagem(ID)
);

CREATE TABLE IF NOT EXISTS dbWinstonChurchill.CategoriaProduto
(
  ID 			INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  CategoriaID 	INT NOT NULL,
  ProdutoID 	INT NOT NULL,
  
  INDEX fk_CategoriaProduto_Categoria_idx (CategoriaID ASC),
  INDEX fk_CategoriaProduto_Produto_idx(ProdutoID ASC),
  
  CONSTRAINT fk_CategoriaProduto_Categoria FOREIGN KEY (CategoriaID) REFERENCES dbWinstonChurchill.Categoria(ID),  
  CONSTRAINT fk_CategoriaProduto_Produto FOREIGN KEY(ProdutoID) REFERENCES dbWinstonChurchill.Produto(ID)
);
 
 
 CREATE TABLE IF NOT EXISTS dbwinstonchurchill.Parametro (
  ID 							INT NOT NULL AUTO_INCREMENT	PRIMARY KEY,
  LimiteCancelCompra 			INT NOT NULL,
  PercLucroEmpresa 				DECIMAL(5,2) NOT NULL,
  PercLucroRepComercial 		DECIMAL(5,2) NOT NULL,
  RodasLeilao 					INT NOT NULL,
  DiasCadaRodada 				INT NOT NULL,
  MargemGarantiaPreco 			DECIMAL(5,2) NOT NULL,
  SegundaMargemGarantiaPreco 	DECIMAL(5,2) NOT NULL
  
 );


 