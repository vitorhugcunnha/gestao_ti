using GestaoCore.Models;
using GestaoCore.dao;

namespace GestaoCore.crud
{
    public class crudSoftware
    {
        private ITela tela;
        private ITela tela2;
        private ITela tela3;
        private ITela tela4;

        private SoftwareDAO dao;
        private LicencaDAO licencaDAO;
        private Software? softwareAtual;

        public crudSoftware(ITela tela, ITela tela2, ITela tela3, ITela tela4)
        {
            this.tela = tela;
            this.tela2 = tela2;
            this.tela3 = tela3;
            this.tela4 = tela4;

            dao = new SoftwareDAO();
            licencaDAO = new LicencaDAO();
        }
        public void Criar()
        {
            Console.Clear();
            tela.MontarMolduraCentralizada("Cadastro de Software");

            int col = (Console.WindowWidth / 2) - 25;
            int lin = (Console.WindowHeight / 2) - 4;

            Console.SetCursorPosition(col, lin);
            Console.WriteLine("V - Voltar");
            Console.SetCursorPosition(col, lin + 2);
            Console.Write("ID da Licença: ");
            Console.SetCursorPosition(col, lin + 4);
            Console.Write("Chave da Licença: ");

            Console.SetCursorPosition(col + 17, lin + 2);

            string idLicencaInput = Console.ReadLine()?.Trim() ?? "";
            if (idLicencaInput.Equals("V", StringComparison.OrdinalIgnoreCase))
            {
                Console.Clear();
                tela.MontarMolduraCentralizada("Voltando ao menu anterior...");
                System.Threading.Thread.Sleep(1200);
                Console.Clear();
                return;
            }

            if (!int.TryParse(idLicencaInput, out int idLicenca))
            {
                tela4.MontarMolduraCentralizada("ID de licença inválido!");
                Console.ReadKey();
                return;
            }

            var licenca = licencaDAO.BuscarPorId(idLicenca);
            if (licenca == null)
            {
                Console.Clear();
                tela4.MontarMolduraCentralizada("Licença não encontrada!");
                Console.ReadKey();
                return;
            }

            Console.SetCursorPosition(col + 20, lin + 4);
            string chaveLicenca = Console.ReadLine()?.Trim() ?? "";

            var software = new Software
            {
                IdLicenca = idLicenca,
                ChaveLicenca = chaveLicenca,
                DataAquisicao = DateTime.Now.Date
            };

            bool sucesso = dao.Inserir(software);

            Console.Clear();
            if (sucesso)
                tela4.MontarMolduraCentralizada("Software cadastrado com sucesso!");
            else
                tela4.MontarMolduraCentralizada("Erro ao cadastrar software!");

            int colConf = (Console.WindowWidth / 2) - 25;
            int linConf = (Console.WindowHeight / 2) - 4;

            Console.SetCursorPosition(colConf, linConf + 2);
            Console.WriteLine($"Licença: {(licenca is null ? "N/A" : (licenca.Nome ?? ""))}");
            Console.SetCursorPosition(colConf, linConf + 3);
            Console.WriteLine($"Chave: {software.ChaveLicenca}");
            Console.SetCursorPosition(colConf, linConf + 4);
            Console.WriteLine($"Data Aquisição: {software.DataAquisicao:yyyy-MM-dd}");

            Console.SetCursorPosition(colConf, linConf + 6);
            Console.Write("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }


        public void Alterar()
        {
            Console.Clear();
            tela4.MontarMolduraCentralizada("Alterar Software");

            int col = (Console.WindowWidth / 2) - 25;
            int lin = (Console.WindowHeight / 2) - 5;

            Console.SetCursorPosition(col, lin);
            Console.Write("ID do Software: ");
            string idInput = Console.ReadLine()?.Trim() ?? "";
            if (!int.TryParse(idInput, out int id))
            {
                tela.MontarMolduraCentralizada("ID inválido.");
                Console.ReadKey();
                return;
            }

            softwareAtual = dao.BuscarPorId(id);
            if (softwareAtual == null)
            {
                tela.MontarMolduraCentralizada("Software não encontrado.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            tela4.MontarMolduraCentralizada("Editar Software");

            Console.SetCursorPosition(col, lin);
            Console.Write($"ID: {softwareAtual.Id}");
            Console.SetCursorPosition(col, lin + 2);
            Console.Write($"ID Licença atual: {softwareAtual.IdLicenca}");
            Console.SetCursorPosition(col + 35, lin + 2);
            Console.Write("Nova ID Licença: ");
            string novaLicencaStr = Console.ReadLine()?.Trim() ?? "";

            Console.SetCursorPosition(col, lin + 4);
            Console.Write($"Chave atual: {softwareAtual.ChaveLicenca}");
            Console.SetCursorPosition(col + 35, lin + 4);
            Console.Write("Nova Chave: ");
            string novaChave = Console.ReadLine()?.Trim() ?? "";

            if (!string.IsNullOrEmpty(novaLicencaStr) && int.TryParse(novaLicencaStr, out int novaLicencaId))
            {
                var lic = licencaDAO.BuscarPorId(novaLicencaId);
                if (lic != null)
                    softwareAtual.IdLicenca = novaLicencaId;
                else
                {
                    tela.MontarMolduraCentralizada("Licença nova não encontrada!");
                    Console.ReadKey();
                    return;
                }
            }

            if (!string.IsNullOrEmpty(novaChave))
                softwareAtual.ChaveLicenca = novaChave;

            bool sucesso = dao.Atualizar(softwareAtual);

            Console.Clear();
            tela.MontarMolduraCentralizada(sucesso
                ? "Software atualizado com sucesso!"
                : "Erro ao atualizar software!");
            Console.ReadKey();
        }

        public void Listar()
        {
            Console.Clear();
            var lista = dao.Listar();
            if (lista.Count == 0)
            {
                tela.MontarMolduraCentralizada("Nenhum software cadastrado.");
                Console.ReadKey();
                return;
            }

            string[] headers = { "ID", "ID LICENÇA", "CHAVE LICENÇA", "DATA AQUISIÇÃO" };
            int[] col = new int[headers.Length];

            for (int i = 0; i < headers.Length; i++)
                col[i] = headers[i].Length;


            foreach (var s in lista)
            {
                col[0] = Math.Max(col[0], s.Id.ToString().Length);
                col[1] = Math.Max(col[1], s.IdLicenca.ToString().Length);
                col[2] = Math.Max(col[2], (s.ChaveLicenca ?? "").Length);
                col[3] = Math.Max(col[3], s.DataAquisicao.ToString("yyyy-MM-dd").Length);
            }


            for (int i = 0; i < col.Length; i++) col[i] += 2;

            int totalCols = col.Sum() + headers.Length + 2;
            int colunaInicial = Math.Max((Console.WindowWidth - totalCols) / 2, 0);
            int linhaInicial = Math.Max((Console.WindowHeight - (lista.Count + 8)) / 2, 0);


            Console.SetCursorPosition(colunaInicial, linhaInicial);
            Console.WriteLine("╔" + new string('═', totalCols - 2) + "╗");


            string titulo = "Lista de Softwares";
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


            foreach (var s in lista)
            {
                Console.SetCursorPosition(colunaInicial, Console.CursorTop);
                Console.Write("║ ");
                Console.Write(s.Id.ToString().PadRight(col[0]));
                Console.Write(" " + s.IdLicenca.ToString().PadRight(col[1]));
                Console.Write(" " + (s.ChaveLicenca ?? "").PadRight(col[2]));
                Console.Write(" " + s.DataAquisicao.ToString("yyyy-MM-dd").PadRight(col[3]));
                Console.WriteLine(" ║");
            }

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("╠" + new string('═', totalCols - 2) + "╣");
            string mensagem = "Pressione qualquer tecla para continuar...";
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
            tela4.MontarMolduraCentralizada("Excluir Software");

            int col = (Console.WindowWidth / 2) - 25;
            int lin = (Console.WindowHeight / 2) - 5;

            Console.SetCursorPosition(col, lin);
            Console.Write("ID do Software: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                tela.MontarMolduraCentralizada("ID inválido.");
                Console.ReadKey();
                return;
            }

            var software = dao.BuscarPorId(id);
            if (software == null)
            {
                tela.MontarMolduraCentralizada("Software não encontrado.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            tela4.MontarMolduraCentralizada("Confirmação de Exclusão");
            Console.SetCursorPosition(col, lin);
            Console.WriteLine($"\nID: {software.Id}");
            Console.SetCursorPosition(col, lin + 1);
            Console.WriteLine($"ID Licença: {software.IdLicenca}");
            Console.SetCursorPosition(col, lin + 2);
            Console.WriteLine($"Chave: {software.ChaveLicenca}");
            Console.SetCursorPosition(col, lin + 3);
            Console.WriteLine($"Data Aquisição: {software.DataAquisicao:yyyy-MM-dd}");

            Console.SetCursorPosition(col, lin + 5);
            Console.WriteLine("1 - Confirmar exclusão");
            Console.SetCursorPosition(col, lin + 6);
            Console.WriteLine("0 - Voltar");

            Console.SetCursorPosition(col, lin + 8);
            Console.Write("Escolha: ");
            string? opcao = Console.ReadLine()?.Trim();

            if (opcao == "1")
            {
                bool sucesso = dao.Excluir(software.Id);
                tela.MontarMolduraCentralizada(sucesso
                    ? "Software excluído com sucesso!"
                    : "Erro ao excluir software!");
                Console.ReadKey();
            }
        }

        public void Voltar()
        {
            Console.Clear();
            tela.MontarMolduraCentralizada("Retornando ao menu anterior...");
            System.Threading.Thread.Sleep(1200);
            Console.Clear();
        }
    }
}