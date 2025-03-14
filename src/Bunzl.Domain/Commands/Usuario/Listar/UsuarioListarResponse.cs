using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.Usuario.Listar;

public class UsuarioListarResponse
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public string? Telefone { get; set; }
    public EPerfilUsuario PerfilPermissao { get; set; }
    public IEnumerable<UsuarioListarEmpresaResponse>? Empresas { get; set; }
    public bool FlagAtivo { get; set; }
}
