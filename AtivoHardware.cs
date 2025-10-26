using System;

/// <summary>
/// Representa um ativo físico (Hardware).
/// Herda da classe Ativo.
/// </summary>
public class AtivoHardware : Ativo
{
    // Atributo específico de Hardware (RF03)
    public string NumeroSerie { get; set; }

    /// <summary>
    /// Construtor da classe filha, que chama (via 'base') o construtor da classe pai (Ativo).
    /// </summary>
    public AtivoHardware(string tipo, string marca, string modelo, DateTime dataAquisicao, string numeroSerie)
        : base(tipo, marca, modelo, dataAquisicao)
    {
        // Define o atributo específico desta classe
        this.NumeroSerie = numeroSerie;
    }

    /// <summary>
    /// Implementação OBRIGATÓRIA do método abstrato.
    /// RF05: ...controlar a duplicidade de ativos de hardware pelo Número de Série...
    /// </summary>
    public override bool ValidarDuplicidade()
    {
        // --- INÍCIO DA LÓGICA DE IMPLEMENTAÇÃO ---
        // Em um sistema real, aqui você faria uma consulta ao Banco de Dados.
        // Ex: return repositorio.ExisteHardwareComNumeroSerie(this.NumeroSerie);
        
        Console.WriteLine($"Verificando duplicidade de Hardware pelo N/S: {this.NumeroSerie}");
        // Simulação:
        if (this.NumeroSerie == "123456-DUPLICADO")
        {
            return true; // Encontrou duplicado
        }
        return false; // Não é duplicado
        // --- FIM DA LÓGICA DE IMPLEMENTAÇÃO ---
    }
}