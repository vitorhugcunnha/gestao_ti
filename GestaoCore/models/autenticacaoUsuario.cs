using MySql.Data.MySqlClient;
namespace GestaoCore.Models
{
    public interface ITela
    {
        void TelaLogin(out string usuario, out string senha);
        void falhaAutenticacao();
        void sucessoAutenticacao();
        void AlternativaIncorreta();
        void TelaSair();
        string telaCrud();
        string telaCrudSecundaria(string nomeMenu);
        void MontarMolduraCentralizada(string titulo);
        void MontarMolduraAdaptavel(string titulo, string[] conteudo);
    }

    public class autenticacaoUsuario
    {
        private string conn = "server=localhost;database=gestao_ti;user=root;password=;";
        private ITela tela;

        public autenticacaoUsuario(ITela tela)
        {
            this.tela = tela;
        }
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
        public bool autenticar()
        {
            bool autenticado = false;
            while (!autenticado)
            {
                string usuario, senha;
                tela.TelaLogin(out usuario, out senha);

                autenticado = Autenticar(usuario, senha);

                if (!autenticado)
                {
                    tela.falhaAutenticacao();
                }
            }

            return true;
        }
    }
}