create database gestao_ti;
use gestao_ti;

CREATE TABLE usuario(
	id_usuario int primary key auto_increment NOT NULL,
    nome_usuario varchar(150) NOT NULL,
    senha varchar(150) NOT NULL
);

insert into usuario(nome_usuario, senha)
values
("vitor_hugo", "110806");