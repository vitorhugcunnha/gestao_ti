using System;

namespace GestaoCore.Models
{
    public class chamadaCRUD
    {
        public bool chamarCRUD(ITela tela, ITela tela2)
        {
            while (true)
            {
                string opcaoCrud = tela2.telaCrud();

                switch (opcaoCrud)
                {
                    case "1":
                        // telaHardware();
                        break;
                    case "2":
                        // telaSoftware();
                        break;
                    case "3":
                        // telaLicenca();
                        break;
                    case "4":
                        // telaColaborador();
                        break;
                    case "5":
                        // telaAlocarRetorno();
                        break;
                    case "6":
                        tela.TelaSair();
                        return true;
                    default:
                        tela.AlternativaIncorreta();
                        break;
                }
            }
        }
    }
}