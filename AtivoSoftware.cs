using System;

/// <summary>
/// Representa um ativo lógico (Software/Licença Instalada).
/// Herda da classe Ativo.
/// </summary>
public class AtivoSoftware : Ativo
{
    // Atributo específico de Software (RF03)
    public string ChaveLicenca { get; set; }
    
    // Relacionamento: Este software "pertence" a uma licença-mãe
    public Licenca LicencaAssociada { get; private set; }

    public AtivoSoftware(string tipo, string marca, string modelo, DateTime dataAquisicao, string chaveLicenca, Licenca licencaAssociada)
        : base(tipo, marca, modelo, dataAquisicao)
    {
        this.ChaveLicenca = chaveLicenca;
        this.LicencaAssociada = licencaAssociada;
        
        // Avisa a Licença-mãe que ela tem uma nova instalação
        // Isso garante a contagem para o RF16
        licencaAssociada.AdicionarInstalacao(this);
    }

    /// <summary>
    /// Implementação OBRIGATÓRIA do método abstrato.
    /// RF05: ...e de software pela Chave de Licença.
    /// </summary>
    public override bool ValidarDuplicidade()
    {
        // --- INÍCIO DA LÓGICA DE IMPLEMENTAÇÃO ---
        // Em um sistema real, aqui você faria uma consulta ao Banco de Dados.
        // Ex: return repositorio.ExisteSoftwareComChave(this.ChaveLicenca);
        
        Console.WriteLine($"Verificando duplicidade de Software pela Chave: {this.ChaveLicenca}");
        // Simulação:
        if (this.ChaveLicenca == "ABC-DUPLICADO")
        {
            return true; // Encontrou duplicado
        }
        return false; // Não é duplicado
        // --- FIM DA LÓGICA DE IMPLEMENTAÇÃO ---
    }
}