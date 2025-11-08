using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace GestaoCore.dao
{
    public class Licenca
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public DateTime DataValidade { get; set; }
    }

    public class LicencaDAO
    {
        private readonly string connectionString =
            "server=localhost;database=gestao_ti;user=root;password=;";

        public bool Inserir(Licenca licenca)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();
                    string sql = "INSERT INTO licenca (nome_software, data_validade) VALUES (@Nome, @DataValidade)";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Nome", licenca.Nome);
                        cmd.Parameters.AddWithValue("@DataValidade", licenca.DataValidade);

                        int linhas = cmd.ExecuteNonQuery();
                        return linhas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao inserir licença: {ex.Message}");
                Console.ResetColor();
                Console.ReadKey();
                return false;
            }
        }

        public List<Licenca> Listar()
        {
            var lista = new List<Licenca>();

            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    string sql = "SELECT id, nome_software, data_validade FROM licenca";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Licenca
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome_software"),
                                DataValidade = reader.GetDateTime("data_validade")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar licenças: {ex.Message}");
            }

            return lista;
        }

        public Licenca? BuscarPorId(int id)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    string sql = "SELECT id, nome_software, data_validade FROM licenca WHERE id = @id";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Licenca
                                {
                                    Id = reader.GetInt32("id"),
                                    Nome = reader.GetString("nome_software"),
                                    DataValidade = reader.GetDateTime("data_validade")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar licença: {ex.Message}");
            }

            return null;
        }

        public bool Atualizar(Licenca licenca)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    string sql = @"UPDATE licenca 
                                   SET nome_software = @nome, data_validade = @data_validade 
                                   WHERE id = @id";

                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@nome", licenca.Nome);
                        cmd.Parameters.AddWithValue("@data_validade", licenca.DataValidade);
                        cmd.Parameters.AddWithValue("@id", licenca.Id);

                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar licença: {ex.Message}");
                return false;
            }
        }

        public bool Excluir(int id)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    string sql = "DELETE FROM licenca WHERE id = @id";

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
                Console.WriteLine($"Erro ao excluir licença: {ex.Message}");
                return false;
            }
        }
    }
}