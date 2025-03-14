using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.OrdemDeCompra.AdicionarObservacao;

public class OrdemDeCompraAdicionarObservacaoResponse(Guid id, string mensagem, Entities.Fornecedor fornecedor, string numero, string usuarioAutenticadoNome, Entities.Empresa? empresa, List<Entities.Usuario>? usuarios = null) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public Entities.Fornecedor? Fornecedor { get; set; } = fornecedor;

    [JsonIgnore]
    public Entities.Empresa? Empresa { get; set; } = empresa;

    [JsonIgnore]
    public List<Entities.Usuario>? Usuarios { get; set; } = usuarios;

    [JsonIgnore]
    public string Numero { get; set; } = numero;

    [JsonIgnore]
    public string UsuarioAutenticadoNome { get; set; } = usuarioAutenticadoNome;
}

