using Bunzl.Domain.DTOs.Base;
using Bunzl.Domain.DTOs.TabelaPreco;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.TabelaPreco.Atualizar;

public class TabelaPrecoAtualizarResponse(
    Guid id, 
    string mensagem, 
    List<TabelaPrecoProdutoComErrosDto>? erros,
	List<Entities.Usuario>? usuarios = null, 
    string? nomeFornecedor = null
	) : BaseResponseDto(id, mensagem)
{
    public List<TabelaPrecoProdutoComErrosDto> Erros { get; set; } = erros ?? [];

	[JsonIgnore]
	public List<Entities.Usuario>? Usuarios { get; set; } = usuarios;

	[JsonIgnore]
	public string? NomeFornecedor { get; set; } = nomeFornecedor;
}

