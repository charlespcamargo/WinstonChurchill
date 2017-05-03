CREATE DATABASE IF NOT EXISTS dbWinstonChurchill;

USE dbWinstonChurchill;


CREATE TABLE IF NOT EXISTS dbWinstonChurchill.Usuario
(
	ID						NUMERIC(10)			NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Nome					VARCHAR(50)			NOT NULL,
    Email					VARCHAR(50)			NOT NULL,
    Ativo					BIT					NOT NULL,    
    DataCadastro			DATETIME			NOT NULL,
    Senha					VARCHAR(128)		NOT NULL
);


CREATE TABLE IF NOT EXISTS dbWinstonChurchill.GrupoUsuario
(
	ID						NUMERIC(10)			NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Nome					VARCHAR(50)			NOT NULL,
    Descricao				VARCHAR(500)		NOT NULL,
    Ativo					BIT					NOT NULL,    
    DataCadastro			DATETIME			NOT NULL
);

CREATE TABLE IF NOT EXISTS dbWinstonChurchill.UsuarioXGrupoUsuario
(
	ID						NUMERIC(10) 		NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UsuarioID				NUMERIC(10)			NOT NULL,
    GrupoUsuarioID			NUMERIC(10)			NOT NULL,
    Ativo					BIT					NOT NULL,    
    DataCadastro			DATETIME			NOT NULL,
    ResponsavelID			NUMERIC(10)			NOT NULL,
    
    FOREIGN KEY FK_UsuarioXGrupoUsuario_UsuarioID (UsuarioID) REFERENCES Usuario(ID),
    FOREIGN KEY FK_UsuarioXGrupoUsuario_GrupoUsuarioID (GrupoUsuarioID) REFERENCES GrupoUsuario(ID),
	CONSTRAINT UC_Usuario_Grupo UNIQUE (UsuarioID,GrupoUsuarioID)
);




