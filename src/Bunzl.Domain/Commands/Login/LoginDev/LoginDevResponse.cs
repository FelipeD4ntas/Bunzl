namespace Bunzl.Domain.Commands.Login.LoginDev;

public class LoginDevResponse(string nome, string email, string token)
{
    public string Nome { get; } = nome;
    public string Email { get; } = email;
    public string Token { get; } = token;
}