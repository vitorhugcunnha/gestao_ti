using MySql.Data.MySqlClient;

namespace GestaoCore.dao
{
    public class Software
    {
        public int Id { get; set; }
        public int IdLicenca { get; set; }
        public string ChaveLicenca { get; set; } = "";
        public DateTime DataAquisicao { get; set; }
    }

    public class SoftwareDAO
    {
        private readonly string connectionString =
            "server=localhost;database=gestao_ti;user=root;password=;";

        public bool Inserir(Software software)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();
                    string sql = @"INSERT INTO software (id_licenca, chave_licenca)
                                   VALUES (@id_licenca, @chave_licenca)";

                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id_licenca", software.IdLicenca);
                        cmd.Parameters.AddWithValue("@chave_licenca", software.ChaveLicenca);

                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir software: {ex.Message}");
                return false;
            }
        }

        public List<Software> Listar()
        {
            var lista = new List<Software>();

            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    string sql = @"SELECT id, id_licenca, chave_licenca, data_aquisicao 
                                   FROM software";

                    using (var cmd = new MySqlCommand(sql, conexao))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Software
                            {
                                Id = reader.GetInt32("id"),
                                IdLicenca = reader.GetInt32("id_licenca"),
                                ChaveLicenca = reader.GetString("chave_licenca"),
                                DataAquisicao = reader.GetDateTime("data_aquisicao")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar softwares: {ex.Message}");
            }

            return lista;
        }

        public Software? BuscarPorId(int id)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    string sql = @"SELECT id, id_licenca, chave_licenca, data_aquisicao 
                                   FROM software WHERE id = @id";

                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Software
                                {
                                    Id = reader.GetInt32("id"),
                                    IdLicenca = reader.GetInt32("id_licenca"),
                                    ChaveLicenca = reader.GetString("chave_licenca"),
                                    DataAquisicao = reader.GetDateTime("data_aquisicao")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar software: {ex.Message}");
            }

            return null;
        }

        public bool Atualizar(Software software)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();


                    string sql = @"UPDATE software SET 
                                   id_licenca = @id_licenca, 
                                   chave_licenca = @chave_licenca
                                   WHERE id = @id";

                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", software.Id);
                        cmd.Parameters.AddWithValue("@id_licenca", software.IdLicenca);
                        cmd.Parameters.AddWithValue("@chave_licenca", software.ChaveLicenca);

                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar software: {ex.Message}");
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

                    string sql = "DELETE FROM software WHERE id = @id";

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
                Console.WriteLine($"Erro ao excluir software: {ex.Message}");
                return false;
            }
        }
    }
}
