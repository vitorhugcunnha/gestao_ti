using GestaoCore.Models;

Tela tela = new Tela(50, 10);
Tela tela2 = new Tela(60, 30);
Tela tela3 = new Tela(80, 10);
Tela tela4 = new Tela(60, 20);
chamadaCRUD chamada = new chamadaCRUD();
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
                bool sair = chamada.chamarCRUD(tela, tela2, tela3, tela4);
                if (sair)
                    return;
            }
            break;
        case "2":
            tela.TelaSair();
            return;
        default:
            tela.AlternativaIncorreta();
            break;
    }
}