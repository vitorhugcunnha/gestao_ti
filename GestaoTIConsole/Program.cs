using GestaoCore.Models;

Tela tela = new Tela(50, 10);
Tela tela2 = new Tela(60, 30);
autenticacaoUsuario autenticacao = new autenticacaoUsuario(tela);

while (true)
{
    string opcao = tela.MolduraMenu();
    switch (opcao)
    {
        case "1":
            if (autenticacao.autenticar())
            {
                tela.sucessoAutenticacao();
            }
            tela2.MontarMolduraCentralizada("Sistema");
            Console.ReadKey();
            break;
        case "2":
            tela.TelaSair();
            return;
        default:
            tela.AlternativaIncorreta();
            break;
    }
}