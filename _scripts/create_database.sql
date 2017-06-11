-- CREATE DATABASE IF NOT EXISTS webrebate;

-- USE webrebate;


/** CUIDADO - DROPA TUDO 
	
    DROP TABLE webrebate.CategoriaProduto;
	DROP TABLE webrebate.ProdutoImagem;
	DROP TABLE webrebate.CategoriaImagem;
	DROP TABLE webrebate.Imagem;
	DROP TABLE webrebate.CaracteristicaProduto;
	DROP TABLE webrebate.Categoria;
	DROP TABLE webrebate.Produto;
	DROP TABLE webrebate.UsuarioXGrupoUsuario;
	DROP TABLE webrebate.GrupoUsuario;
	DROP TABLE webrebate.Usuario;
    
CUIDADO - DROPA TUDO ***/

CREATE TABLE  webrebate.Usuario
(
	ID						INT					NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Nome					VARCHAR(50)			NOT NULL,
    Email					VARCHAR(50)			NOT NULL,
    Ativo					BIT					NOT NULL,    
    DataCadastro			DATETIME			NOT NULL,
    Senha					VARCHAR(128)		NOT NULL
);


CREATE TABLE IF NOT EXISTS webrebate.GrupoUsuario
(
	ID						INT					NOT NULL PRIMARY KEY,
    Nome					VARCHAR(50)			NOT NULL,
    Descricao				VARCHAR(500)		NOT NULL,
    Ativo					BIT					NOT NULL,    
    DataCadastro			DATETIME			NOT NULL
);

CREATE TABLE IF NOT EXISTS webrebate.UsuarioXGrupoUsuario
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

 
CREATE TABLE IF NOT EXISTS webrebate.Categoria 
(
  ID 					INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Nome 					VARCHAR(50) NOT NULL,
  Descricao				VARCHAR(255) NULL,
  Ativo 				BIT NOT NULL,
  DataCadastro 			DATETIME NOT NULL,
  UsuarioID 			INT NULL,
  
  INDEX 		fk_Categoria_Usuario_idx (UsuarioID ASC),
  CONSTRAINT 	fk_Categoria_Usuario FOREIGN KEY (UsuarioID) REFERENCES webrebate.Usuario(ID)
);

CREATE TABLE IF NOT EXISTS webrebate.Produto 
(
  ID 			INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Nome 			VARCHAR(50) NOT NULL,
  Descricao 	VARCHAR(255) NULL,
  Ativo 		BIT NOT NULL,
  DataCadastro 	DATETIME NOT NULL,
  UsuarioID 	INT NOT NULL,
  
  
  INDEX fk_Produto_Usuario_idx (UsuarioID ASC),
	
  CONSTRAINT fk_Produto_Usuario FOREIGN KEY (UsuarioID) REFERENCES webrebate.Usuario(ID)
);


CREATE TABLE IF NOT EXISTS webrebate.CaracteristicaProduto 
(
  ID 			INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Nome 			VARCHAR(50) NOT NULL,
  ProdutoID 	INT NOT NULL,
  
  INDEX fk_CaracteristicaProduto_Produto_idx (ProdutoID ASC),
  CONSTRAINT fk_CaracteristicaProduto_Produto FOREIGN KEY (ProdutoID) REFERENCES webrebate.Produto(ID)
);


CREATE TABLE IF NOT EXISTS webrebate.Imagem 
(
  ID 					INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  NomeArquivo 			VARCHAR(255) NOT NULL,
  DiretorioFisico 		VARCHAR(255) NOT NULL,
  TamanhoBytes 			INT NOT NULL,
  DataCadastro 			VARCHAR(45) NOT NULL,
  UsuarioID 			INT NOT NULL,
  Tipo 					VARCHAR(45) NOT NULL,
  
	INDEX fk_Imagem_usuario_idx (UsuarioID ASC),
	CONSTRAINT fk_Imagem_usuario FOREIGN KEY (UsuarioID) REFERENCES webrebate.Usuario(ID)
);

CREATE TABLE IF NOT EXISTS webrebate.CategoriaImagem 
(
  ID 					INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  CategoriaID 			INT NOT NULL,
  ImagemID 				INT NOT NULL,
  
  
  INDEX fk_CategoriaImagem_Categoria_idx(CategoriaID ASC),
  INDEX fk_CategoriaImagem_Imagem_idx (ImagemID ASC),
  
  CONSTRAINT fk_CategoriaImagem_Categoria FOREIGN KEY (CategoriaID) REFERENCES webrebate.Categoria(ID),
  CONSTRAINT fk_CategoriaImagem_Imagem 	  FOREIGN KEY (ImagemID)    REFERENCES webrebate.Imagem(ID)
);

CREATE TABLE IF NOT EXISTS webrebate.ProdutoImagem
(
  ID 			INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  ProdutoID 	INT NOT NULL,
  ImagemID 		INT NOT NULL,
  
  INDEX fk_ProdutoImagem_Produto_idx(ProdutoID ASC),
  INDEX fk_ProdutoImagem_Imagem_idx(ImagemID ASC),
  
  CONSTRAINT fk_ProdutoImagem_Produto FOREIGN KEY(ProdutoID) REFERENCES webrebate.Produto(ID),
  CONSTRAINT fk_ProdutoImagem_Imagem FOREIGN KEY(ImagemID)  REFERENCES webrebate.Imagem(ID)
);

CREATE TABLE IF NOT EXISTS webrebate.CategoriaProduto
(
  ID 			INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  CategoriaID 	INT NOT NULL,
  ProdutoID 	INT NOT NULL,
  
  INDEX fk_CategoriaProduto_Categoria_idx (CategoriaID ASC),
  INDEX fk_CategoriaProduto_Produto_idx(ProdutoID ASC),
  
  CONSTRAINT fk_CategoriaProduto_Categoria FOREIGN KEY (CategoriaID) REFERENCES webrebate.Categoria(ID),  
  CONSTRAINT fk_CategoriaProduto_Produto FOREIGN KEY(ProdutoID) REFERENCES webrebate.Produto(ID)
);
 
 
 CREATE TABLE IF NOT EXISTS webrebate.Parametro (
  ID 							INT NOT NULL AUTO_INCREMENT	PRIMARY KEY,
  LimiteCancelCompra 			INT NOT NULL,
  PercLucroEmpresa 				DECIMAL(5,2) NOT NULL,
  PercLucroRepComercial 		DECIMAL(5,2) NOT NULL,
  RodasLeilao 					INT 		 NOT NULL,
  DiasCadaRodada 				INT 		 NOT NULL,
  MargemGarantiaPreco 			DECIMAL(5,2) NOT NULL,
  SegundaMargemGarantiaPreco 	DECIMAL(5,2) NOT NULL
  
 );
 
 
 CREATE TABLE IF NOT EXISTS webrebate.Endereco (
  ID 							INT 			NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Logradouro 					VARCHAR(150)  	NOT NULL,
  Bairro 						VARCHAR(50)   	NOT NULL,
  Cidade 						VARCHAR(50)   	NOT NULL,
  Estado 						VARCHAR(50)   	NOT NULL,
  CEP 							VARCHAR(11)   	NOT NULL
);
 

CREATE TABLE IF NOT EXISTS webrebate.ParceiroNegocio (
  ID 							INT 			NOT NULL AUTO_INCREMENT	PRIMARY KEY,
  CNPJ 							VARCHAR(18) 	NOT NULL,
  RazaoSocial 					VARCHAR(100) 	NOT NULL,
  NomeFantasia 					VARCHAR(100) 	NOT NULL,
  Telefone 						VARCHAR(14) 	NOT NULL,
  Celular 						VARCHAR(15) 	NOT NULL,
  Email 						VARCHAR(150) 	NOT NULL,
  EnderecoID 					INT 			NOT NULL,
  UsuarioID 					INT 			NOT NULL,
  TipoParceiro					INT				NOT NULL,
  DataCadastro					DATETIME		NOT NULL,
  INDEX fk_ParceiroNegocio_Usuario_idx (UsuarioID ASC),
  INDEX fk_ParceiroNegocio_Endereco_idx (EnderecoID ASC),
  CONSTRAINT fk_ParceiroNegocio_Usuario     FOREIGN KEY (UsuarioID)     REFERENCES webrebate.Usuario (ID), 
  CONSTRAINT fk_ParceiroNegocio_Endereco    FOREIGN KEY (EnderecoID)    REFERENCES webrebate.Endereco (ID)
);



CREATE TABLE IF NOT EXISTS webrebate.Grupo (
  ID 							INT 	    	NOT NULL AUTO_INCREMENT PRIMARY KEY ,
  Nome 							VARCHAR(100) 	NOT NULL,
  TipoGrupo						INT				NOT NULL,
  UsuarioID						INT				NOT NULL,
  INDEX fk_Grupo_Usuario_idx (UsuarioID ASC),
  CONSTRAINT fk_Grupo_Usuario     FOREIGN KEY (UsuarioID)     REFERENCES webrebate.Usuario (ID)
);


CREATE TABLE IF NOT EXISTS webrebate.ParceiroNegocioGrupo (
  ID 							INT 			NOT NULL AUTO_INCREMENT	PRIMARY KEY,
  ParceiroID 					INT 			NOT NULL,
  GrupoID 				INT 			NOT NULL,
  INDEX fk_ParceiroNegocio_Parceiro_idx (ParceiroID ASC),
  INDEX fk_ParceiroNegocio_Grupo_idx (GrupoID ASC),
  CONSTRAINT fk_ParceiroNegocio_Parceiro    	 FOREIGN KEY (ParceiroID)    	REFERENCES webrebate.ParceiroNegocio (ID),
  CONSTRAINT fk_ParceiroNegocio_Grupo     FOREIGN KEY (GrupoID)    REFERENCES webrebate.Grupo (ID)
  );

CREATE TABLE IF NOT EXISTS webrebate.GrupoCategoria (
  ID 							INT 			NOT NULL AUTO_INCREMENT PRIMARY KEY,
  GrupoID		 				INT				NOT NULL,
  CategoriaID 					INT				NOT NULL  ,
   INDEX fk_GrupoCategoria_Categoria_idx (CategoriaID ASC),
   INDEX fk_GrupoCategoria_Grupo_idx (GrupoID ASC),
   CONSTRAINT fk_GrupoCategoria_Grupo  	 	FOREIGN KEY (GrupoID)   	REFERENCES webrebate.Grupo (ID),
   CONSTRAINT fk_GrupoCategoria_Categoria    	 		FOREIGN KEY (CategoriaID)	    REFERENCES webrebate.Categoria (ID)
);


CREATE TABLE IF NOT EXISTS webrebate.CompradorProduto (
  ID 							INT 			NOT NULL AUTO_INCREMENT	PRIMARY KEY,
  ParceiroID 					INT 			NOT NULL,
  ProdutoID 					INT 			NOT NULL,
  ValorMedioCompra 				DECIMAL(12,2) 	NOT NULL,
  Quantidade 					INT 			NOT NULL,
  Frequencia 					INT 			NOT NULL,
  INDEX fk_CompradorProduto_Parceiro_idx (ParceiroID ASC),
  INDEX fk_CompradorProduto_Produto_idx (ProdutoID ASC),
  CONSTRAINT fk_CompradorProduto_Parceiro		    FOREIGN KEY (ParceiroID)  		  REFERENCES webrebate.ParceiroNegocio (ID),
  CONSTRAINT fk_CompradorProduto_Produto		    FOREIGN KEY (ProdutoID)		      REFERENCES webrebate.Produto (ID)
);


CREATE TABLE IF NOT EXISTS webrebate.Contato (
  ID 							INT 			NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Nome 							VARCHAR(100) 	NOT NULL,
  Email 						VARCHAR(150) 	NOT NULL,
  Telefone 						VARCHAR(14) 	NOT NULL,
  ParceiroID 					INT 			NOT NULL,
  INDEX fk_Contato_Parceiro_idx (ParceiroID ASC),
  CONSTRAINT fk_Contato_Parceiro    FOREIGN KEY (ParceiroID)    REFERENCES webrebate.ParceiroNegocio (ID)
 );



CREATE TABLE IF NOT EXISTS webrebate.FornecedorProduto (
  ID 							INT NOT NULL AUTO_INCREMENT	PRIMARY KEY,
  Valor 						DECIMAL(12,2) 	NOT NULL,
  Volume 						INT NOT NULL,
  CapacidadeMaxima 				INT NOT NULL,
  ParceiroID 					INT NOT NULL,
  ProdutoID 					INT NOT NULL,
  INDEX fk_FornecedorProduto_Produto_idx (ProdutoID ASC),
  INDEX fk_FornecedorProduto_Parceiro_idx (ParceiroID ASC),
  CONSTRAINT fk_FornecedorProduto_Produto    		FOREIGN KEY (ProdutoID)    			REFERENCES webrebate.Produto (ID),
  CONSTRAINT fk_FornecedorProduto_Fornecedor    FOREIGN KEY (ParceiroID)    		REFERENCES webrebate.ParceiroNegocio (ID)
);



CREATE TABLE IF NOT EXISTS webrebate.GrupoUsuarioRecurso (
  ID                INT NOT NULL AUTO_INCREMENT  PRIMARY KEY,
  GrupoID           INT NOT NULL,
  Recurso           VARCHAR(50) NOT NULL,
  INDEX fk_GrupoUsuarioRecurso_GrupoUsuario1_idx (GrupoID ASC),
  CONSTRAINT fk_GrupoUsuarioRecurso_GrupoUsuario1    FOREIGN KEY (GrupoID)    REFERENCES webrebate.GrupoUsuario (ID)
    );


