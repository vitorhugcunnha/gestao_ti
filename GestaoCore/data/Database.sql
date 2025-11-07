create database gestao_ti;
use gestao_ti;

create table usuario (
    id_usuario int primary key auto_increment not null,
    nome_usuario varchar(150) not null,
    senha varchar(150) not null
);

create table hardware (
    id int auto_increment primary key,
    tipo varchar(50) not null,
    marca varchar(50) not null,
    modelo varchar(100) not null,
    numero_serie varchar(100) unique not null,
    status enum('em_estoque', 'em_uso', 'manutencao', 'descartado') default 'em_estoque'
);

create table licenca (
    id int auto_increment primary key,
    nome_software varchar(100) not null,
    capacidade_total int not null,
    data_validade date not null
);

create table software (
    id int auto_increment primary key,
    id_licenca int not null,
    chave_licenca varchar(255) not null,
    data_aquisicao date not null default (current_date),
    foreign key (id_licenca) references licenca(id) on delete cascade
);

create table colaborador (
    id int auto_increment primary key,
    nome varchar(100) not null,
    email varchar(100) not null,
    cargo varchar(100) not null,
    departamento varchar(100) not null
);

create table alocacao (
    id int auto_increment primary key,
    id_colaborador int not null,
    id_hardware int null,
    id_software int null,
    data_alocacao datetime default current_timestamp,
    data_retorno datetime null,
    descricao_retorno varchar(255) null,
    status enum('em_uso', 'retornado') default 'em_uso',
    foreign key (id_colaborador) references colaborador(id) on delete cascade,
    foreign key (id_hardware) references hardware(id) on delete set null,
    foreign key (id_software) references software(id) on delete set null
);

insert into usuario (nome_usuario, senha)
values
('vitor_hugo', '110806');

select * from usuario;