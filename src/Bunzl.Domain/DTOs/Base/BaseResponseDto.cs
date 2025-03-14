namespace Bunzl.Domain.DTOs.Base;

public class BaseResponseDto(Guid id, string mensagem)
{
    public Guid Id { get; } = id;
    public string Mensagem { get; } = mensagem;
}