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

        public List<Hardware> Listar()
        {
            var lista = new List<Hardware>();

            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    string sql = "SELECT id, tipo, marca, modelo, numero_serie, status FROM hardware";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Hardware
                            {
                                Id = reader.GetInt32("id"),
                                Tipo = reader.GetString("tipo"),
                                Marca = reader.GetString("marca"),
                                Modelo = reader.GetString("modelo"),
                                NumeroSerie = reader.GetString("numero_serie"),
                                Status = reader.GetString("status")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar hardware: {ex.Message}");
            }

            return lista;
        }

        public Hardware? BuscarPorId(int id)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();
                    string sql = "SELECT id, tipo, marca, modelo, numero_serie, status FROM hardware WHERE id = @id";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Hardware
                                {
                                    Id = reader.GetInt32("id"),
                                    Tipo = reader.GetString("tipo"),
                                    Marca = reader.GetString("marca"),
                                    Modelo = reader.GetString("modelo"),
                                    NumeroSerie = reader.GetString("numero_serie"),
                                    Status = reader.GetString("status")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar hardware: {ex.Message}");
            }

            return null;
        }

        public bool Excluir(int id)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();
                    string sql = "DELETE FROM hardware WHERE id = @id";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir hardware: {ex.Message}");
                return false;
            }
        }
        public bool Atualizar(Hardware hardware)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    string sql = @"UPDATE hardware SET tipo = @tipo, 
                               marca = @marca, modelo = @modelo, numero_serie = @numero_serie, status = @status
                           WHERE id = @id";

                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@tipo", hardware.Tipo);
                        cmd.Parameters.AddWithValue("@marca", hardware.Marca);
                        cmd.Parameters.AddWithValue("@modelo", hardware.Modelo);
                        cmd.Parameters.AddWithValue("@numero_serie", hardware.NumeroSerie);
                        cmd.Parameters.AddWithValue("@status", hardware.Status);
                        cmd.Parameters.AddWithValue("@id", hardware.Id);

                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar hardware: {ex.Message}");
                return false;
            }
        }

    }
}
