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

                        var crudSoftware = new crudSoftware(tela, tela2, tela3, tela4);
                        bool voltarDoSoftware = false;

                        while (!voltarDoSoftware)
                        {
                            string opcaoSecundaria = tela3.telaCrudSecundaria("Software");

                            switch (opcaoSecundaria)
                            {
                                case "C":
                                    crudSoftware.Criar();
                                    break;
                                case "A":
                                    crudSoftware.Alterar();
                                    break;
                                case "L":
                                    crudSoftware.Listar();
                                    break;
                                case "D":
                                    crudSoftware.Deletar();
                                    break;
                                case "V":
                                    crudSoftware.Voltar();
                                    voltarDoSoftware = true;
                                    break;
                                default:
                                    tela.AlternativaIncorreta();
                                    break;
                            }
                        }
                        break;
                    case "3":
                        var crudLicenca = new CrudLicenca(tela, tela2, tela3, tela4);
                        bool voltarDaLicenca = false;

                        while (!voltarDaLicenca)
                        {
                            string opcaoSecundaria = tela3.telaCrudSecundaria("Licenca");

                            switch (opcaoSecundaria)
                            {
                                case "C":
                                    crudLicenca.Criar();
                                    break;
                                case "A":
                                    crudLicenca.Alterar();
                                    break;
                                case "L":
                                    crudLicenca.Listar();
                                    break;
                                case "D":
                                    crudLicenca.Deletar();
                                    break;
                                case "V":
                                    crudLicenca.Voltar();
                                    voltarDaLicenca = true;
                                    break;
                                default:
                                    tela.AlternativaIncorreta();
                                    break;
                            }
                        }
                        break;

                    case "4":
                        var crudColaborador = new CrudColaborador(tela, tela2, tela3, tela4);
                        bool voltarDoColaborador = false;

                        while (!voltarDoColaborador)
                        {
                            string opcaoSecundaria = tela3.telaCrudSecundaria("Colaborador");

                            switch (opcaoSecundaria)
                            {
                                case "C":
                                    crudColaborador.Criar();
                                    break;
                                case "A":
                                    crudColaborador.Alterar();
                                    break;
                                case "L":
                                    crudColaborador.Listar();
                                    break;
                                case "D":
                                    crudColaborador.Deletar();
                                    break;
                                case "V":
                                    crudColaborador.Voltar();
                                    voltarDoColaborador = true;
                                    break;
                                default:
                                    tela.AlternativaIncorreta();
                                    break;
                            }
                        }
                        break;

                    case "5":
                        var crudAlocar = new crudAlocar(tela, tela2, tela3, tela4);
                        bool voltarDaAlocacao = false;
                        while (!voltarDaAlocacao)
                        {
                            string opcaoSecundaria = tela3.telaCrudSecundaria("Alocar TI");

                            switch (opcaoSecundaria)
                            {
                                case "C":
                                    crudAlocar.Criar();
                                    break;
                                case "A":
                                    crudAlocar.Alterar();
                                    break;
                                case "L":
                                    crudAlocar.Listar();
                                    break;
                                case "D":
                                    crudAlocar.Deletar();
                                    break;
                                case "V":
                                    crudAlocar.Voltar();
                                    voltarDaAlocacao = true;
                                    break;
                                default:
                                    tela.AlternativaIncorreta();
                                    break;
                            }
                        }
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
