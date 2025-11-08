using GestaoCore.Models;
using GestaoCore.dao;

namespace GestaoCore.crud
{
    public class CrudLicenca
    {
        private ITela tela;
        private ITela tela2;
        private ITela tela3;
        private ITela tela4;

        private LicencaDAO dao;
        private Licenca? licencaAtual;

        public CrudLicenca(ITela tela, ITela tela2, ITela tela3, ITela tela4)
        {
            this.tela = tela3;
            this.tela2 = tela2;
            this.tela3 = tela3;
            this.tela4 = tela4;

            dao = new LicencaDAO();
        }

        public void Criar()
        {
            Console.Clear();
            tela.MontarMolduraCentralizada("Cadastro de Licença");

            int col = (Console.WindowWidth / 2) - 25;
            int lin = (Console.WindowHeight / 2) - 3;

            Console.SetCursorPosition(col, lin);
            Console.Write("Nome da Licença: ");
            Console.SetCursorPosition(col, lin + 2);
            Console.Write("Data de Validade (AAAA-MM-DD): ");

            Console.SetCursorPosition(col + 18, lin);
            string nome = Console.ReadLine()?.Trim() ?? "";

            DateTime dataValidade = DateTime.MinValue;
            bool dataValida = false;

            do
            {
                Console.SetCursorPosition(col + 31, lin + 2);
                string dataInput = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(dataInput))
                {
                    Console.SetCursorPosition(col, lin + 4);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("A data de validade é obrigatória!");
                    Console.ResetColor();

                    Console.SetCursorPosition(col + 31, lin + 2);
                    Console.Write(new string(' ', 15));
                    continue;
                }

                if (!DateTime.TryParseExact(dataInput, "yyyy-MM-dd",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out dataValidade))
                {
                    Console.SetCursorPosition(col, lin + 4);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Formato inválido! Use o padrão AAAA-MM-DD.");
                    Console.ResetColor();

                    Console.SetCursorPosition(col + 31, lin + 2);
                    Console.Write(new string(' ', 15));
                }
                else
                {
                    dataValida = true;
                    Console.SetCursorPosition(col, lin + 4);
                    Console.Write(new string(' ', 80));
                }

            } while (!dataValida);

            var licenca = new Licenca
            {
                Nome = nome,
                DataValidade = dataValidade
            };

            bool sucesso = dao.Inserir(licenca);

            Console.Clear();
            if (sucesso)
                tela.MontarMolduraCentralizada("Licença cadastrada com sucesso!");
            else
                tela.MontarMolduraCentralizada("Erro ao cadastrar licença!");

            Console.SetCursorPosition(col, lin + 4);
            Console.Write($"Nome: {nome}");
            Console.SetCursorPosition(col, lin + 5);
            Console.Write($"Validade: {dataValidade:yyyy-MM-dd}");

            Console.SetCursorPosition(col, lin + 7);
            Console.Write("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }


    public void Alterar()
    {
        Console.Clear();
        tela4.MontarMolduraCentralizada("Alterar Licença");

        int col = (Console.WindowWidth / 2) - 25;
        int lin = (Console.WindowHeight / 2) - 5;

        Console.SetCursorPosition(col, lin);
        Console.Write("ID: ");
        string? entrada = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(entrada) || entrada == "0")
        {
            Voltar();
            return;
        }

        if (!int.TryParse(entrada, out int id))
        {
            tela.MontarMolduraCentralizada("ID inválido!");
            Voltar();
            return;
        }

        licencaAtual = dao.BuscarPorId(id);
        if (licencaAtual == null)
        {
            tela.MontarMolduraCentralizada("Licença não encontrada!");
            Voltar();
            return;
        }

        Console.Clear();
        tela4.MontarMolduraCentralizada("Editar Licença");

        Console.SetCursorPosition(col, lin);
        Console.WriteLine($"ID: {licencaAtual.Id}");

        Console.SetCursorPosition(col, lin + 2);
        Console.Write($"NOME ATUAL: {licencaAtual.Nome}");
        Console.SetCursorPosition(col + 35, lin + 2);
        Console.Write("Novo NOME: ");
        string novoNome = Console.ReadLine()?.Trim() ?? "";

        Console.SetCursorPosition(col, lin + 4);
        Console.Write($"DATA ATUAL: {licencaAtual.DataValidade:yyyy-MM-dd}");
        Console.SetCursorPosition(col + 35, lin + 4);
        Console.Write("Nova DATA (AAAA-MM-DD): ");
        string novaData = Console.ReadLine()?.Trim() ?? "";

        if (!string.IsNullOrEmpty(novoNome))
            licencaAtual.Nome = novoNome;

        if (!string.IsNullOrEmpty(novaData) && DateTime.TryParse(novaData, out DateTime novaDataVal))
            licencaAtual.DataValidade = novaDataVal;

        bool sucesso = dao.Atualizar(licencaAtual);

        Console.Clear();
        tela.MontarMolduraCentralizada(sucesso ? "Licença atualizada com sucesso!" : "Erro ao atualizar licença!");

        Console.SetCursorPosition(col, lin + 3);
        Console.Write("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
        public void Listar()
        {
            Console.Clear();
            var licencas = dao.Listar();

            if (licencas.Count == 0)
            {
                tela.MontarMolduraCentralizada("Nenhuma licença cadastrada.");
                Console.SetCursorPosition((Console.WindowWidth / 2) - 15, (Console.WindowHeight / 2) + 2);
                Console.Write("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            string[] headers = { "ID", "NOME", "DATA DE VALIDADE" };
            int[] col = new int[headers.Length];

            for (int i = 0; i < headers.Length; i++)
                col[i] = headers[i].Length;


            foreach (var l in licencas)
            {
                col[0] = Math.Max(col[0], l.Id.ToString().Length);
                col[1] = Math.Max(col[1], l.Nome.Length);
                col[2] = Math.Max(col[2], l.DataValidade.ToString("yyyy-MM-dd").Length);
            }


            for (int i = 0; i < col.Length; i++)
                col[i] += 2;

            int totalTabela = col.Sum() + headers.Length + 3;


            string mensagem = "Pressione qualquer tecla para continuar..";

            int totalCols = Math.Max(totalTabela, mensagem.Length + 4);

            int larguraConsole = Console.WindowWidth;
            int alturaConsole = Console.WindowHeight;
            int colunaInicial = Math.Max((larguraConsole - totalCols) / 2, 0);
            int alturaTabela = 7 + licencas.Count;
            int linhaInicial = Math.Max((alturaConsole - alturaTabela) / 2, 0);

            Console.SetCursorPosition(colunaInicial, linhaInicial);
            Console.WriteLine("╔" + new string('═', totalCols - 2) + "╗");

            string titulo = "Lista de Licenças";
            int paddingTitulo = Math.Max((totalCols - 2 - titulo.Length) / 2, 0);
            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("║" + new string(' ', paddingTitulo) + titulo +
                              new string(' ', totalCols - 2 - paddingTitulo - titulo.Length) + "║");

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("╠" + new string('═', totalCols - 2) + "╣");

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.Write("║ ");
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(headers[i].PadRight(col[i]));
            }
            Console.WriteLine(" ║");

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("╟" + new string('─', totalCols - 2) + "╢");

            foreach (var l in licencas)
            {
                Console.SetCursorPosition(colunaInicial, Console.CursorTop);
                Console.Write("║ ");
                Console.Write(l.Id.ToString().PadRight(col[0]));
                Console.Write(l.Nome.PadRight(col[1]));
                Console.Write(l.DataValidade.ToString("yyyy-MM-dd").PadRight(col[2]));
                Console.WriteLine(" ║");
            }

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("╠" + new string('═', totalCols - 2) + "╣");

            int paddingMsg = Math.Max((totalCols - 2 - mensagem.Length) / 2, 0);
            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("║" + new string(' ', paddingMsg) + mensagem +
                              new string(' ', totalCols - 2 - paddingMsg - mensagem.Length) + "║");

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("╚" + new string('═', totalCols - 2) + "╝");

            Console.ReadKey();
        }

        public void Deletar()
        {
            Console.Clear();
            tela4.MontarMolduraCentralizada("Excluir Licença");

            int col = (Console.WindowWidth / 2) - 25;
            int lin = (Console.WindowHeight / 2) - 5;

            Console.SetCursorPosition(col, lin);
            Console.Write("ID: ");
            string? entrada = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(entrada) || !int.TryParse(entrada, out int id))
            {
                tela.MontarMolduraCentralizada("ID inválido!");
                Voltar();
                return;
            }

            var licenca = dao.BuscarPorId(id);
            if (licenca == null)
            {
                tela.MontarMolduraCentralizada("Licença não encontrada!");
                Voltar();
                return;
            }

            Console.Clear();
            tela4.MontarMolduraCentralizada("Confirmação de Exclusão");

            Console.SetCursorPosition(col, lin);
            Console.WriteLine($"ID: {licenca.Id}");
            Console.SetCursorPosition(col, lin + 1);
            Console.WriteLine($"Nome: {licenca.Nome}");
            Console.SetCursorPosition(col, lin + 2);
            Console.WriteLine($"Validade: {licenca.DataValidade:yyyy-MM-dd}");

            Console.SetCursorPosition(col, lin + 4);
            Console.WriteLine("1 - Confirmar exclusão");
            Console.SetCursorPosition(col, lin + 5);
            Console.WriteLine("0 - Voltar");

            Console.SetCursorPosition(col, lin + 7);
            Console.Write("Escolha: ");
            string? opcao = Console.ReadLine();

            if (opcao == "1")
            {
                bool sucesso = dao.Excluir(id);
                Console.Clear();
                tela.MontarMolduraCentralizada(sucesso
                    ? "Licença excluída com sucesso!"
                    : "Erro ao excluir licença!");
                Console.ReadKey();
            }
            else
            {
                Voltar();
            }
        }

        public void Voltar()
        {
            Console.Clear();
            tela.MontarMolduraCentralizada("Retornando ao menu anterior...");
            Thread.Sleep(1500);
            Console.Clear();
        }
    }
}
