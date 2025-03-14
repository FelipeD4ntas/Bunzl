namespace Bunzl.Domain.DTOs;

public class FornecedorDto
{
    public Guid Id { get; set; }
    public required string RazaoSocial { get; set; }
    public required string NomeFantasia { get; set; }
}