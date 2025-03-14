using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Fornecedor.AtualizarProduto;

public class FornecedorProdutoAtualizarResponse(Guid id, string mensagem, List<Entities.Usuario>? usuarios = null, string? nomeFornecedor = null, string? sku = null, string? descricaoCompletaProduto = null) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public List<Entities.Usuario>? Usuarios { get; set; } = usuarios;

    [JsonIgnore]
    public string? NomeFornecedor { get; set; } = nomeFornecedor;

    public string? Sku { get; set; } = sku;

    [JsonIgnore]
    public string? DescricaoCompletaProduto { get; set; } = descricaoCompletaProduto;
}
