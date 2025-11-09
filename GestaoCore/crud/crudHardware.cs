using GestaoCore.Models;
using GestaoCore.dao;

namespace GestaoCore.crud
{
    public class CrudHardware
    {
        private ITela tela;
        private ITela tela2;
        private ITela tela3;
        private ITela tela4;

        private HardwareDAO dao;
        private Hardware? hardwareAtual;

        public CrudHardware(ITela tela, ITela tela2, ITela tela3, ITela tela4)
        {
            this.tela = tela3;
            this.tela2 = tela2;
            this.tela3 = tela3;
            this.tela4 = tela4;

            dao = new HardwareDAO();
        }

        public void Criar()
        {
            Console.Clear();
            tela.MontarMolduraCentralizada("Cadastro de Hardware");

            int col = (Console.WindowWidth / 2) - 25;
            int lin = (Console.WindowHeight / 2) - 3;

            Console.SetCursorPosition(col, lin);
            Console.Write("Tipo: ");
            Console.SetCursorPosition(col, lin + 2);
            Console.Write("Marca: ");
            Console.SetCursorPosition(col, lin + 4);
            Console.Write("Modelo: ");
            Console.SetCursorPosition(col, lin + 6);
            Console.Write("Número de Série: ");

            Console.SetCursorPosition(col + 7, lin);
            string tipo = Console.ReadLine()?.Trim() ?? "";
            Console.SetCursorPosition(col + 8, lin + 2);
            string marca = Console.ReadLine()?.Trim() ?? "";
            Console.SetCursorPosition(col + 9, lin + 4);
            string modelo = Console.ReadLine()?.Trim() ?? "";
            Console.SetCursorPosition(col + 17, lin + 6);
            string numeroSerie = Console.ReadLine()?.Trim() ?? "";

            while (Console.KeyAvailable) Console.ReadKey(true);

            var hardware = new Hardware
            {
                Tipo = tipo,
                Marca = marca,
                Modelo = modelo,
                NumeroSerie = numeroSerie
            };

            bool sucesso = dao.Inserir(hardware);

            Console.Clear();
            if (sucesso)
                tela.MontarMolduraCentralizada("Cadastro Concluído");
            else
                tela.MontarMolduraCentralizada("Erro ao cadastrar hardware.");

            int colConf = (Console.WindowWidth / 2) - 25;
            int linConf = (Console.WindowHeight / 2) - 4;

            Console.SetCursorPosition(colConf, linConf + 2);
            Console.WriteLine($"Tipo: {tipo}");
            Console.SetCursorPosition(colConf, linConf + 3);
            Console.WriteLine($"Marca: {marca}");
            Console.SetCursorPosition(colConf, linConf + 4);
            Console.WriteLine($"Modelo: {modelo}");
            Console.SetCursorPosition(colConf, linConf + 5);
            Console.WriteLine($"Nº Série: {numeroSerie}");

            Console.SetCursorPosition(colConf, linConf + 7);
            Console.Write("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void Alterar()
        {
            Console.Clear();
            tela4.MontarMolduraCentralizada("Alterar Hardware");

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
                tela.MostrarMensagem("ID inválido.");
                Voltar();
                return;
            }

            hardwareAtual = dao.BuscarPorId(id);

            if (hardwareAtual == null)
            {
                tela.MostrarMensagem("ID não encontrado.");
                Voltar();
                return;
            }

            Console.Clear();
            tela4.MontarMolduraCentralizada("Editar Hardware");

            Console.SetCursorPosition(col, lin);
            Console.Write($"ID: {hardwareAtual.Id}");

            Console.SetCursorPosition(col, lin + 2);
            Console.Write($"TIPO: {hardwareAtual.Tipo}");
            Console.SetCursorPosition(col + 30, lin + 2);
            Console.Write("Novo TIPO: ");
            string novoTipo = Console.ReadLine()?.Trim() ?? "";

            Console.SetCursorPosition(col, lin + 4);
            Console.Write($"MARCA: {hardwareAtual.Marca}");
            Console.SetCursorPosition(col + 30, lin + 4);
            Console.Write("Nova MARCA: ");
            string novaMarca = Console.ReadLine()?.Trim() ?? "";

            Console.SetCursorPosition(col, lin + 6);
            Console.Write($"MODELO: {hardwareAtual.Modelo}");
            Console.SetCursorPosition(col + 30, lin + 6);
            Console.Write("Novo MODELO: ");
            string novoModelo = Console.ReadLine()?.Trim() ?? "";

            Console.SetCursorPosition(col, lin + 8);
            Console.Write($"Nº SÉRIE: {hardwareAtual.NumeroSerie}");
            Console.SetCursorPosition(col + 30, lin + 8);
            Console.Write("Novo Nº SÉRIE: ");
            string novoNumeroSerie = Console.ReadLine()?.Trim() ?? "";

            Console.SetCursorPosition(col, lin + 10);
            Console.Write($"STATUS: {hardwareAtual.Status}");
            Console.SetCursorPosition(col + 30, lin + 10);
            Console.Write("Novo STATUS: ");
            string novoStatus = Console.ReadLine()?.Trim() ?? "";

            if (!string.IsNullOrEmpty(novoTipo)) hardwareAtual.Tipo = novoTipo;
            if (!string.IsNullOrEmpty(novaMarca)) hardwareAtual.Marca = novaMarca;
            if (!string.IsNullOrEmpty(novoModelo)) hardwareAtual.Modelo = novoModelo;
            if (!string.IsNullOrEmpty(novoNumeroSerie)) hardwareAtual.NumeroSerie = novoNumeroSerie;
            if (!string.IsNullOrEmpty(novoStatus)) hardwareAtual.Status = novoStatus;

            Console.Clear();
            tela4.MontarMolduraCentralizada("Confirmação de Alteração");

            Console.SetCursorPosition(col, lin);
            Console.WriteLine($"ID: {hardwareAtual.Id}");
            Console.SetCursorPosition(col, lin + 1);
            Console.WriteLine($"TIPO: {hardwareAtual.Tipo}");
            Console.SetCursorPosition(col, lin + 2);
            Console.WriteLine($"MARCA: {hardwareAtual.Marca}");
            Console.SetCursorPosition(col, lin + 3);
            Console.WriteLine($"MODELO: {hardwareAtual.Modelo}");
            Console.SetCursorPosition(col, lin + 4);
            Console.WriteLine($"Nº SÉRIE: {hardwareAtual.NumeroSerie}");
            Console.SetCursorPosition(col, lin + 5);
            Console.WriteLine($"STATUS: {hardwareAtual.Status}");

            Console.SetCursorPosition(col, lin + 7);
            Console.WriteLine("1 - Confirmar alterações");
            Console.SetCursorPosition(col, lin + 8);
            Console.WriteLine("0 - Cancelar e voltar");

            Console.SetCursorPosition(col, lin + 10);
            Console.Write("Escolha uma opção: ");
            string? opcao = Console.ReadLine()?.Trim();

            if (opcao == "1")
            {
                bool sucesso = dao.Atualizar(hardwareAtual);
                Console.Clear();
                tela.MontarMolduraCentralizada(sucesso
                    ? "Hardware alterado com sucesso!"
                    : "Erro ao atualizar hardware!");
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
            var hardwares = dao.Listar();

            if (hardwares.Count == 0)
            {
                tela.MontarMolduraCentralizada("Nenhum hardware cadastrado.");
                Console.SetCursorPosition((Console.WindowWidth / 2) - 15, (Console.WindowHeight / 2) + 2);
                Console.Write("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            string[] headers = { "ID", "TIPO", "MARCA", "MODELO", "N° SÉRIE", "STATUS" };
            int[] col = new int[headers.Length];

            for (int i = 0; i < headers.Length; i++)
                col[i] = headers[i].Length;

            foreach (var h in hardwares)
            {
                col[0] = Math.Max(col[0], h.Id.ToString().Length);
                col[1] = Math.Max(col[1], h.Tipo.Length);
                col[2] = Math.Max(col[2], h.Marca.Length);
                col[3] = Math.Max(col[3], h.Modelo.Length);
                col[4] = Math.Max(col[4], h.NumeroSerie.Length);
                col[5] = Math.Max(col[5], h.Status.Length);
            }

            for (int i = 0; i < col.Length; i++)
                col[i] += 2;

            int totalTabela = col.Sum() + headers.Length + 3;
            string mensagem = "Pressione qualquer tecla para continuar...";
            int totalCols = Math.Max(totalTabela, mensagem.Length + 4);

    
            int larguraConsole = Console.WindowWidth;
            int alturaConsole = Console.WindowHeight;
            int colunaInicial = Math.Max((larguraConsole - totalCols) / 2, 0);
            int alturaTabela = 7 + hardwares.Count;
            int linhaInicial = Math.Max((alturaConsole - alturaTabela) / 2, 0);

            Console.SetCursorPosition(colunaInicial, linhaInicial);
            Console.WriteLine("╔" + new string('═', totalCols - 2) + "╗");

            string titulo = "Lista de Hardwares";
            int paddingTitulo = Math.Max((totalCols - 2 - titulo.Length) / 2, 0);
            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("║" + new string(' ', paddingTitulo) + titulo +
                            new string(' ', totalCols - 2 - paddingTitulo - titulo.Length) + "║");

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("╠" + new string('═', totalCols - 2) + "╣");

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.Write("║ ");
            for (int i = 0; i < headers.Length; i++)
                Console.Write(headers[i].PadRight(col[i]));
            Console.WriteLine(" ║");

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("╟" + new string('─', totalCols - 2) + "╢");

            foreach (var h in hardwares)
            {
                Console.SetCursorPosition(colunaInicial, Console.CursorTop);
                Console.Write("║ ");
                Console.Write(h.Id.ToString().PadRight(col[0]));
                Console.Write(h.Tipo.PadRight(col[1]));
                Console.Write(h.Marca.PadRight(col[2]));
                Console.Write(h.Modelo.PadRight(col[3]));
                Console.Write(h.NumeroSerie.PadRight(col[4]));
                Console.Write(h.Status.PadRight(col[5]));
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
            tela4.MontarMolduraCentralizada("Excluir Hardware");

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
                Console.SetCursorPosition(col, lin + 2);
                Console.WriteLine("ID inválido.");
                Console.WriteLine("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            var dao = new HardwareDAO();
            var hardware = dao.BuscarPorId(id);

            Console.Clear();

            if (hardware == null)
            {
                tela.MontarMolduraCentralizada("Hardware não encontrado.");
                Console.SetCursorPosition(col + 2, lin + 6);
                Console.Write("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            tela4.MontarMolduraCentralizada("Confirmação de Exclusão");

            Console.SetCursorPosition(col, lin);
            Console.WriteLine($"ID: {hardware.Id}");
            Console.SetCursorPosition(col, lin + 1);
            Console.WriteLine($"TIPO: {hardware.Tipo}");
            Console.SetCursorPosition(col, lin + 2);
            Console.WriteLine($"MARCA: {hardware.Marca}");
            Console.SetCursorPosition(col, lin + 3);
            Console.WriteLine($"MODELO: {hardware.Modelo}");
            Console.SetCursorPosition(col, lin + 4);
            Console.WriteLine($"Nº SÉRIE: {hardware.NumeroSerie}");
            Console.SetCursorPosition(col, lin + 5);
            Console.WriteLine($"STATUS: {hardware.Status}");

            Console.SetCursorPosition(col, lin + 7);
            Console.WriteLine("1 - Confirmar exclusão");
            Console.SetCursorPosition(col, lin + 8);
            Console.WriteLine("0 - Voltar");

            Console.SetCursorPosition(col, lin + 10);
            Console.Write("Escolha uma opção: ");
            string? opcao = Console.ReadLine()?.Trim();

            if (opcao == "1")
            {
                bool sucesso = dao.Excluir(hardware.Id);
                Console.Clear();
                if (sucesso)
                    tela.MontarMolduraCentralizada("Hardware excluído com sucesso!");
                else
                    tela.MontarMolduraCentralizada("Erro ao excluir hardware!");

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
        public int Contar()
        {
            return dao.Listar().Count;
        }
    }
}
