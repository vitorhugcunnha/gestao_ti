using System;
using GestaoCore.Models;
using GestaoCore.dao;

namespace GestaoCore.crud
{
    public class CrudHardware
    {
        private ITela tela;
        private ITela tela2;
        private ITela tela3;
        private ITela tela4;

        public CrudHardware(ITela tela, ITela tela2, ITela tela3, ITela tela4)
        {
            this.tela = tela3;
            this.tela2 = tela2;
            this.tela3 = tela3;
            this.tela4 = tela4;
        }

        public void Criar()
        {
            Console.Clear();
            tela.MontarMolduraCentralizada("Cadastro de Hardware");

            int col = (Console.WindowWidth / 2) - 25;
            int lin = (Console.WindowHeight / 2) - 3;

            Console.SetCursorPosition(col, lin);
            Console.Write("Tipo: ");
            Console.SetCursorPosition(col, lin + 2);
            Console.Write("Marca: ");
            Console.SetCursorPosition(col, lin + 4);
            Console.Write("Modelo: ");
            Console.SetCursorPosition(col, lin + 6);
            Console.Write("Número de Série: ");

            Console.SetCursorPosition(col + 7, lin);
            string tipo = Console.ReadLine()?.Trim() ?? "";

            Console.SetCursorPosition(col + 8, lin + 2);
            string marca = Console.ReadLine()?.Trim() ?? "";

            Console.SetCursorPosition(col + 9, lin + 4);
            string modelo = Console.ReadLine()?.Trim() ?? "";

            Console.SetCursorPosition(col + 17, lin + 6);
            string numeroSerie = Console.ReadLine()?.Trim() ?? "";

            while (Console.KeyAvailable) Console.ReadKey(true);

            var hardware = new Hardware
            {
                Tipo = tipo,
                Marca = marca,
                Modelo = modelo,
                NumeroSerie = numeroSerie
            };

            var dao = new HardwareDAO();
            bool sucesso = dao.Inserir(hardware);

            Console.Clear();
            tela.MontarMolduraCentralizada("Cadastro Concluído");

            int colConf = (Console.WindowWidth / 2) - 25;
            int linConf = (Console.WindowHeight / 2) - 4;

            Console.SetCursorPosition(colConf, linConf);

            if (sucesso)
            {
                Console.WriteLine("Hardware cadastrado com sucesso!\n");
            }
            else
            {
                Console.WriteLine("Erro ao cadastrar hardware.\n");
            }

            Console.SetCursorPosition(colConf, linConf + 2);
            Console.WriteLine($"Tipo: {tipo}");
            Console.SetCursorPosition(colConf, linConf + 3);
            Console.WriteLine($"Marca: {marca}");
            Console.SetCursorPosition(colConf, linConf + 4);
            Console.WriteLine($"Modelo: {modelo}");
            Console.SetCursorPosition(colConf, linConf + 5);
            Console.WriteLine($"Nº Série: {numeroSerie}");

            Console.SetCursorPosition(colConf, linConf + 7);
            Console.Write("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void Alterar()
        {


        }

        public void Listar()
        {

        }

        public void Deletar()
        {

        }

        public void Voltar()
        {
            Console.Clear();
            tela.MontarMolduraCentralizada("Retornando ao menu anterior...");
            Thread.Sleep(1500);
            Console.Clear();
        }
    }
}
