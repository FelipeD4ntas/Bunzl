using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.Integracao.ObterProdutos;

public class ObterProdutosResponse(Guid id, string mensagem, string? sku, string? descricao, string? unidadeMedida, DateTime dataAlteração) : BaseResponseDto(id, mensagem)
{
    public string? SKU { get; set; } = sku;
    public string? Descricao { get; set; } = descricao;
    public string? UnidadeMedida { get; set; } = unidadeMedida;
    public DateTime? DataAlteracao { get; set; } = dataAlteração;
}
