using System;
using GestaoCore.crud;

namespace GestaoCore.Models
{
    public class Alert
    {
        private CrudHardware crudHardware;
        private CrudLicenca crudLicenca;

        public Alert(ITela tela, ITela tela2, ITela tela3, ITela tela4)
        {
            crudHardware = new CrudHardware(tela, tela2, tela3, tela4);
            crudLicenca = new CrudLicenca(tela, tela2, tela3, tela4);
        }

        private void DesenharCaixaDireita(string texto, int linha)
        {
            int largura = texto.Length + 4;
            int coluna = Console.WindowWidth - largura - 2;

            Console.SetCursorPosition(coluna, linha);
            Console.Write("╔" + new string('═', largura) + "╗");

            Console.SetCursorPosition(coluna, linha + 1);
            Console.Write("║ " + texto.PadRight(largura - 2) + " ║");

            Console.SetCursorPosition(coluna, linha + 2);
            Console.Write("╚" + new string('═', largura) + "╝");
        }

        public void VerificarEstoque()
        {
            try
            {
                int qtdHardware = crudHardware.Contar();
                int qtdLicenca = crudLicenca.Contar();

                int linha = 1;

                if (qtdHardware < 10)
                {
                    DesenharCaixaDireita("⚠️ Baixo estoque de Hardware", linha);
                    linha += 4;
                }

                if (qtdLicenca < 10)
                {
                    DesenharCaixaDireita("⚠️ Baixo estoque de Licenças", linha);
                    linha += 4;
                }
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(2, 2);
                Console.WriteLine($"Erro ao verificar estoque: {ex.Message}");
            }
        }
    }
}
