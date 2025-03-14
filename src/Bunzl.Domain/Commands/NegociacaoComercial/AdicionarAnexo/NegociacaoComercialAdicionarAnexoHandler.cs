using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.NegociacaoComercial.AdicionarAnexo;

public class NegociacaoComercialAdicionarAnexoHandler(IPublisher mediator, IRepositoryNegociacaoComercial repositoryNegociacaoComercial)
	: Notifiable, IRequestHandler<NegociacaoComercialAdicionarAnexoRequest, CommandResponse<NegociacaoComercialAdicionarAnexoResponse>>
{
    public async Task<CommandResponse<NegociacaoComercialAdicionarAnexoResponse>> Handle(NegociacaoComercialAdicionarAnexoRequest request, CancellationToken cancellationToken)
    {
        var tamanhoMaximoMB = 25 * 1024 * 1024;
        if (request.Arquivo.Length > tamanhoMaximoMB)
        {
            AddNotification("Arquivo", FornecedorResources.DocumentoTamanhoMaximo);
            return new CommandResponse<NegociacaoComercialAdicionarAnexoResponse>(this);
        }

        var negociacaoComercial = await repositoryNegociacaoComercial.GetByAsync(true, f => f.Id == request.NegociacaoComercialId, cancellationToken, p => p.Anexos);
        if (negociacaoComercial == null)
        {
            AddNotification("Fornecedor", NegociacaoComercialResources.NegociacaoComercialNaoEncontrada);
            return new CommandResponse<NegociacaoComercialAdicionarAnexoResponse>(this);
        }

        var diretorioArquivo = Path.Combine(Directory.GetCurrentDirectory(), "uploads", negociacaoComercial.Codigo.ToString());
        if (!Directory.Exists(diretorioArquivo))
            Directory.CreateDirectory(diretorioArquivo);

        var caminhoArquivo = Path.Combine(diretorioArquivo, request.Arquivo.FileName);
        if (Path.Exists(caminhoArquivo))
        {
            AddNotification("Arquivo", FornecedorResources.ArquivoJaExiste);
            return new CommandResponse<NegociacaoComercialAdicionarAnexoResponse>(this);
        }

        await using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
        {
            await request.Arquivo.CopyToAsync(stream, cancellationToken);
        }

        var documento = new Entities.NegociacaoComercialAnexo
		(
            negociacaoComercial.Id,
            request.Arquivo.FileName,
            request.Arquivo.ContentType,
            request.Observacao
        );

        negociacaoComercial.AdicionarAnexo(documento);

		repositoryNegociacaoComercial.Update(negociacaoComercial);
        await mediator.Publish(new AuditoriaAdicionarInput(documento.Id, TabelasResources.NegociacaoComercial, "Anexo Adicionado", ETipoAuditoria.Modificado));

        return new CommandResponse<NegociacaoComercialAdicionarAnexoResponse>(
            new NegociacaoComercialAdicionarAnexoResponse(documento.Id, NegociacaoComercialResources.AnexoAdicionadoComSucesso, documento),
            this);
    }
}
