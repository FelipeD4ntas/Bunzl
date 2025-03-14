namespace Bunzl.Domain.Commands.Usuario.Ativar
{
    public class UsuarioAtivarResponse(Guid id)
    {
        public Guid Id { get; } = id;
    }
}
