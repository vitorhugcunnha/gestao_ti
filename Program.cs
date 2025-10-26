using System;
using System.Collections.Generic;
using System.Linq;

// Define o namespace (deve ser o mesmo nome do seu projeto)
namespace GestaoDeAtivos
{
    public class Program
    {
        // --- "Banco de Dados" em Memória ---
        private static List<Ativo> DB_Ativos = new List<Ativo>();
        private static List<Colaborador> DB_Colaboradores = new List<Colaborador>();
        private static List<Licenca> DB_Licencas = new List<Licenca>();
        private static List<Usuario> DB_Usuarios = new List<Usuario>();

        // --- Ponto de Entrada do Programa ---
        public static void Main(string[] args)
        {
            Console.WriteLine("--- Bem-vindo ao Sistema de Gestão de Ativos (SGA-TI) ---");
            
            DB_Usuarios.Add(new Usuario("admin", "admin", PapelUsuario.TecnicoTI));

            bool executando = true;
            while (executando)
            {
                // 1. Exibir o menu de opções
                ExibirMenuPrincipal();
                
                // 2. Ler a escolha do usuário
                // 'ReadLine' retorna string?, mas 'opcao' era 'string'.
                // Mudamos 'opcao' para 'string?' para aceitar o nulo.
                string? opcao = Console.ReadLine();
                
                // 3. Executar a ação escolhida
                switch (opcao)
                {
                    case "1":
                        CadastrarHardware();
                        break;
                    case "2":
                        CadastrarLicenca();
                        break;
                    case "3":
                        CadastrarSoftware();
                        break;
                    case "4":
                        CadastrarColaborador();
                        break;
                    case "5":
                        AlocarAtivo();
                        break;
                    case "6":
                        RetornarAtivo();
                        break;
                    case "7":
                        ListarAtivos();
                        break;
                    case "8":
                        ListarColaboradores();
                        break;
                    case "0":
                        executando = false;
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida! Tente novamente.");
                        break;
                }

                if (executando)
                {
                    Pausar();
                }
            }

            Console.WriteLine("--- Sistema encerrado. Obrigado! ---");
        }

        // --- MÉTODOS DO MENU ---

        private static void ExibirMenuPrincipal()
        {
            Console.Clear(); // Limpa a tela
            Console.WriteLine("===== GJV-TI | MENU PRINCIPAL =====");
            Console.WriteLine("1. Cadastrar Ativo (Hardware)");
            Console.WriteLine("2. Cadastrar (Licença Mãe)");
            Console.WriteLine("3. Cadastrar Ativo (Software)");
            Console.WriteLine("4. Cadastrar Colaborador");
            Console.WriteLine("---");
            Console.WriteLine("5. Alocar Ativo");
            Console.WriteLine("6. Retornar Ativo");
            Console.WriteLine("---");
            Console.WriteLine("7. Listar Todos os Ativos");
            Console.WriteLine("8. Listar Colaboradores");
            Console.WriteLine("0. Sair");
            Console.Write("\nEscolha sua opção: ");
        }

        private static void CadastrarHardware()
        {
            Console.Clear();
            Console.WriteLine("--- 1. Cadastro de Novo Hardware ---");
            
            // Operador '!' (null-forgiving) utilizado para dizer ao compilador que garantimos que ReadLine() não será nulo.
            Console.Write("Tipo (ex: Notebook): ");
            string tipo = Console.ReadLine()!;
            Console.Write("Marca (ex: Dell): ");
            string marca = Console.ReadLine()!;
            Console.Write("Modelo (ex: XPS 15): ");
            string modelo = Console.ReadLine()!;
            Console.Write("Número de Série: ");
            string numSerie = Console.ReadLine()!;

            var novoHardware = new AtivoHardware(tipo, marca, modelo, DateTime.Now, numSerie);
            
            DB_Ativos.Add(novoHardware);

            Console.WriteLine($"\nSUCESSO! Ativo '{novoHardware.Marca} {novoHardware.Modelo}' criado.");
            Console.WriteLine($"ID Patrimonial gerado: {novoHardware.IdPatrimonial}");
            Console.WriteLine($"Status: {novoHardware.Status}"); 
        }

        private static void CadastrarLicenca()
        {
            Console.Clear();
            Console.WriteLine("--- 2. Cadastro de (Licença Mãe) ---");

            Console.Write("Nome do Software (ex: Adobe Photoshop): ");
            string nome = Console.ReadLine()!;
            Console.Write("Capacidade Total (Nº de usuários): ");
            int capacidade = int.Parse(Console.ReadLine()!);
            Console.Write("Data de Validade (AAAA-MM-DD): ");
            DateTime validade = DateTime.Parse(Console.ReadLine()!);

            var novaLicenca = new Licenca(nome, validade, capacidade);
            DB_Licencas.Add(novaLicenca);

            Console.WriteLine($"\nSUCESSO! Licença '{novaLicenca.NomeSoftware}' (ID: {novaLicenca.IdLicenca}) criada.");
        }
        
        private static void CadastrarSoftware()
        {
            Console.Clear();
            Console.WriteLine("--- 3. Cadastro de Ativo (Software) ---");
            
            if (DB_Licencas.Count == 0)
            {
                Console.WriteLine("ERRO: Você precisa cadastrar uma (Licença Mãe) primeiro (Opção 2).");
                return;
            }

            Console.WriteLine("Licenças-Mãe disponíveis:");
            foreach (var l in DB_Licencas)
            {
                Console.WriteLine($"ID: {l.IdLicenca} | Nome: {l.NomeSoftware} | Vagas: {l.CapacidadeTotal - l.GetInstalacoesAtivas()}");
            }
            
            Console.Write("Digite o ID da Licença-Mãe para vincular este software: ");
            int idLicenca = int.Parse(Console.ReadLine()!);
            
            // 'licencaMae' pode ser nula (FirstOrDefault).
            // Declaramos como 'Licenca?' (anulável).
            Licenca? licencaMae = DB_Licencas.FirstOrDefault(l => l.IdLicenca == idLicenca);
            
            // O 'if' abaixo já trata o caso nulo, então o compilador fica satisfeito.
            if(licencaMae == null)
            {
                Console.WriteLine("ERRO: Licença-Mãe não encontrada.");
                return;
            }

            Console.Write("Chave de Licença (Key): ");
            string chave = Console.ReadLine()!;

            try
            {
                var novoSoftware = new AtivoSoftware(
                    tipo: "Software", 
                    marca: licencaMae.NomeSoftware, 
                    modelo: "Instalação", 
                    dataAquisicao: DateTime.Now, 
                    chaveLicenca: chave, 
                    licencaAssociada: licencaMae
                );

                DB_Ativos.Add(novoSoftware);
                Console.WriteLine($"\nSUCESSO! Ativo de Software (ID: {novoSoftware.IdPatrimonial}) criado e vinculado à licença '{licencaMae.NomeSoftware}'.");
            }
            catch (InvalidOperationException e) 
            {
                Console.WriteLine($"\nFALHA AO CRIAR: {e.Message}");
            }
        }

        private static void CadastrarColaborador()
        {
            Console.Clear();
            Console.WriteLine("--- 4. Cadastro de Novo Colaborador ---");

            Console.Write("ID (Matrícula, ex: C1001): ");
            string id = Console.ReadLine()!;
            Console.Write("Nome: ");
            string nome = Console.ReadLine()!;
            Console.Write("Departamento: ");
            string depto = Console.ReadLine()!;

            var novoColab = new Colaborador(id, nome, depto);
            DB_Colaboradores.Add(novoColab);

            Console.WriteLine($"\nSUCESSO! Colaborador '{novoColab.Nome}' (ID: {novoColab.IdColaborador}) criado.");
        }

        private static void AlocarAtivo()
        {
            Console.Clear();
            Console.WriteLine("--- 5. Alocar Ativo ---");

            // 1. Encontrar o Ativo
            ListarAtivos("EM_ESTOQUE");
            
            Console.Write("\nDigite o ID Patrimonial do Ativo a alocar: ");
            string idAtivo = Console.ReadLine()!;
            
            // 'BuscarAtivoPorId' retorna 'Ativo?', então declaramos a variável como 'Ativo?'
            Ativo? ativo = BuscarAtivoPorId(idAtivo);

            if (ativo == null)
            {
                Console.WriteLine("ERRO: Ativo não encontrado.");
                return;
            }

            // 2. Encontrar o Colaborador
            ListarColaboradores();
            
            Console.Write("\nDigite o ID (Matrícula) do Colaborador: ");
            string idColab = Console.ReadLine()!;

            // 'BuscarColaboradorPorId' retorna 'Colaborador?', então declaramos como 'Colaborador?'
            Colaborador? colab = BuscarColaboradorPorId(idColab);

            if (colab == null)
            {
                Console.WriteLine("ERRO: Colaborador não encontrado.");
                return;
            }

            // 3. Executar Alocação
            try
            {
                // (O compilador sabe que 'ativo' e 'colab' não são nulos aqui por causa dos 'if' acima)
                ativo.AlocarPara(colab, DateTime.Now);
                Console.WriteLine($"\nSUCESSO! Ativo '{ativo.Modelo}' alocado para '{colab.Nome}'.");
                Console.WriteLine($"Novo status do ativo: {ativo.Status}");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"\nFALHA NA ALOCAÇÃO: {e.Message}");
            }
        }
        
        private static void RetornarAtivo()
        {
            Console.Clear();
            Console.WriteLine("--- 6. Retornar Ativo ---");

            // 1. Encontrar o Ativo
            ListarAtivos("EM_USO");
            
            Console.Write("\nDigite o ID Patrimonial do Ativo a retornar: ");
            string idAtivo = Console.ReadLine()!;
            
            // 'BuscarAtivoPorId' retorna 'Ativo?'
            Ativo? ativo = BuscarAtivoPorId(idAtivo);

            if (ativo == null)
            {
                Console.WriteLine("ERRO: Ativo não encontrado.");
                return;
            }

            Console.Write("Motivo do Retorno (ex: Manutenção, Descarte, Troca): ");
            string motivo = Console.ReadLine()!;
            Console.Write("Novo Status (1 para EmEstoque, 2 para Manutencao): ");
            string? statusOpcao = Console.ReadLine(); // 'string?' para o switch
            
            StatusAtivo novoStatus = (statusOpcao == "2") ? StatusAtivo.Manutencao : StatusAtivo.EmEstoque;

            // 3. Executar Retorno
            try
            {
                ativo.Retornar(DateTime.Now, motivo, novoStatus);
                Console.WriteLine($"\nSUCESSO! Ativo '{ativo.Modelo}' retornado.");
                Console.WriteLine($"Novo status: {ativo.Status}");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"\nFALHA AO RETORNAR: {e.Message}");
            }
        }

        private static void ListarAtivos(string filtroStatus = "")
        {
            if(filtroStatus == "")
                Console.Clear();
            
            Console.WriteLine($"\n--- Lista de Ativos ({filtroStatus}) ---");
            
            var ativosParaListar = DB_Ativos;

            if (filtroStatus == "EM_ESTOQUE")
                ativosParaListar = DB_Ativos.Where(a => a.Status == StatusAtivo.EmEstoque).ToList();
            else if (filtroStatus == "EM_USO")
                ativosParaListar = DB_Ativos.Where(a => a.Status == StatusAtivo.EmUso).ToList();
                
            if (ativosParaListar.Count == 0)
            {
                Console.WriteLine("Nenhum ativo encontrado.");
                return;
            }

            foreach (var ativo in ativosParaListar)
            {
                var alocacaoAtiva = ativo.HistoricoAlocacoes.FirstOrDefault(a => a.DataFim == null);
                string alocadoPara = (alocacaoAtiva != null) ? alocacaoAtiva.Colaborador.Nome : "N/A";

                Console.WriteLine($"> ID: {ativo.IdPatrimonial} | Tipo: {ativo.Tipo} | Modelo: {ativo.Modelo} | Status: {ativo.Status} | Alocado para: {alocadoPara}");
            }
        }
        
        private static void ListarColaboradores()
        {
            Console.WriteLine("\n--- Lista de Colaboradores ---");
            if (DB_Colaboradores.Count == 0)
            {
                Console.WriteLine("Nenhum colaborador cadastrado.");
                return;
            }

            foreach (var c in DB_Colaboradores)
            {
                Console.WriteLine($"> ID: {c.IdColaborador} | Nome: {c.Nome} | Depto: {c.Departamento}");
            }
        }

        // --- MÉTODOS AUXILIARES ---

        private static void Pausar()
        {
            Console.WriteLine("\nPressione Enter para voltar ao menu...");
            Console.ReadLine();
        }

        // CORREÇÃO: O método agora retorna 'Ativo?' (anulável)
        private static Ativo? BuscarAtivoPorId(string id)
        {
            return DB_Ativos.FirstOrDefault(a => a.IdPatrimonial.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        // CORREÇÃO: O método agora retorna 'Colaborador?' (anulável)
        private static Colaborador? BuscarColaboradorPorId(string id)
        {
            return DB_Colaboradores.FirstOrDefault(c => c.IdColaborador.Equals(id, StringComparison.OrdinalIgnoreCase));
        }
    }
}