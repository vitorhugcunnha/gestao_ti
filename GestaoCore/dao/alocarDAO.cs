using MySql.Data.MySqlClient;

namespace GestaoCore.dao
{
    public class Alocacao
    {
        public int Id { get; set; }
        public int IdColaborador { get; set; }
        public int? IdHardware { get; set; }
        public int? IdSoftware { get; set; }
        public DateTime DataAlocacao { get; set; }
        public DateTime? DataRetorno { get; set; }
        public string? DescricaoRetorno { get; set; }
        public string Status { get; set; } = "em_uso";
    }

    public class alocarDAO
    {
        private readonly string connectionString =
            "server=localhost;database=gestao_ti;user=root;password=;";

        public bool Inserir(Alocacao alocacao)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();
                    string sql = @"INSERT INTO alocacao 
                                   (id_colaborador, id_hardware, id_software, data_retorno, status)
                                   VALUES (@id_colaborador, @id_hardware, @id_software, @data_retorno, @status)";

                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id_colaborador", alocacao.IdColaborador);
                        cmd.Parameters.AddWithValue("@id_hardware", (object?)alocacao.IdHardware ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@id_software", (object?)alocacao.IdSoftware ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@data_retorno", (object?)alocacao.DataRetorno ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@status", alocacao.Status);

                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir alocação: {ex.Message}");
                return false;
            }
        }

        public List<Alocacao> Listar()
        {
            var lista = new List<Alocacao>();

            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();
                    string sql = @"SELECT id, id_colaborador, id_hardware, id_software, 
                                          data_alocacao, data_retorno, descricao_retorno, status
                                   FROM alocacao";

                    using (var cmd = new MySqlCommand(sql, conexao))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Alocacao
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                IdColaborador = reader.GetInt32(reader.GetOrdinal("id_colaborador")),
                                IdHardware = reader.IsDBNull(reader.GetOrdinal("id_hardware")) ? null : reader.GetInt32(reader.GetOrdinal("id_hardware")),
                                IdSoftware = reader.IsDBNull(reader.GetOrdinal("id_software")) ? null : reader.GetInt32(reader.GetOrdinal("id_software")),
                                DataAlocacao = reader.GetDateTime(reader.GetOrdinal("data_alocacao")),
                                DataRetorno = reader.IsDBNull(reader.GetOrdinal("data_retorno")) ? null : reader.GetDateTime(reader.GetOrdinal("data_retorno")),
                                DescricaoRetorno = reader.IsDBNull(reader.GetOrdinal("descricao_retorno")) ? null : reader.GetString(reader.GetOrdinal("descricao_retorno")),
                                Status = reader.GetString(reader.GetOrdinal("status"))
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar alocações: {ex.Message}");
            }

            return lista;
        }

        public Alocacao? BuscarPorId(int id)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();
                    string sql = @"SELECT id, id_colaborador, id_hardware, id_software, 
                                          data_alocacao, data_retorno, descricao_retorno, status
                                   FROM alocacao WHERE id = @id";

                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Alocacao
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    IdColaborador = reader.GetInt32(reader.GetOrdinal("id_colaborador")),
                                    IdHardware = reader.IsDBNull(reader.GetOrdinal("id_hardware")) ? null : reader.GetInt32(reader.GetOrdinal("id_hardware")),
                                    IdSoftware = reader.IsDBNull(reader.GetOrdinal("id_software")) ? null : reader.GetInt32(reader.GetOrdinal("id_software")),
                                    DataAlocacao = reader.GetDateTime(reader.GetOrdinal("data_alocacao")),
                                    DataRetorno = reader.IsDBNull(reader.GetOrdinal("data_retorno")) ? null : reader.GetDateTime(reader.GetOrdinal("data_retorno")),
                                    DescricaoRetorno = reader.IsDBNull(reader.GetOrdinal("descricao_retorno")) ? null : reader.GetString(reader.GetOrdinal("descricao_retorno")),
                                    Status = reader.GetString(reader.GetOrdinal("status"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar alocação: {ex.Message}");
            }

            return null;
        }

        public bool Atualizar(Alocacao alocacao)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();
                    string sql = @"UPDATE alocacao SET 
                                   id_colaborador = @id_colaborador,
                                   id_hardware = @id_hardware,
                                   id_software = @id_software,
                                   data_retorno = @data_retorno,
                                   descricao_retorno = @descricao_retorno,
                                   status = @status
                                   WHERE id = @id";

                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", alocacao.Id);
                        cmd.Parameters.AddWithValue("@id_colaborador", alocacao.IdColaborador);
                        cmd.Parameters.AddWithValue("@id_hardware", (object?)alocacao.IdHardware ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@id_software", (object?)alocacao.IdSoftware ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@data_retorno", (object?)alocacao.DataRetorno ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@descricao_retorno", (object?)alocacao.DescricaoRetorno ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@status", alocacao.Status);

                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar alocação: {ex.Message}");
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
                    string sql = "DELETE FROM alocacao WHERE id = @id";

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
                Console.WriteLine($"Erro ao excluir alocação: {ex.Message}");
                return false;
            }
        }
    }
}
