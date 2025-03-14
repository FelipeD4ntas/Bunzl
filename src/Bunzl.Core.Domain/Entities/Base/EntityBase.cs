using Bunzl.Core.Domain.Interfaces.Base;
using Bunzl.Infra.CrossCutting.NotificationPattern;

namespace Bunzl.Core.Domain.Entities.Base;

public abstract class EntityBase<TId> : Notifiable, IEntityBase
{
    public TId Id { get; protected set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAlteracao { get; set; } = null;
    public Guid UsuarioCriacao { get; set; }
    public Guid UsuarioAlteracao { get; set; }
}