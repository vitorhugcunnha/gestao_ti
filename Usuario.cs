using System;

/// <summary>
/// Representa o usuário que acessa o sistema (Técnico, Gerente, etc.)
/// Responsável pela segurança e permissões.
/// </summary>
public class Usuario
{
    public int IdUsuario { get; private set; }
    public string NomeUsuario { get; set; }
    public string SenhaHash { get; private set; } // Armazenar HASH, nunca a senha pura
    public PapelUsuario Role { get; set; }

    public Usuario(string nomeUsuario, string senhaPura, PapelUsuario role)
    {
        this.IdUsuario = new Random().Next(1, 100);
        this.NomeUsuario = nomeUsuario;

        this.SenhaHash = GerarHashDaSenha(senhaPura); // Simulação
        
        this.Role = role;
    }

    public bool FazerLogin(string senhaPura)
    {
        // Lógica de autenticação (simplificada)
        return GerarHashDaSenha(senhaPura) == this.SenhaHash;
    }

    /// <summary>
    /// Controle de acesso baseado em papéis de usuário.
    /// </summary>
    public bool TemPermissaoPara(PapelUsuario papelNecessario)
    {
        // Exemplo simples: O Técnico de TI pode fazer tudo.
        if (this.Role == PapelUsuario.TecnicoTI)
        {
            return true;
        }
        
        // O Gerente só pode acessar o que for de Gerente.
        return this.Role == papelNecessario;
    }

    // Método auxiliar de simulação de Hash
    private string GerarHashDaSenha(string senha)
    {
        // ISSO NÃO É FEITO EM SISTEMAS REAIS! BCrypt.Net ou similares podem ser utilizados para esta tarefa.
        return $"hash_{senha}_simulado";
    }
}