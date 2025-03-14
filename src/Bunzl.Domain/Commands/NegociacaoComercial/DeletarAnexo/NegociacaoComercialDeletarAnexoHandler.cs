using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.NegociacaoComercial.DeletarAnexo;

public class NegociacaoComercialDeletarAnexoHandler(IPublisher mediator, IRepositoryNegociacaoComercial repositoryNegociacaoComercial)
    : Notifiable, IRequestHandler<NegociacaoComercialDeletarAnexoRequest, CommandResponse<NegociacaoComercialDeletarAnexoResponse>>
{
    public async Task<CommandResponse<NegociacaoComercialDeletarAnexoResponse>> Handle(NegociacaoComercialDeletarAnexoRequest request, CancellationToken cancellationToken)
    {
        var negociacaoComercial = await repositoryNegociacaoComercial.GetByAsync(true, f => f.Id == request.Id, cancellationToken, p => p.Anexos);
        if (negociacaoComercial == null)
        {
            AddNotification("NegociacaoComercial", NegociacaoComercialResources.NegociacaoComercialNaoEncontrada);
            return new CommandResponse<NegociacaoComercialDeletarAnexoResponse>(this);
        }

        var anexo = negociacaoComercial.Anexos.Where(x => x.Id == request.AnexoId).FirstOrDefault();
        if (anexo == null)
        {
            AddNotification("Documento", NegociacaoComercialResources.AnexoNaoEncontrado);
            return new CommandResponse<NegociacaoComercialDeletarAnexoResponse>(this);
        }

        negociacaoComercial.DeletarAnexo(anexo);

        if (IsInvalid())
            return new CommandResponse<NegociacaoComercialDeletarAnexoResponse>(this);

        repositoryNegociacaoComercial.Update(negociacaoComercial);
        await mediator.Publish(new AuditoriaAdicionarInput(negociacaoComercial.Id, TabelasResources.NegociacaoComercial, "Anexo Deletado", ETipoAuditoria.Modificado));

        var diretorioArquivo = Path.Combine(Directory.GetCurrentDirectory(), "uploads", negociacaoComercial.Codigo.ToString());
        var caminhoArquivo = Path.Combine(diretorioArquivo, anexo.Nome);

        if (File.Exists(caminhoArquivo))
        {
            File.Delete(caminhoArquivo);
        }

        var response = new NegociacaoComercialDeletarAnexoResponse(anexo.Id, NegociacaoComercialResources.AnexoDeletadoComSucesso);
        return new CommandResponse<NegociacaoComercialDeletarAnexoResponse>(response, this);
    }
}