using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.NegociacaoComercial.AdicionarObservacao;

public class NegociacaoComercialAdicionarObservacaoResponse(Guid id, string mensagem, Entities.Fornecedor fornecedor, string codigo, string usuarioAutenticadoNome, List<Entities.Usuario>? usuarios = null) : BaseResponseDto(id, mensagem)
{
	[JsonIgnore]
	public Entities.Fornecedor? Fornecedor { get; set; } = fornecedor;

	[JsonIgnore]
	public List<Entities.Usuario>? Usuarios { get; set; } = usuarios;

	[JsonIgnore]
	public string Codigo { get; set; } = codigo;

    [JsonIgnore] 
    public string UsuarioAutenticadoNome { get; set; } = usuarioAutenticadoNome;
}

