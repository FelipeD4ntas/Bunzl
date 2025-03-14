using Bunzl.Domain.DTOs.Base;
using Bunzl.Domain.Entities;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarAnexoProduto;

public class FornecedorProdutoAdicionarAnexoResponse(Guid id, string mensagem, string? fornecedorNome = null, List<Entities.Usuario>? usuarios = null, string? sku = null, string? descricaoCompletaProduto = null) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public string? FornecedorNome { get; set; } = fornecedorNome;

    [JsonIgnore]
    public List<Entities.Usuario>? Usuarios { get; set; } = usuarios;

    public string? Sku { get; set; } = sku;

    [JsonIgnore]
    public string? DescricaoCompletaProduto { get; set; } = descricaoCompletaProduto;
}
