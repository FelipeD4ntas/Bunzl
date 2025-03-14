using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.DeletarAnexo;

public class OrdemDeCompraDeletarAnexoHandler(IPublisher mediator, IRepositoryOrdemDeCompra repositoryOrdemDeCompra)
    : Notifiable, IRequestHandler<OrdemDeCompraDeletarAnexoRequest, CommandResponse<OrdemDeCompraDeletarAnexoResponse>>
{
    public async Task<CommandResponse<OrdemDeCompraDeletarAnexoResponse>> Handle(OrdemDeCompraDeletarAnexoRequest request, CancellationToken cancellationToken)
    {
        var ordemDeCompra = await repositoryOrdemDeCompra.GetByAsync(true, f => f.Id == request.Id, cancellationToken, p => p.Anexos);
        if (ordemDeCompra == null)
        {
            AddNotification("OrdemDeCompra", OrdemDeCompraResources.OrdemDeCompraNaoEncontrada);
            return new CommandResponse<OrdemDeCompraDeletarAnexoResponse>(this);
        }

        var anexo = ordemDeCompra.Anexos.FirstOrDefault(x => x.Id == request.AnexoId);
        if (anexo == null)
        {
            AddNotification("Anexo", OrdemDeCompraResources.AnexoNaoEncontrado);
            return new CommandResponse<OrdemDeCompraDeletarAnexoResponse>(this);
        }

        ordemDeCompra.DeletarAnexo(anexo);

		if (ordemDeCompra.Anexos.Count == 0)
		{
			ordemDeCompra.Status = EStatusOrdemDeCompra.AguardandoPi;
		}

		if (IsInvalid())
            return new CommandResponse<OrdemDeCompraDeletarAnexoResponse>(this);

        repositoryOrdemDeCompra.Update(ordemDeCompra);
        await mediator.Publish(new AuditoriaAdicionarInput(ordemDeCompra.Id, TabelasResources.OrdemCompra, "Anexo Deletado", ETipoAuditoria.Modificado));

        var diretorioArquivo = Path.Combine(Directory.GetCurrentDirectory(), "uploads", ordemDeCompra.Id.ToString());
        var caminhoArquivo = Path.Combine(diretorioArquivo, anexo.Nome);

        if (File.Exists(caminhoArquivo))
        {
            File.Delete(caminhoArquivo);
        }

        var response = new OrdemDeCompraDeletarAnexoResponse(anexo.Id, OrdemDeCompraResources.AnexoDeletadoComSucesso);
        return new CommandResponse<OrdemDeCompraDeletarAnexoResponse>(response, this);
    }
}