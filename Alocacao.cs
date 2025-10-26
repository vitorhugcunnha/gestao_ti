using System;

/// <summary>
/// Classe associativa que representa o "evento" de uma alocação.
/// Ela armazena os dados DO RELACIONAMENTO entre um Ativo e um Colaborador.
/// </summary>
public class Alocacao
{
    public int IdAlocacao { get; private set; }
    public DateTime DataInicio { get; private set; } // RF07
    public DateTime? DataFim { get; private set; } // Nulável (pode não ter data de fim)
    public string MotivoFim { get; private set; } // RF11

    // Relacionamentos (Chaves Estrangeiras)
    public Ativo Ativo { get; private set; } // RNF11
    public Colaborador Colaborador { get; private set; } // RNF11

    /// <summary>
    /// Construtor é chamado de dentro do método Ativo.AlocarPara()
    /// </summary>
    public Alocacao(Ativo ativo, Colaborador colaborador, DateTime dataInicio)
    {
        this.IdAlocacao = new Random().Next(10000, 99999); // Simulação de ID
        
        // RNF11: O sistema deve registrar a alocação de um ativo, vinculando-o a um
        //        ID do Ativo e a um ID do Colaborador (Usuário).
        this.Ativo = ativo;
        this.Colaborador = colaborador;
        
        // RF07: O sistema deve registrar a Data de Início da alocação obrigatoriamente.
        this.DataInicio = dataInicio;
        
        // Começa Nulo, pois a alocação está ativa.
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