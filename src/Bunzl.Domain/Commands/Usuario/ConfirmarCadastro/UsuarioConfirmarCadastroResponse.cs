namespace Bunzl.Domain.Commands.Usuario.ConfirmarCadastro;

public class UsuarioConfirmarCadastroResponse(string mensagem)
{
    public string Mensagem { get; } = mensagem;
}