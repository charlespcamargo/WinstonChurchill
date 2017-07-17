INSERT INTO grupousuario (ID, Nome, Descricao, Ativo, DataCadastro) VALUES (1000, 'SuperUsuario', 'Super Usuario', true, now());
INSERT INTO grupousuario (ID, Nome, Descricao, Ativo, DataCadastro) VALUES (1001, 'Administrador', 'Administrador', true, now());
INSERT INTO grupousuario (ID, Nome, Descricao, Ativo, DataCadastro) VALUES (1, 'Fornecedor', 'Fornecedor', true, now());
INSERT INTO grupousuario (ID, Nome, Descricao, Ativo, DataCadastro) VALUES (2, 'Comprador', 'Comprador', true, now());
INSERT INTO grupousuario (ID, Nome, Descricao, Ativo, DataCadastro) VALUES (3, 'RepresentanteComercial', 'Representante Comercial', true, now());


/*Perfil*/
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1000, 'Perfil');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1001, 'Perfil');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1, 'Perfil');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (2, 'Perfil');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (3, 'Perfil');

/*Usuario*/
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1000, 'Usuario');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1001, 'Usuario');

/*Grup oAcesso*/
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1000, 'GrupoAcesso');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1001, 'GrupoAcesso');

/*Parâmetros*/
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1000, 'Parametro');

/*Categoria*/

INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1000, 'Categoria');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1001, 'Categoria');

INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1000, 'CategoriaImagem');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1001, 'CategoriaImagem');

/*Produto*/
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1000, 'Produto');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1001, 'Produto');

INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1000, 'ProdutoImagens');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1001, 'ProdutoImagens');

/*Grupo de Negócio*/
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1000, 'Grupo');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1001, 'Grupo');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (3, 'Grupo');

/*Parceiro de Negócio*/
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1000, 'ParceiroNegocio');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1001, 'ParceiroNegocio');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1, 'ParceiroNegocio');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (2, 'ParceiroNegocio');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (3, 'ParceiroNegocio');

/*Parceiro de Negócio*/
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1000, 'Leilao');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1001, 'Leilao');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (1, 'Leilao');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (2, 'Leilao');
INSERT INTO GrupoUsuarioRecurso (GrupoID, Recurso) VALUES (3, 'Leilao');


INSERT INTO UsuarioxGrupoUsuario (UsuarioID, GrupoUsuarioID, Ativo, DataCadastro, ResponsavelID) VALUES(1, 1000, 1, Now(), 1)

/*

select * from grupousuario
select * from usuarioxgrupousuario
select * from GrupoUsuarioRecurso

select * from Usuario

*/