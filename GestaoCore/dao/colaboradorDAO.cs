using MySql.Data.MySqlClient;

namespace GestaoCore.dao
{
    public class Colaborador
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Email { get; set; } = "";
        public string Cargo { get; set; } = "";
        public string Departamento { get; set; } = "";
    }

    public class ColaboradorDAO
    {
        private readonly string connectionString =
            "server=localhost;database=gestao_ti;user=root;password=;";

        public bool Inserir(Colaborador colaborador)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    string sql = @"INSERT INTO colaborador (nome, email, cargo, departamento)
                                   VALUES (@nome, @email, @cargo, @departamento)";

                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@nome", colaborador.Nome);
                        cmd.Parameters.AddWithValue("@email", colaborador.Email);
                        cmd.Parameters.AddWithValue("@cargo", colaborador.Cargo);
                        cmd.Parameters.AddWithValue("@departamento", colaborador.Departamento);

                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir colaborador: {ex.Message}");
                return false;
            }
        }

        public List<Colaborador> Listar()
        {
            var lista = new List<Colaborador>();

            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    string sql = "SELECT id, nome, email, cargo, departamento FROM colaborador";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Colaborador
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                Email = reader.GetString("email"),
                                Cargo = reader.GetString("cargo"),
                                Departamento = reader.GetString("departamento")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar colaboradores: {ex.Message}");
            }

            return lista;
        }

        public Colaborador? BuscarPorId(int id)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    string sql = "SELECT id, nome, email, cargo, departamento FROM colaborador WHERE id = @id";
                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Colaborador
                                {
                                    Id = reader.GetInt32("id"),
                                    Nome = reader.GetString("nome"),
                                    Email = reader.GetString("email"),
                                    Cargo = reader.GetString("cargo"),
                                    Departamento = reader.GetString("departamento")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar colaborador: {ex.Message}");
            }

            return null;
        }

        public bool Atualizar(Colaborador colaborador)
        {
            try
            {
                using (var conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    string sql = @"UPDATE colaborador SET 
                                   nome = @nome, 
                                   email = @email, 
                                   cargo = @cargo, 
                                   departamento = @departamento
                                   WHERE id = @id";

                    using (var cmd = new MySqlCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@nome", colaborador.Nome);
                        cmd.Parameters.AddWithValue("@email", colaborador.Email);
                        cmd.Parameters.AddWithValue("@cargo", colaborador.Cargo);
                        cmd.Parameters.AddWithValue("@departamento", colaborador.Departamento);
                        cmd.Parameters.AddWithValue("@id", colaborador.Id);

                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        return linhasAfetadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar colaborador: {ex.Message}");
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

                    string sql = "DELETE FROM colaborador WHERE id = @id";

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
                Console.WriteLine($"Erro ao excluir colaborador: {ex.Message}");
                return false;
            }
        }
    }
}
