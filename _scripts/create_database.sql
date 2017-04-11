CREATE DATABASE dbWinstonChurchill;

USE dbWinstonChurchill;

CREATE TABLE Usuario
(
	ID				INT 				NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Nome			VARCHAR(50)			NOT NULL,
    Descricao		VARCHAR(500)		NULL
)
