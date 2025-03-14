namespace Bunzl.Domain.Commands.Usuario.Inativar
{
    public class UsuarioInativarResponse(Guid id)
    {
        public Guid Id { get; } = id;
    }
}
