using Bunzl.Domain.DTOs;
using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaCadastroProduto;

public class ProdutosAdicionarPlanilhaResponse(Guid id, string mensagem, List<FornecedorProdutoErrosDto> fornecedorProdutoErrosCadastro, int quantidadeProdutosImportados = 0, string? fornecedorNome = null, List<Entities.Usuario>? usuarios = null) : BaseResponseDto(id, mensagem)
{
    public List<FornecedorProdutoErrosDto>? FornecedorProdutoErrosCadastro { get; set; } = fornecedorProdutoErrosCadastro;
    public int QuantidadeProdutosImportados { get; set; } = quantidadeProdutosImportados;

    [JsonIgnore]
    public string? FornecedorNome { get; set; } = fornecedorNome;

    [JsonIgnore]
    public List<Entities.Usuario>? Usuarios { get; set; } = usuarios;
}
