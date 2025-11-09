# 💻 Sistema de Gestão de Recursos de TI

O Sistema de Gestão de Recursos de TI é uma aplicação de console desenvolvida em C# (.NET) com o objetivo de gerenciar os principais ativos tecnológicos de uma organização, como hardware, software, licenças e colaboradores.

Para o sistema funcionar precisa do MySql WorkBench e XAMPP Controll para o sistema funcionar.

Comando para fazer rodar no CMD:
```bash
dotnet run --project GestaoTIConsole/GestaoTIConsole.csproj
```
O sistema foi projetado com base em uma arquitetura modular e orientada a objetos, permitindo operações completas de CRUD (Criar, Ler, Atualizar e Deletar) para cada entidade, além de funcionalidades adicionais como alocação e retorno de equipamentos.

Com uma interface textual organizada e intuitiva, o sistema também conta com alertas automáticos de baixo estoque, exibidos dinamicamente no canto superior da tela, para auxiliar no controle e reposição de recursos.

⚙️ Funcionalidades Principais

Gerenciamento de Hardware — controle completo de equipamentos cadastrados.

Gerenciamento de Software e Licenças — controle de softwares e suas respectivas licenças.

Gerenciamento de Colaboradores — cadastro e manutenção de informações dos usuários.

Alocação e Retorno — vinculação de recursos a colaboradores e controle de uso.

Alertas de Estoque — avisos automáticos quando há baixo estoque de hardware ou licenças.

🧩 Tecnologias Utilizadas

Linguagem: C#

Banco de Dados: MySQL

Paradigma: Programação Orientada a Objetos (POO)

Conceitos: CRUD, DAO (Data Access Object), tratamento de exceções e camadas de abstração

🚀 Objetivo do Projeto

O projeto foi desenvolvido com foco em organizar o controle de recursos de TI dentro de uma empresa, promovendo maior rastreabilidade, eficiência e clareza nas operações de alocação e manutenção de equipamentos e softwares.

Tree do Arquivo:

```bash
C:.
├───GestaoCore
│   ├───bin
│   │   └───Debug
│   │       └───net9.0
│   ├───crud
│   ├───dao
│   ├───data
│   ├───models
│   └───obj
│       └───Debug
│           └───net9.0
│               ├───ref
│               └───refint
├───GestaoTIConsole
│   ├───bin
│   │   └───Debug
│   │       └───net9.0
│   │           └───runtimes
│   │               ├───win
│   │               │   └───lib
│   │               │       └───net8.0
│   │               └───win-x64
│   │                   └───native
│   ├───obj
│   │   └───Debug
│   │       └───net9.0
│   │           ├───ref
│   │           └───refint
│   └───utils
└───obj
    └───Debug
        └───net9.0
```
