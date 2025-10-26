using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Gerencia os direitos de uso de um software (a "licença-mãe").
/// </summary>
public class Licenca
{
    public int IdLicenca { get; private set; }
    public string NomeSoftware { get; set; }
    public DateTime DataValidade { get; set; }
    public int CapacidadeTotal { get; set; }

    // Relacionamento: Uma licença "controla" várias instalações (AtivoSoftware)
    private List<AtivoSoftware> instalacoes;
    public IReadOnlyList<AtivoSoftware> Instalacoes => instalacoes; // Propriedade pública apenas para leitura

    public Licenca(string nomeSoftware, DateTime dataValidade, int capacidadeTotal)
    {
        this.IdLicenca = new Random().Next(1000, 9999); // Simulação de ID
        this.NomeSoftware = nomeSoftware;
        this.DataValidade = dataValidade;
        this.CapacidadeTotal = capacidadeTotal;
        this.instalacoes = new List<AtivoSoftware>();
    }
    
    /// <summary>
    /// Método interno chamado pelo AtivoSoftware para se registrar na licença.
    /// </summary>
    public void AdicionarInstalacao(AtivoSoftware software)
    {
        if (VerificarCapacidadeExcedida())
        {
            // Impede que o AtivoSoftware seja criado se a licença estiver cheia
            throw new InvalidOperationException($"Capacidade total ({this.CapacidadeTotal}) da licença '{this.NomeSoftware}' foi excedida.");
        }
        this.instalacoes.Add(software);
    }

    public int GetInstalacoesAtivas()
    {
        // Retorna a contagem de instalações ativas (Status "Em Uso")
        return this.instalacoes.Count(inst => inst.Status == StatusAtivo.EmUso);
    }

    /// <summary>
    /// Verifica se o número de instalações excede o permitido
    /// </summary>
    public bool VerificarCapacidadeExcedida()
    {
        // Compara a contagem atual com o limite
        return GetInstalacoesAtivas() >= this.CapacidadeTotal;
    }

    /// <summary>
    /// Verifica se a licença está próxima de vencer
    /// </summary>
    public bool VerificarVencimento(int diasAntecedencia)
    {
        // Alerta com 60 e 30 dias de antecedência do vencimento da licença.
        return (this.DataValidade - DateTime.Now).TotalDays <= diasAntecedencia;
    }
}