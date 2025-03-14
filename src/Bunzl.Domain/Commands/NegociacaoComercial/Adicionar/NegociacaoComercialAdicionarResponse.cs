using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Adicionar;

public class NegociacaoComercialAdicionarResponse(Guid id, string mensagem, Entities.Fornecedor? fornecedor = null, string? nomeEmpresa = null) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public Entities.Fornecedor? Fornecedor { get; set; } = fornecedor;

    [JsonIgnore]
    public string? NomeEmpresa { get; set; } = nomeEmpresa;
}

