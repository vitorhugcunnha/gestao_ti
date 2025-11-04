using GestaoCore.Models;

Tela tela = new Tela(50, 10);
Tela tela2 = new Tela(60, 30);
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
                bool sair = chamada.chamarCRUD(tela, tela2);
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
