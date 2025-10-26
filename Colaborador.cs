/// <summary>
/// Representa o usuário final (funcionário) que recebe o ativo.
/// </summary>
public class Colaborador
{
    // Um contador estático que pertence à CLASSE, não a um objeto individual.
    // Ele será compartilhado por todas as instâncias de Colaborador.
    private static int proximoId = 1; // Contagem inicia em 1

    public int IdColaborador { get; private set; }
    public string Nome { get; set; }
    public string Departamento { get; set; }

    public Colaborador(string nome, string departamento)
    {
        // 1. Atribui o próximo ID disponível
        this.IdColaborador = proximoId; 
        
        // 2. Incrementa o contador para o próximo Colaborador que for criado
        proximoId++;

        this.Nome = nome;
        this.Departamento = departamento;
    }
}