using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Classe abstrata que serve como base para Ativos de Hardware e Software.
/// Define comportamentos e atributos comuns a todos os ativos.
/// </summary>
public abstract class Ativo
{
    // Atributos (Propriedades do C#)
    public string IdPatrimonial { get; private set; }
    public string Tipo { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public DateTime DataAquisicao { get; set; }
    public StatusAtivo Status { get; private set; }

    // Relacionamento: Um ativo tem um histórico de alocações
    // Usamos List<> para armazenar as várias alocações que um ativo pode ter ao longo do tempo.
    public List<Alocacao> HistoricoAlocacoes { get; private set; }

    /// <summary>
    /// Construtor (protected) para ser chamado APENAS pelas classes filhas (AtivoHardware, AtivoSoftware).
    /// </summary>
    protected Ativo(string tipo, string marca, string modelo, DateTime dataAquisicao)
    {
        // RF02: O sistema deve gerar um código identificador (etiqueta patrimonial)
        //       para cada ativo automaticamente.
        this.IdPatrimonial = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        
        this.Tipo = tipo;
        this.Marca = marca;
        this.Modelo = modelo;
        this.DataAquisicao = dataAquisicao;
        
        // RF04, RF09: Define o status inicial. Um ativo novo sempre entra "Em Estoque".
        this.Status = StatusAtivo.EmEstoque;
        this.HistoricoAlocacoes = new List<Alocacao>();
    }

    /// <summary>
    /// Inicia o processo de alocação deste ativo para um colaborador.
    /// </summary>
    public Alocacao AlocarPara(Colaborador colaborador, DateTime dataInicio)
    {
        // O sistema deve permitir a alocação de um ativo apenas se o seu
        // status for "Em Estoque".
        if (this.Status != StatusAtivo.EmEstoque)
        {
            throw new InvalidOperationException("Este ativo não está 'Em Estoque' e não pode ser alocado.");
        }

        // Cria o registro da alocação vinculando ativo e colaborador.
        // Garante que a Data de Início seja registrada.
        var novaAlocacao = new Alocacao(this, colaborador, dataInicio);
        this.HistoricoAlocacoes.Add(novaAlocacao);
        
        // Atualiza o status do ativo para "Em Uso" automaticamente após a sua alocação.
        this.AtualizarStatus(StatusAtivo.EmUso);
        
        return novaAlocacao;
    }

    /// <summary>
    /// Inicia o processo de retorno (desalocação) deste ativo.
    /// </summary>
    public void Retornar(DateTime dataFim, string motivo, StatusAtivo novoStatusAposRetorno)
    {
        // Encontra a alocação atual (a que não tem data de fim)
        var alocacaoAtiva = this.HistoricoAlocacoes.FirstOrDefault(a => a.DataFim == null);

        if (alocacaoAtiva == null || this.Status != StatusAtivo.EmUso)
        {
            throw new InvalidOperationException("Este ativo não está 'Em Uso' no momento.");
        }

        // RF11: O sistema deve registrar a desalocação e retorno de um ativo,
        //       registrando a Data de Fim e o Motivo.
        alocacaoAtiva.Finalizar(dataFim, motivo);
        
        // RF12: O sistema deve atualizar o status do ativo para "Em Estoque" ou
        //       "Manutenção" automaticamente... após o seu retorno.
        if(novoStatusAposRetorno != StatusAtivo.EmEstoque && novoStatusAposRetorno != StatusAtivo.Manutencao)
        {
            throw new ArgumentException("O novo status após retorno deve ser 'EmEstoque' ou 'Manutencao'.");
        }
        this.AtualizarStatus(novoStatusAposRetorno);
    }

    /// <summary>
    /// Método (privado/protegido) que centraliza a mudança de status (RF04).
    /// </summary>
    private void AtualizarStatus(StatusAtivo novoStatus)
    {
        // Aqui poderiam entrar lógicas de log, auditoria, etc.
        this.Status = novoStatus;
        Console.WriteLine($"Ativo {this.IdPatrimonial} teve status atualizado para: {novoStatus}");
    }
    
    /// <summary>
    /// Verifica se o ativo está próximo do descarte (para RF17).
    /// </summary>
    public bool EstaProximoDescarte(int anosCicloVida = 4)
    {
        // RF17: ...para equipamentos com mais de 4 anos de uso.
        return (DateTime.Now - this.DataAquisicao).TotalDays > (anosCicloVida * 365);
    }

    // --- Método Abstrato (Contrato) ---
    
    /// <summary>
    /// Contrato (método abstrato) que OBRIGA as classes filhas
    /// a implementar sua própria lógica de verificação de duplicidade (RF05).
    /// </summary>
    public abstract bool ValidarDuplicidade(); // RF05
}