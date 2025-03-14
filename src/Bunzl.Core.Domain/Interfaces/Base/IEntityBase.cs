namespace Bunzl.Core.Domain.Interfaces.Base;

public interface IEntityBase
{
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public Guid UsuarioCriacao { get; set; }
    public Guid UsuarioAlteracao { get; set; }
}