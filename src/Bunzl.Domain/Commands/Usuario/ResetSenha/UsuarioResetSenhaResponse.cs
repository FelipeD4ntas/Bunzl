namespace Bunzl.Domain.Commands.Usuario.ResetSenha;

public class UsuarioResetSenhaResponse(string mensagem)
{
    public string Mensagem { get; } = mensagem;
}