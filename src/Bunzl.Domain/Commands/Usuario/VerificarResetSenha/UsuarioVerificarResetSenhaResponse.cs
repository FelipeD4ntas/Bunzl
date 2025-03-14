namespace Bunzl.Domain.Commands.Usuario.VerificarResetSenha;

public class UsuarioVerificarResetSenhaResponse(string mensagem)
{
    public string Mensagem { get; } = mensagem;
}