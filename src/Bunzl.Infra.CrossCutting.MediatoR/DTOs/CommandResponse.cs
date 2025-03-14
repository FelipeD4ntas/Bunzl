using Bunzl.Infra.CrossCutting.NotificationPattern.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern.Interface;

namespace Bunzl.Infra.CrossCutting.MediatoR.DTOs;

public class CommandResponse<T>
    where T : class
{
    public bool Sucesso { get; set; }
    public T? Dados { get; set; }
    public IReadOnlyCollection<Notification> Notificacoes { get; set; }

    public CommandResponse(T dados, INotifiable notificacoes)
    {
        Sucesso = notificacoes.IsValid();
        Dados = dados;
        Notificacoes = notificacoes.Notifications;
    }

    public CommandResponse(INotifiable notificacoes)
    {
        Sucesso = false;
        Dados = null;
        Notificacoes = notificacoes.Notifications;
    }
}