Tela tela = new Tela(50, 10);

while (true)
{
    string opcao = tela.MolduraMenu();
    switch (opcao)
    {
        case "1":
            string usuario, senha;
            tela.TelaLogin(out usuario, out senha);
            break;
        case "2":
            tela.TelaSair();
            return;
        default:
            tela.AlternativaIncorreta();
            break;
    }
}