using Bunzl.Domain.DTOs;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.Usuario.Obter;

public class UsuarioObterResponse(
    Guid id,
    string nome,
    string email,
    string telefone,
    string area,
    bool flagPrimeiroLogin,
    EPerfilUsuario perfilPermissao,
    IEnumerable<EmpresaDto> empresas,
    Guid? fornecedorId = null)
{
    public Guid Id { get; set; } = id;
    public string Nome { get; set; } = nome;
    public string Email { get; set; } = email;
    public string Telefone { get; set; } = telefone;
    public string Area { get; set; } = area;
    public bool FlagPrimeiroLogin { get; set; } = flagPrimeiroLogin;
    public EPerfilUsuario PerfilPermissao { get; set; } = perfilPermissao;
    public IEnumerable<EmpresaDto> Empresas { get; set; } = empresas;
    public Guid? FornecedorId { get; set; } = fornecedorId;
}