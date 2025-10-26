/// <summary>
/// Representa o usuário final (funcionário) que recebe o ativo (RNF01, RNF11).
/// </summary>
public class Colaborador
{
    public string IdColaborador { get; private set; } // Chave (Ex: Matrícula)
    public string Nome { get; set; }
    public string Departamento { get; set; }

    public Colaborador(string idColaborador, string nome, string departamento)
    {
        this.IdColaborador = idColaborador;
        this.Nome = nome;
        this.Departamento = departamento;
    }
}