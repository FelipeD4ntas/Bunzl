namespace Bunzl.Infra.CrossCutting.Security.Autenticacao;

public interface IUsuarioAutenticado
{
    public Guid UsuarioId { get; }
    public string UsuarioNome { get; }
    public Guid UsuarioEmpresa { get; }
    public string Permissoes { get; }
    public DateTime Expiracao { get; }
    public string Idioma { get; }
    public string Profile { get; }
}