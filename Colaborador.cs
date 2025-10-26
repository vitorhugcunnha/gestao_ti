/// <summary>
/// Representa o usuário final (funcionário) que recebe o ativo (RNF01, RNF11).
/// </summary>
public class Colaborador
{
    public int IdColaborador { get; private set; }
    public string Nome { get; set; }
    public string Departamento { get; set; }

    public Colaborador(string idColaborador, string nome, string departamento)
    {
        this.IdColaborador = new Random().Next(1000, 9999); // Simulação de ID
        this.Nome = nome;
        this.Departamento = departamento;
    }
}