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
    public DateTime DataValidade { get; set; } // RF13
    public int CapacidadeTotal { get; set; } // RF16

    // Relacionamento: Uma licença "controla" várias instalações (AtivoSoftware)
    private List<AtivoSoftware> instalacoes;
    public IReadOnlyList<AtivoSoftware> Instalacoes => instalacoes; // Propriedade pública apenas para leitura

    public Licenca(string nomeSoftware, DateTime dataValidade, int capacidadeTotal)
    {
        this.IdLicenca = new Random().Next(1000, 9999); // Simulação de ID
        this.NomeSoftware = nomeSoftware;
        this.DataValidade = dataValidade; // RF13
        this.CapacidadeTotal = capacidadeTotal; // RF16
        this.instalacoes = new List<AtivoSoftware>();
    }
    
    /// <summary>
    /// Método interno chamado pelo AtivoSoftware para se registrar na licença.
    /// </summary>
    public void AdicionarInstalacao(AtivoSoftware software)
    {
        // RF16: O sistema deve garantir que o número de instalações ativas não
        //       exceda a capacidade total da licença.
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
    /// Verifica se o número de instalações excede o permitido (RF16).
    /// </summary>
    public bool VerificarCapacidadeExcedida()
    {
        // Compara a contagem atual com o limite
        return GetInstalacoesAtivas() >= this.CapacidadeTotal;
    }

    /// <summary>
    /// Verifica se a licença está próxima de vencer (RF15).
    /// </summary>
    public bool VerificarVencimento(int diasAntecedencia)
    {
        // RF15: O sistema deve alertar o Técnico de TI com 60 e 30 dias de
        //       antecedência do vencimento da licença.
        return (this.DataValidade - DateTime.Now).TotalDays <= diasAntecedencia;
    }
}