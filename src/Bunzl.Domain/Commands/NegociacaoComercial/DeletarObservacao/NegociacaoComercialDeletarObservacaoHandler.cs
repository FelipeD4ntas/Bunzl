using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;
using prmToolkit.EnumExtension;

namespace Bunzl.Domain.Commands.NegociacaoComercial.DeletarObservacao;

public class NegociacaoComercialDeletarObservacaoHandler(IPublisher mediator, IRepositoryNegociacaoComercial repositoryNegociacaoComercial, IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<NegociacaoComercialDeletarObservacaoRequest, CommandResponse<NegociacaoComercialDeletarObservacaoResponse>>
{
    public async Task<CommandResponse<NegociacaoComercialDeletarObservacaoResponse>> Handle(NegociacaoComercialDeletarObservacaoRequest request, CancellationToken cancellationToken)
    {
        var negociacaoComercial = await repositoryNegociacaoComercial.GetByAsync(true, c => c.Id == request.NegociacaoComercialId, cancellationToken, p => p.NegociacaoComercialObservacoes);
        if (negociacaoComercial == null)
        {
            AddNotification("NegociacaoComercial", NegociacaoComercialResources.NegociacaoComercialNaoEncontrada);
            return new CommandResponse<NegociacaoComercialDeletarObservacaoResponse>(this);
        }

        var observacao = negociacaoComercial.NegociacaoComercialObservacoes.FirstOrDefault(x => x.Id == request.ObservacaoId);
        if (observacao == null)
        {
            AddNotification("Observacao", NegociacaoComercialResources.ObservacaoNaoEncontrada);
            return new CommandResponse<NegociacaoComercialDeletarObservacaoResponse>(this);
        }

        var perfilUsuario = usuarioAutenticado.Permissoes.ToEnum<EPerfilUsuario>();
        if (perfilUsuario != EPerfilUsuario.AdministradorSuperUser)
        {
            AddNotification("Observacao", NegociacaoComercialResources.SomenteUsuarioAdministradorPodeDeletar);
            return new CommandResponse<NegociacaoComercialDeletarObservacaoResponse>(this);
        }

        negociacaoComercial.DeletarObservacao(observacao);
        repositoryNegociacaoComercial.Update(negociacaoComercial);
        await mediator.Publish(new AuditoriaAdicionarInput(negociacaoComercial.Id, TabelasResources.Fornecedor, "Observação Deletada", ETipoAuditoria.Modificado));

        var response = new NegociacaoComercialDeletarObservacaoResponse(observacao.Id, NegociacaoComercialResources.ObservacaoDeletadaComSucesso);
        return new CommandResponse<NegociacaoComercialDeletarObservacaoResponse>(response, this);
    }
}
