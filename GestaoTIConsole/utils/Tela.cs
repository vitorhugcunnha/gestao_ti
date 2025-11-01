public class Tela : GestaoCore.Models.Tela
{
    private int largura;
    private int altura;


    public Tela(int largura, int altura)
    {
        this.largura = largura;
        this.altura = altura;
    }
    public Tela(int largura, int altura, int coluna, int linha)
    {
        this.largura = largura;
        this.altura = altura;
    }
    public string MolduraMenu()
    {
        Console.Clear();
        MontarMolduraCentralizada("Gestao de TI");
        int col = (Console.WindowWidth / 2) - 15;
        int lin = (Console.WindowHeight / 2) - 1;


        Console.SetCursorPosition(col, lin);
        Console.WriteLine("1 - Login");
        Console.SetCursorPosition(col, lin + 1);
        Console.WriteLine("2 - Sair");
        Console.SetCursorPosition(col, lin + 3);
        Console.Write("Opcao: ");
        string opcao = Console.ReadLine();

        return opcao;
    }

    public void MontarMolduraCentralizada(string titulo = "")
    {
        int larguraConsole = Console.WindowWidth;
        int alturaConsole = Console.WindowHeight;

        int larguraMoldura = this.largura;
        int alturaMoldura = this.altura;

        int ci = (larguraConsole - larguraMoldura) / 2;
        int li = (alturaConsole - alturaMoldura) / 2;
        int cf = ci + larguraMoldura;
        int lf = li + alturaMoldura;


        for (int col = ci; col < cf; col++)
        {
            Console.SetCursorPosition(col, li);
            Console.Write("═");
            Console.SetCursorPosition(col, lf);
            Console.Write("═");
        }

        for (int lin = li; lin < lf; lin++)
        {
            Console.SetCursorPosition(ci, lin);
            Console.Write("║");
            Console.SetCursorPosition(cf, lin);
            Console.Write("║");
        }

        Console.SetCursorPosition(ci, li);
        Console.Write("╔");
        Console.SetCursorPosition(cf, li);
        Console.Write("╗");
        Console.SetCursorPosition(ci, lf);
        Console.Write("╚");
        Console.SetCursorPosition(cf, lf);
        Console.Write("╝");

        if (!string.IsNullOrEmpty(titulo))
        {
            int colTitulo = ci + (larguraMoldura - titulo.Length) / 2;
            Console.SetCursorPosition(colTitulo, li + 1);
            Console.Write(titulo);
        }
    }

    public void TelaLogin(out string usuario, out string senha)
    {
        Console.Clear();
        MontarMolduraCentralizada("Login do Sistema");

        int col = (Console.WindowWidth / 2) - 15;
        int lin = (Console.WindowHeight / 2) - 1;

        Console.SetCursorPosition(col, lin);
        Console.Write("Usuario: ");
        usuario = Console.ReadLine()?.Trim() ?? "";

        Console.SetCursorPosition(col, lin + 2);
        Console.Write("Senha: ");
        senha = "";

        ConsoleKeyInfo tecla;
        do
        {
            tecla = Console.ReadKey(true);
            if (tecla.Key != ConsoleKey.Backspace && tecla.Key != ConsoleKey.Enter)
            {
                senha += tecla.KeyChar;
                Console.Write("*");
            }
            else if (tecla.Key == ConsoleKey.Backspace && senha.Length > 0)
            {
                senha = senha.Substring(0, senha.Length - 1);
                Console.Write("\b \b");
            }
        } while (tecla.Key != ConsoleKey.Enter);
    }
    public void TelaSair()
    {
        Console.Clear();
        MontarMolduraCentralizada("Encerrando o Programa...");
        Thread.Sleep(2000);
        Console.Clear();
    }

    public void AlternativaIncorreta()
    {
        Console.Clear();
        MontarMolduraCentralizada("Alternativa Incorreta!");
        Console.ReadKey();
    }

    public void falhaAutenticacao()
    {
        Console.Clear();
        MontarMolduraCentralizada("Usuario ou senha incorretos!");
        Console.ReadKey();
        Console.Clear();
    }

    public void sucessoAutenticacao()
    {
        Console.Clear();
        MontarMolduraCentralizada("Login realizado com sucesso!");
        Thread.Sleep(1500);
        Console.Clear();
        MontarMolduraCentralizada("Bem-vindo ao Sistema Gestao de TI!");
        Thread.Sleep(1500);
        Console.Clear();
    }
}