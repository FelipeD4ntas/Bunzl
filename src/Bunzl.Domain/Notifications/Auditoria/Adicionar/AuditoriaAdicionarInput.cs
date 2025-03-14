using Bunzl.Domain.Enumerators;
using MediatR;

namespace Bunzl.Domain.Notifications.Auditoria.Adicionar;

public class AuditoriaAdicionarInput(Guid entidadeId, string entidadeNome, string funcao, ETipoAuditoria tipoAuditoria) : INotification
{
    public Guid EntidadeId { get; init; } = entidadeId;
    public string EntidadeNome { get; init; } = entidadeNome;
    public string Funcao { get; init; } = funcao;
    public ETipoAuditoria TipoAuditoria { get; init; } = tipoAuditoria;
}
