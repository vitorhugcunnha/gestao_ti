using GestaoCore.Models;
using GestaoCore.dao;

namespace GestaoCore.crud
{
    public class crudAlocar
    {
        private ITela tela;
        private ITela tela2;
        private ITela tela3;
        private ITela tela4;

        private alocarDAO dao;
        private ColaboradorDAO colaboradorDAO;
        private HardwareDAO hardwareDAO;
        private SoftwareDAO softwareDAO;

        public crudAlocar(ITela tela, ITela tela2, ITela tela3, ITela tela4)
        {
            this.tela = tela;
            this.tela2 = tela2;
            this.tela3 = tela3;
            this.tela4 = tela4;

            dao = new alocarDAO();
            colaboradorDAO = new ColaboradorDAO();
            hardwareDAO = new HardwareDAO();
            softwareDAO = new SoftwareDAO();
        }

        public void Criar()
        {
            Console.Clear();
            tela4.MontarMolduraCentralizada("Nova Alocação");

            int col = (Console.WindowWidth / 2) - 25;
            int lin = (Console.WindowHeight / 2) - 6;

            Console.SetCursorPosition(col, lin);
            Console.Write("ID do Colaborador: ");
            string colabInput = Console.ReadLine()?.Trim() ?? "";

            if (!int.TryParse(colabInput, out int idColaborador))
            {
                tela4.MontarMolduraCentralizada("ID inválido!");
                Console.ReadKey();
                return;
            }

            var colaborador = colaboradorDAO.BuscarPorId(idColaborador);
            if (colaborador == null)
            {
                tela4.MontarMolduraCentralizada("Colaborador não encontrado!");
                Console.ReadKey();
                return;
            }

            Console.SetCursorPosition(col, lin + 2);
            Console.Write("ID do Hardware (ou deixe vazio): ");
            string hardInput = Console.ReadLine()?.Trim() ?? "";
            int? idHardware = null;
            if (int.TryParse(hardInput, out int idHardTemp))
            {
                if (hardwareDAO.BuscarPorId(idHardTemp) != null)
                    idHardware = idHardTemp;
                else
                {
                    tela4.MontarMolduraCentralizada("Hardware não encontrado!");
                    Console.ReadKey();
                    return;
                }
            }

            Console.SetCursorPosition(col, lin + 4);
            Console.Write("ID do Software (ou deixe vazio): ");
            string softInput = Console.ReadLine()?.Trim() ?? "";
            int? idSoftware = null;
            if (int.TryParse(softInput, out int idSoftTemp))
            {
                if (softwareDAO.BuscarPorId(idSoftTemp) != null)
                    idSoftware = idSoftTemp;
                else
                {
                    tela4.MontarMolduraCentralizada("Software não encontrado!");
                    Console.ReadKey();
                    return;
                }
            }

            Console.SetCursorPosition(col, lin + 6);
            Console.Write("Data de Retorno (dd/MM/yyyy ou deixe vazio): ");
            string dataRetornoInput = Console.ReadLine()?.Trim() ?? "";
            DateTime? dataRetorno = null;
            if (!string.IsNullOrEmpty(dataRetornoInput))
            {
                if (DateTime.TryParse(dataRetornoInput, out DateTime parsedDate))
                {
                    dataRetorno = parsedDate;
                }
                else
                {
                    tela4.MontarMolduraCentralizada("Data inválida! Use o formato dd/MM/yyyy.");
                    Console.ReadKey();
                    return;
                }
            }

            Console.SetCursorPosition(col, lin + 8);
            Console.Write("Descrição do Retorno (opcional): ");
            string descricaoRetorno = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(descricaoRetorno))
                descricaoRetorno = null;

            var alocacao = new Alocacao
            {
                IdColaborador = idColaborador,
                IdHardware = idHardware,
                IdSoftware = idSoftware,
                DataAlocacao = DateTime.Now,
                DataRetorno = dataRetorno,
                DescricaoRetorno = descricaoRetorno,
                Status = "em_uso"
            };

            bool sucesso = dao.Inserir(alocacao);

            Console.Clear();
            tela4.MontarMolduraCentralizada(sucesso ? "Alocação registrada com sucesso!" : "Erro ao registrar alocação!");
            Console.ReadKey();
        }

        public void Alterar()
        {
            Console.Clear();
            tela4.MontarMolduraCentralizada("Registrar Retorno de Alocação");

            int col = (Console.WindowWidth / 2) - 25;
            int lin = (Console.WindowHeight / 2) - 6;

            Console.SetCursorPosition(col, lin);
            Console.Write("ID da Alocação: ");
            string? entrada = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(entrada) || entrada == "0")
            {
                Voltar();
                return;
            }

            if (!int.TryParse(entrada, out int id))
            {
                Console.Clear();
                tela4.MontarMolduraCentralizada("ID inválido.");
                Console.SetCursorPosition(col, lin + 2);
                Console.Write("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                Voltar();
                return;
            }

            var alocacao = dao.BuscarPorId(id);

            if (alocacao == null)
            {
                Console.Clear();
                tela4.MontarMolduraCentralizada("Alocação não encontrada.");
                Console.SetCursorPosition(col, lin + 2);
                Console.Write("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                Voltar();
                return;
            }

            var colaborador = colaboradorDAO.BuscarPorId(alocacao.IdColaborador);
            var hardware = alocacao.IdHardware.HasValue ? hardwareDAO.BuscarPorId(alocacao.IdHardware.Value) : null;
            var software = alocacao.IdSoftware.HasValue ? softwareDAO.BuscarPorId(alocacao.IdSoftware.Value) : null;

            Console.Clear();
            tela4.MontarMolduraCentralizada("Editar Alocação / Registrar Retorno");

            Console.SetCursorPosition(col, lin);
            Console.WriteLine($"ID: {alocacao.Id}");

            Console.SetCursorPosition(col, lin + 1);
            Console.WriteLine($"COLABORADOR: {colaborador?.Nome ?? "Desconhecido"}");

            Console.SetCursorPosition(col, lin + 2);
            Console.WriteLine($"HARDWARE: {(hardware != null ? $"{hardware.Marca} {hardware.Modelo}" : "-")}");

            Console.SetCursorPosition(col, lin + 3);
            Console.WriteLine($"SOFTWARE: {software?.ChaveLicenca ?? "-"}");

            Console.SetCursorPosition(col, lin + 5);
            Console.WriteLine($"DATA ALOCAÇÃO: {alocacao.DataAlocacao:yyyy-MM-dd}");

            Console.SetCursorPosition(col, lin + 7);
            Console.Write("Nova DESCRIÇÃO do retorno: ");
            string novaDescricao = Console.ReadLine()?.Trim() ?? "";

            alocacao.DataRetorno = DateTime.Now;
            if (!string.IsNullOrEmpty(novaDescricao))
                alocacao.DescricaoRetorno = novaDescricao;
            alocacao.Status = "retornado";

            Console.Clear();
            tela4.MontarMolduraCentralizada("Confirmação de Retorno");

            Console.SetCursorPosition(col, lin);
            Console.WriteLine($"ID: {alocacao.Id}");
            Console.SetCursorPosition(col, lin + 1);
            Console.WriteLine($"COLABORADOR: {colaborador?.Nome ?? "Desconhecido"}");
            Console.SetCursorPosition(col, lin + 2);
            Console.WriteLine($"HARDWARE: {(hardware != null ? $"{hardware.Marca} {hardware.Modelo}" : "-")}");
            Console.SetCursorPosition(col, lin + 3);
            Console.WriteLine($"SOFTWARE: {software?.ChaveLicenca ?? "-"}");
            Console.SetCursorPosition(col, lin + 4);
            Console.WriteLine($"DATA RETORNO: {alocacao.DataRetorno:yyyy-MM-dd HH:mm}");
            Console.SetCursorPosition(col, lin + 5);
            Console.WriteLine($"DESCRIÇÃO: {alocacao.DescricaoRetorno}");
            Console.SetCursorPosition(col, lin + 6);
            Console.WriteLine($"STATUS: {alocacao.Status}");

            Console.SetCursorPosition(col, lin + 8);
            Console.WriteLine("1 - Confirmar retorno");
            Console.SetCursorPosition(col, lin + 9);
            Console.WriteLine("0 - Cancelar e voltar");

            Console.SetCursorPosition(col, lin + 11);
            Console.Write("Escolha uma opção: ");
            string? opcao = Console.ReadLine()?.Trim();

            if (opcao == "1")
            {
                bool sucesso = dao.Atualizar(alocacao);
                Console.Clear();
                tela4.MontarMolduraCentralizada(sucesso
                    ? "Retorno registrado com sucesso!"
                    : "Erro ao registrar retorno!");
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
            var lista = dao.Listar();

            if (lista.Count == 0)
            {
                tela.MontarMolduraCentralizada("Nenhuma alocação encontrada.");
                Console.ReadKey();
                return;
            }

            string[] headers = { "ID", "COLABORADOR", "HARDWARE", "SOFTWARE", "DATA ALOCAÇÃO", "DATA RETORNO", "STATUS" };
            int[] col = new int[headers.Length];

            for (int i = 0; i < headers.Length; i++)
                col[i] = headers[i].Length;

            foreach (var a in lista)
            {
                var colaborador = colaboradorDAO.BuscarPorId(a.IdColaborador);
                var hardware = a.IdHardware.HasValue ? hardwareDAO.BuscarPorId(a.IdHardware.Value) : null;
                var software = a.IdSoftware.HasValue ? softwareDAO.BuscarPorId(a.IdSoftware.Value) : null;

                string nomeColab = colaborador?.Nome ?? "Desconhecido";
                string nomeHard = hardware != null ? $"{hardware.Marca} {hardware.Modelo}" : "-";
                string nomeSoft = software?.ChaveLicenca ?? "-";
                string dataAloc = a.DataAlocacao.ToString("yyyy-MM-dd");
                string dataRet = a.DataRetorno.HasValue ? a.DataRetorno.Value.ToString("yyyy-MM-dd") : "-";

                col[0] = Math.Max(col[0], a.Id.ToString().Length);
                col[1] = Math.Max(col[1], nomeColab.Length);
                col[2] = Math.Max(col[2], nomeHard.Length);
                col[3] = Math.Max(col[3], nomeSoft.Length);
                col[4] = Math.Max(col[4], dataAloc.Length);
                col[5] = Math.Max(col[5], dataRet.Length);
                col[6] = Math.Max(col[6], a.Status.Length);
            }

            for (int i = 0; i < col.Length; i++)
                col[i] += 2;

            int totalCols = col.Sum() + headers.Length + 2;
            int colunaInicial = Math.Max((Console.WindowWidth - totalCols) / 2, 0);
            int linhaInicial = Math.Max((Console.WindowHeight - (lista.Count + 8)) / 2, 0);

            Console.SetCursorPosition(colunaInicial, linhaInicial);
            Console.WriteLine("╔" + new string('═', totalCols - 2) + "╗");

            string titulo = "Lista de Alocações";
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

            foreach (var a in lista)
            {
                var colaborador = colaboradorDAO.BuscarPorId(a.IdColaborador);
                var hardware = a.IdHardware.HasValue ? hardwareDAO.BuscarPorId(a.IdHardware.Value) : null;
                var software = a.IdSoftware.HasValue ? softwareDAO.BuscarPorId(a.IdSoftware.Value) : null;

                string nomeColab = colaborador?.Nome ?? "Desconhecido";
                string nomeHard = hardware != null ? $"{hardware.Marca} {hardware.Modelo}" : "-";
                string nomeSoft = software?.ChaveLicenca ?? "-";
                string dataAloc = a.DataAlocacao.ToString("yyyy-MM-dd");
                string dataRet = a.DataRetorno.HasValue ? a.DataRetorno.Value.ToString("yyyy-MM-dd") : "-";

                Console.SetCursorPosition(colunaInicial, Console.CursorTop);
                Console.Write("║ ");
                Console.Write(a.Id.ToString().PadRight(col[0]));
                Console.Write(" " + nomeColab.PadRight(col[1]));
                Console.Write(" " + nomeHard.PadRight(col[2]));
                Console.Write(" " + nomeSoft.PadRight(col[3]));
                Console.Write(" " + dataAloc.PadRight(col[4]));
                Console.Write(" " + dataRet.PadRight(col[5]));
                Console.Write(" " + a.Status.PadRight(col[6]));
                Console.WriteLine(" ║");
            }

            Console.SetCursorPosition(colunaInicial, Console.CursorTop);
            Console.WriteLine("╚" + new string('═', totalCols - 2) + "╝");

            Console.ReadKey();
        }

        public void Deletar()
        {
            Console.Clear();
            tela4.MontarMolduraCentralizada("Excluir Alocação");

            int col = (Console.WindowWidth / 2) - 25;
            int lin = (Console.WindowHeight / 2) - 5;

            Console.SetCursorPosition(col, lin);
            Console.Write("ID da Alocação: ");
            string? entrada = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(entrada) || entrada == "0")
            {
                Voltar();
                return;
            }

            if (!int.TryParse(entrada, out int id))
            {
                Console.Clear();
                tela4.MontarMolduraCentralizada("ID inválido.");
                Console.SetCursorPosition(col, lin + 2);
                Console.Write("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                Voltar();
                return;
            }

            var alocacao = dao.BuscarPorId(id);

            if (alocacao == null)
            {
                Console.Clear();
                tela4.MontarMolduraCentralizada("Alocação não encontrada.");
                Console.SetCursorPosition(col, lin + 2);
                Console.Write("Pressione qualquer tecla para voltar...");
                Console.ReadKey();
                Voltar();
                return;
            }

            var colaborador = colaboradorDAO.BuscarPorId(alocacao.IdColaborador);
            var hardware = alocacao.IdHardware.HasValue ? hardwareDAO.BuscarPorId(alocacao.IdHardware.Value) : null;
            var software = alocacao.IdSoftware.HasValue ? softwareDAO.BuscarPorId(alocacao.IdSoftware.Value) : null;

            string nomeColab = colaborador?.Nome ?? "Desconhecido";
            string nomeHard = hardware != null ? $"{hardware.Marca} {hardware.Modelo}" : "-";
            string nomeSoft = software?.ChaveLicenca ?? "-";
            string dataAloc = alocacao.DataAlocacao.ToString("yyyy-MM-dd");
            string dataRet = alocacao.DataRetorno.HasValue ? alocacao.DataRetorno.Value.ToString("yyyy-MM-dd") : "-";
            string status = alocacao.Status;

            Console.Clear();
            tela4.MontarMolduraCentralizada("Confirmação de Exclusão");

            Console.SetCursorPosition(col, lin);
            Console.WriteLine($"ID: {alocacao.Id}");
            Console.SetCursorPosition(col, lin + 1);
            Console.WriteLine($"COLABORADOR: {nomeColab}");
            Console.SetCursorPosition(col, lin + 2);
            Console.WriteLine($"HARDWARE: {nomeHard}");
            Console.SetCursorPosition(col, lin + 3);
            Console.WriteLine($"SOFTWARE: {nomeSoft}");
            Console.SetCursorPosition(col, lin + 4);
            Console.WriteLine($"DATA ALOCAÇÃO: {dataAloc}");
            Console.SetCursorPosition(col, lin + 5);
            Console.WriteLine($"DATA RETORNO: {dataRet}");
            Console.SetCursorPosition(col, lin + 6);
            Console.WriteLine($"STATUS: {status}");

            Console.SetCursorPosition(col, lin + 8);
            Console.WriteLine("1 - Confirmar exclusão");
            Console.SetCursorPosition(col, lin + 9);
            Console.WriteLine("0 - Voltar");

            Console.SetCursorPosition(col, lin + 11);
            Console.Write("Escolha uma opção: ");
            string? opcao = Console.ReadLine()?.Trim();

            if (opcao == "1")
            {
                bool sucesso = dao.Excluir(alocacao.Id);
                Console.Clear();
                tela4.MontarMolduraCentralizada(sucesso
                    ? "Alocação excluída com sucesso!"
                    : "Erro ao excluir alocação!");
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
            tela.MontarMolduraCentralizada("Retornando...");
            Thread.Sleep(1200);
            Console.Clear();
        }
    }
}
