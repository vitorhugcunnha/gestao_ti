using MySql.Data.MySqlClient;
using System;

namespace GestaoCore.dao
{
    public class Hardware
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = "";
        public string Marca { get; set; } = "";
        public string Modelo { get; set; } = "";
        public string NumeroSerie { get; set; } = "";
        public string Status { get; set; } = "em_estoque";
    }

    public class HardwareDAO
    {
        private readonly string connectionString =
            "server=localhost;database=gestao_ti;user=root;password=;";

        public bool Inserir(Hardware hardware)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    string sql = @"INSERT INTO hardware (tipo, marca, modelo, numero_serie, status) 
                    VALUES (@tipo, @marca, @modelo, @numero_serie, @status)";

                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@tipo", hardware.Tipo);
                        cmd.Parameters.AddWithValue("@marca", hardware.Marca);
                        cmd.Parameters.AddWithValue("@modelo", hardware.Modelo);
                        cmd.Parameters.AddWithValue("@numero_serie", hardware.NumeroSerie);
                        cmd.Parameters.AddWithValue("@status", hardware.Status);

                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir hardware: {ex.Message}");
                return false;
            }
        }
    }
}
