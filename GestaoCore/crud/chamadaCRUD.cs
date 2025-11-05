// ...existing code...
using System;
using GestaoCore.Models;
using GestaoCore.crud;

namespace GestaoCore.Models
{
    public class chamadaCRUD
    {
        public bool chamarCRUD(ITela tela, ITela tela2, ITela tela3, ITela tela4)
        {
            while (true)
            {
                string opcaoCrud = tela2.telaCrud();

                switch (opcaoCrud)
                {
                    case "1":
                        var crudHardware = new CrudHardware(tela, tela2, tela3, tela4);
                        bool voltarDoHardware = false;

                        while (!voltarDoHardware)
                        {
                            string opcaoSecundaria = tela3.telaCrudSecundaria("Hardware");

                            switch (opcaoSecundaria)
                            {
                                case "C":
                                    crudHardware.Criar();
                                    break;
                                case "A":
                                    crudHardware.Alterar();
                                    break;
                                case "L":
                                    crudHardware.Listar();
                                    break;
                                case "D":
                                    crudHardware.Deletar();
                                    break;
                                case "V":
                                    crudHardware.Voltar();
                                    voltarDoHardware = true;
                                    break;
                                default:
                                    tela.AlternativaIncorreta();
                                    break;
                            }
                        }
                        break;

                    case "2":
                        tela3.telaCrudSecundaria("Software");
                        break;

                    case "3":
                        tela3.telaCrudSecundaria("Licenca");
                        break;

                    case "4":
                        tela3.telaCrudSecundaria("Colaborador");
                        break;

                    case "5":
                        tela3.telaCrudSecundaria("Alocar ou Retornar um Hardware ou Software");
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
// ...existing code...