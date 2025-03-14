namespace Bunzl.Core.Domain.DTOs.Base;

public class CommitResult(bool sucesso, string? mensagemErro, string? mensagemErroDetalhada)
{
    public bool Sucesso { get; } = sucesso;
    public string? MensagemErro { get; } = mensagemErro;
    public string? MensagemErroDetalhada { get; } = mensagemErroDetalhada;
}