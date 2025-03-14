namespace Bunzl.Core.Domain.DTOs.Email;

public class EnviarEmail
{
    public bool Sucesso { get; set; }
    public required string Mensagem { get; set; }
}