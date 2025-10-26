using System;

/// <summary>
/// Classe associativa que representa o "evento" de uma alocação.
/// Ela armazena os dados DO RELACIONAMENTO entre um Ativo e um Colaborador.
/// </summary>
public class Alocacao
{
    public int IdAlocacao { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; } // Nulável (pode não ter data de fim)
    public string MotivoFim { get; private set; }

    // Relacionamentos (Chaves Estrangeiras)
    public Ativo Ativo { get; private set; } 
    public Colaborador Colaborador { get; private set; }

    /// <summary>
    /// Construtor é chamado de dentro do método Ativo.AlocarPara()
    /// </summary>
    public Alocacao(Ativo ativo, Colaborador colaborador, DateTime dataInicio)
    {
        this.IdAlocacao = new Random().Next(10000, 99999); // Simulação de ID
        
        this.Ativo = ativo;
        this.Colaborador = colaborador;
        
        this.DataInicio = dataInicio;
        
        this.DataFim = null;
        this.MotivoFim = string.Empty;
    }

    /// <summary>
    /// Método chamado pelo Ativo.Retornar() para fechar/finalizar esta alocação (RF11).
    /// </summary>
    public void Finalizar(DateTime dataFim, string motivo)
    {
        this.DataFim = dataFim;
        this.MotivoFim = motivo;
    }
}