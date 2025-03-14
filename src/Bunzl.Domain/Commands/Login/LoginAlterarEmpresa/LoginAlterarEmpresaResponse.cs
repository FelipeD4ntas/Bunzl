namespace Bunzl.Domain.Commands.Login.LoginAlterarEmpresa;

public class LoginAlterarEmpresaResponse(string nome, string email, string token)
{
    public string Nome { get; } = nome;
    public string Email { get; } = email;
    public string Token { get; } = token;
}