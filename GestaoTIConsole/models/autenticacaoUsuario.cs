using MySql.Data.MySqlClient;

public class autenticacaoUsuario
{
    private string conn = "server=localhost;database=gestao_ti;user=root;password=;";
    public bool Autenticar(string nome, string senha)
    {
        using (var conexao = new MySqlConnection(conn))
        {
            conexao.Open();

            string query = "SELECT COUNT(*) FROM usuario WHERE TRIM(nome_usuario) = @nome AND TRIM(senha) = @senha";
            using (var cmd = new MySqlCommand(query, conexao))
            {
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome.Trim();
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = senha.Trim();

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
    }
} 