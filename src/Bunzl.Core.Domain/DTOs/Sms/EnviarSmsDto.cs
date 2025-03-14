namespace Bunzl.Core.Domain.DTOs.Sms;

public class EnviarSmsDto(bool sucesso, string mensagem)
{
    public bool Sucesso { get; } = sucesso;
    public string Mensagem { get; } = mensagem;
}
