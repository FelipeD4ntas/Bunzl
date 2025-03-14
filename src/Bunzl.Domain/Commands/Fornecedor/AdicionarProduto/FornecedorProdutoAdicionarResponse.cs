using Bunzl.Domain.DTOs.Base;
using Bunzl.Domain.Entities;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarProduto;

public class FornecedorProdutoAdicionarResponse(Guid id, string mensagem, FornecedorProduto fornecedorProduto, string? fornecedorNome = null, List<Entities.Usuario>? usuarios = null, string? descricaoCompletaProduto = null) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public FornecedorProduto FornecedorProduto { get; set; } = fornecedorProduto;

    [JsonIgnore]
    public string? FornecedorNome { get; set; } = fornecedorNome;

    [JsonIgnore]
    public List<Entities.Usuario>? Usuarios { get; set; } = usuarios;

    [JsonIgnore]
    public string? DescricaoCompletaProduto { get; set; } = descricaoCompletaProduto;

    public Guid ProdutoId { get; set; }
}
