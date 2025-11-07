using GestaoCore.Models;
using GestaoCore.dao;

namespace GestaoCore.crud
{
    public class CrudColaborador
    {
        private ITela tela;
        private ITela tela2;
        private ITela tela3;
        private ITela tela4;

        private ColaboradorDAO dao;
        private Colaborador? colaboradorAtual;

        public CrudColaborador(ITela tela, ITela tela2, ITela tela3, ITela tela4)
        {
            this.tela = tela3;
            this.tela2 = tela2;
            this.tela3 = tela3;
            this.tela4 = tela4;

            dao = new ColaboradorDAO();
        }

        public void Criar()
        {
            Console.Clear();
            tela.MontarMolduraCentralizada("Cadastro de Colaborador");

            int col = (Console.WindowWidth / 2) - 25;
            int lin = (Console.WindowHeight / 2) - 3;

            Console.SetCursorPosition(col, lin);
            Console.Write("Nome: ");
            Console.SetCursorPosition(col, lin + 2);
            Console.Write("E-mail: ");
            Console.SetCursorPosition(col, lin + 4);
            Console.Write("Cargo: ");
            Console.SetCursorPosition(col, lin + 6);
            Console.Write("Departamento: ");

            Console.SetCursorPosition(col + 7, lin);
            string nome = Console.ReadLine()?.Trim() ?? "";
            Console.SetCursorPosition(col + 9, lin + 2);
            string email = Console.ReadLine()?.Trim() ?? "";
            Console.SetCursorPosition(col + 8, lin + 4);
            string cargo = Console.ReadLine()?.Trim() ?? "";
            Console.SetCursorPosition(col + 15, lin + 6);
            string departamento = Console.ReadLine()?.Trim() ?? "";

            while (Console.KeyAvailable) Console.ReadKey(true);

            var colaborador = new Colaborador
            {
                Nome = nome,
                Email = email,
                Cargo = cargo,
                Departamento = departamento
            };

            bool sucesso = dao.Inserir(colaborador);

            Console.Clear();
            if (sucesso)
                tela.MontarMolduraCentralizada("Cadastro Concluído");
            else
                tela.MontarMolduraCentralizada("Erro ao cadastrar colaborador.");

            int colConf = (Console.WindowWidth / 2) - 25;
            int linConf = (Console.WindowHeight / 2) - 4;

            Console.SetCursorPosition(colConf, linConf + 2);
            Console.WriteLine($"Nome: {nome}");
            Console.SetCursorPosition(colConf, linConf + 3);
            Console.WriteLine($"E-mail: {email}");
            Console.SetCursorPosition(colConf, linConf + 4);
            Console.WriteLine($"Cargo: {cargo}");
            Console.SetCursorPosition(colConf, linConf + 5);
            Console.WriteLine($"Departamento: {departamento}");

            Console.SetCursorPosition(colConf, linConf + 7);
            Console.Write("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void Alterar()
        {
            Console.Clear();
            tela4.MontarMolduraCentralizada("Alterar Colaborador");

            int col = (Console.WindowWidth / 2) - 25;
            int lin = (Console.WindowHeight / 2) - 6;

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
                Console.Clear();
                tela.MontarMolduraCentralizada("ID inválido.");
                Console.SetCursorPosition(col, lin + 2);
                Console.Write("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                Voltar();
                return;
            }

            colaboradorAtual = dao.BuscarPorId(id);

            if (colaboradorAtual == null)
            {
                Console.Clear();
                tela.MontarMolduraCentralizada("Colaborador não encontrado.");
                Console.SetCursorPosition(col, lin + 2);
                Console.Write("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                Voltar();
                return;
            }

            Console.Clear();
            tela4.MontarMolduraCentralizada("Editar Colaborador");

            Console.SetCursorPosition(col, lin);
            Console.Write($"ID: {colaboradorAtual.Id}");

            Console.SetCursorPosition(col, lin + 2);
            Console.Write($"NOME: {colaboradorAtual.Nome}");
            Console.SetCursorPosition(col + 30, lin + 2);
            Console.Write("Novo NOME: ");
            string novoNome = Console.ReadLine()?.Trim() ?? "";

            Console.SetCursorPosition(col, lin + 4);
            Console.Write($"E-MAIL: {colaboradorAtual.Email}");
            Console.SetCursorPosition(col + 30, lin + 4);
            Console.Write("Novo E-MAIL: ");
            string novoEmail = Console.ReadLine()?.Trim() ?? "";

            Console.SetCursorPosition(col, lin + 6);
            Console.Write($"CARGO: {colaboradorAtual.Cargo}");
            Console.SetCursorPosition(col + 30, lin + 6);
            Console.Write("Novo CARGO: ");
            string novoCargo = Console.ReadLine()?.Trim() ?? "";

            Console.SetCursorPosition(col, lin + 8);
            Console.Write($"DEPARTAMENTO: {colaboradorAtual.Departamento}");
            Console.SetCursorPosition(col + 30, lin + 8);
            Console.Write("Novo DEPARTAMENTO: ");
            string novoDepartamento = Console.ReadLine()?.Trim() ?? "";

            if (!string.IsNullOrEmpty(novoNome)) colaboradorAtual.Nome = novoNome;
            if (!string.IsNullOrEmpty(novoEmail)) colaboradorAtual.Email = novoEmail;
            if (!string.IsNullOrEmpty(novoCargo)) colaboradorAtual.Cargo = novoCargo;
            if (!string.IsNullOrEmpty(novoDepartamento)) colaboradorAtual.Departamento = novoDepartamento;

            Console.Clear();
            tela4.MontarMolduraCentralizada("Confirmação de Alteração");

            Console.SetCursorPosition(col, lin);
            Console.WriteLine($"ID: {colaboradorAtual.Id}");
            Console.SetCursorPosition(col, lin + 1);
            Console.WriteLine($"NOME: {colaboradorAtual.Nome}");
            Console.SetCursorPosition(col, lin + 2);
            Console.WriteLine($"E-MAIL: {colaboradorAtual.Email}");
            Console.SetCursorPosition(col, lin + 3);
            Console.WriteLine($"CARGO: {colaboradorAtual.Cargo}");
            Console.SetCursorPosition(col, lin + 4);
            Console.WriteLine($"DEPARTAMENTO: {colaboradorAtual.Departamento}");

            Console.SetCursorPosition(col, lin + 6);
            Console.WriteLine("1 - Confirmar alterações");
            Console.SetCursorPosition(col, lin + 7);
            Console.WriteLine("0 - Cancelar e voltar");

            Console.SetCursorPosition(col, lin + 9);
            Console.Write("Escolha uma opção: ");
            string? opcao = Console.ReadLine()?.Trim();

            if (opcao == "1")
            {
                bool sucesso = dao.Atualizar(colaboradorAtual);
                Console.Clear();
                tela.MontarMolduraCentralizada(sucesso
                    ? "Colaborador alterado com sucesso!"
                    : "Erro ao atualizar colaborador!");
                Console.SetCursorPosition(col, lin + 2);
                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                Voltar();
            }
            else
            {
                Voltar();
            }
        }

        public void Listar()
        {
            Console.Clear();
            var colaboradores = dao.Listar();
            if (colaboradores.Count == 0)
            {
                tela.MontarMolduraCentralizada("Nenhum colaborador cadastrado.");
                Console.SetCursorPosition((Console.WindowWidth / 2) - 15, (Console.WindowHeight / 2) + 2);
                Console.Write("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            string[] headers = { "ID", "NOME", "E-MAIL", "CARGO", "DEPARTAMENTO" };
            int[] col = new int[headers.Length];

            for (int i = 0; i < headers.Length; i++)
                col[i] = headers[i].Length;
            foreach (var c in colaboradores)
            {
                col[0] = Math.Max(col[0], c.Id.ToString().Length);
                col[1] = Math.Max(col[1], c.Nome.Length);
                col[2] = Math.Max(col[2], c.Email.Length);
                col[3] = Math.Max(col[3], c.Cargo.Length);
                col[4] = Math.Max(col[4], c.Departamento.Length);
            }

            for (int i = 0; i < col.Length; i++)
                col[i] += 1;

            int totalCols = Math.Max(col.Sum() + (headers.Length + 2) + 1, 3);
            int larguraConsole = Console.WindowWidth;
            int alturaConsole = Console.WindowHeight;
            int colunaInicial = Math.Max((larguraConsole - totalCols) / 2, 0);
            int alturaTabela = 7 + colaboradores.Count;
            int linhaInicial = Math.Max((alturaConsole - alturaTabela) / 2, 0);

            Console.SetCursorPosition(colunaInicial, linhaInicial);
            Console.WriteLine("╔" + new string('═', totalCols - 2) + "╗");

            string titulo = "Lista de Colaboradores";
            int paddingTitulo = Math.Max((totalCols - 2 - titulo.Length) / 2, 0);
            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("║" + new string(' ', paddingTitulo) + titulo +
                              new string(' ', Math.Max(totalCols - 2 - paddingTitulo - titulo.Length, 0)) + "║");

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("╠" + new string('═', totalCols - 2) + "╣");

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.Write("║ ");
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(headers[i].PadRight(col[i]));
                if (i < headers.Length - 1) Console.Write(" ");
            }
            Console.WriteLine(" ║");

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("╟" + new string('─', totalCols - 2) + "╢");

            foreach (var c in colaboradores)
            {
                Console.SetCursorPosition(colunaInicial, Console.CursorTop);
                Console.Write("║ ");
                Console.Write(c.Id.ToString().PadRight(col[0]));
                Console.Write(" " + c.Nome.PadRight(col[1]));
                Console.Write(" " + c.Email.PadRight(col[2]));
                Console.Write(" " + c.Cargo.PadRight(col[3]));
                Console.Write(" " + c.Departamento.PadRight(col[4]));
                Console.WriteLine(" ║");
            }

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("╠" + new string('═', totalCols - 2) + "╣");

            string mensagem = "Pressione qualquer tecla para continuar..";
            int paddingMsg = Math.Max((totalCols - 2 - mensagem.Length) / 2, 0);
            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("║" + new string(' ', paddingMsg) + mensagem +
                              new string(' ', Math.Max(totalCols - 2 - paddingMsg - mensagem.Length, 0)) + "║");

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("╚" + new string('═', totalCols - 2) + "╝");

            Console.ReadKey();
        }
        public void Deletar()
        {
            Console.Clear();
            tela4.MontarMolduraCentralizada("Excluir Colaborador");

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
                Console.Clear();
                tela.MontarMolduraCentralizada("ID inválido.");
                Console.SetCursorPosition(col, lin + 2);
                Console.Write("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                Voltar();
                return;
            }

            var colaborador = dao.BuscarPorId(id);

            if (colaborador == null)
            {
                Console.Clear();
                tela.MontarMolduraCentralizada("Colaborador não encontrado.");
                Console.SetCursorPosition(col, lin + 2);
                Console.Write("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                Voltar();
                return;
            }

            Console.Clear();
            tela4.MontarMolduraCentralizada("Confirmação de Exclusão");

            Console.SetCursorPosition(col, lin);
            Console.WriteLine($"ID: {colaborador.Id}");
            Console.SetCursorPosition(col, lin + 1);
            Console.WriteLine($"NOME: {colaborador.Nome}");
            Console.SetCursorPosition(col, lin + 2);
            Console.WriteLine($"CARGO: {colaborador.Cargo}");
            Console.SetCursorPosition(col, lin + 3);
            Console.WriteLine($"E-MAIL: {colaborador.Email}");
            Console.SetCursorPosition(col, lin + 4);
            Console.WriteLine($"DEPARTAMENTO: {colaborador.Departamento}");

            Console.SetCursorPosition(col, lin + 6);
            Console.WriteLine("1 - Confirmar exclusão");
            Console.SetCursorPosition(col, lin + 7);
            Console.WriteLine("0 - Voltar");

            Console.SetCursorPosition(col, lin + 9);
            Console.Write("Escolha uma opção: ");
            string? opcao = Console.ReadLine()?.Trim();

            if (opcao == "1")
            {
                bool sucesso = dao.Excluir(colaborador.Id);
                Console.Clear();
                tela.MontarMolduraCentralizada(sucesso
                    ? "Colaborador excluído com sucesso!"
                    : "Erro ao excluir colaborador!");
                Console.SetCursorPosition(col, lin + 2);
                Console.Write("Pressione qualquer tecla para continuar...");
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
