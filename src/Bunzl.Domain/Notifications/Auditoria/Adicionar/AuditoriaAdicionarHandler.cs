using Bunzl.Domain.Interfaces;
using Mapster;
using MediatR;

namespace Bunzl.Domain.Notifications.Auditoria.Adicionar;

public class AuditoriaAdicionarHandler(IRepositoryAuditoria repositoryAuditoria) : INotificationHandler<AuditoriaAdicionarInput>
{
    public async Task Handle(AuditoriaAdicionarInput notification, CancellationToken cancellationToken)
    {
        var auditoria = notification.Adapt<Entities.Auditoria>();
        await repositoryAuditoria.AddAsync(auditoria, cancellationToken);
    }
}