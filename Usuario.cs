using System;

/// <summary>
/// Representa o usuário que acessa o sistema (Técnico, Gerente, etc.)
/// Responsável pela segurança e permissões (RNF08).
/// </summary>
public class Usuario
{
    public int IdUsuario { get; private set; }
    public string NomeUsuario { get; set; }
    public string SenhaHash { get; private set; } // Armazenar HASH, nunca a senha pura
    public PapelUsuario Role { get; set; } // RNF08

    public Usuario(string nomeUsuario, string senhaPura, PapelUsuario role)
    {
        this.IdUsuario = new Random().Next(1, 100);
        this.NomeUsuario = nomeUsuario;
        
        // RNF09: O sistema deve proteger as informações confidenciais...
        // Em um sistema real, use uma biblioteca de Hashing (ex: BCrypt)
        this.SenhaHash = GerarHashDaSenha(senhaPura); // Simulação
        
        this.Role = role;
    }

    public bool FazerLogin(string senhaPura)
    {
        // Lógica de autenticação (simplificada)
        return GerarHashDaSenha(senhaPura) == this.SenhaHash;
    }

    /// <summary>
    /// RNF08: O sistema deve ter controle de acesso baseado em papéis de usuário...
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
        // NÃO FAÇA ISSO EM PRODUÇÃO! Use BCrypt.Net ou similar.
        return $"hash_{senha}_simulado";
    }
}