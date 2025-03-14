namespace Bunzl.Domain.Commands.Login.LoginInicial;

public class LoginInicialResponse
{
    public LoginInicialResponse(Guid id, string nome, string email, string? telefone)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Telefone = telefone;

    }

    public LoginInicialResponse(string email, bool usuarioSenhaExpirada = true)
    {
        Email = email;
        UsuarioSenhaExpirada = usuarioSenhaExpirada;
    }

    public Guid? Id { get; }
    public string? Nome { get; }
    public string Email { get; }
    public string? Telefone { get; }
    public bool UsuarioSenhaExpirada { get; }
}