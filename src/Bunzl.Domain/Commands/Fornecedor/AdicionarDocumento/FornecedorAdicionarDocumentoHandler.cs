using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarDocumento;

public class FornecedorAdicionarDocumentoHandler(IPublisher mediator, IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<FornecedorAdicionarDocumentoRequest, CommandResponse<FornecedorAdicionarDocumentoResponse>>
{
    public async Task<CommandResponse<FornecedorAdicionarDocumentoResponse>> Handle(FornecedorAdicionarDocumentoRequest request, CancellationToken cancellationToken)
    {
        var tamanhoMaximoMB = 25 * 1024 * 1024;
        if (request.Arquivo.Length > tamanhoMaximoMB)
        {
            AddNotification("Arquivo", FornecedorResources.DocumentoTamanhoMaximo);
            return new CommandResponse<FornecedorAdicionarDocumentoResponse>(this);
        }

        var fornecedor = await repositoryFornecedor.GetByAsync(true, f => f.Id == request.FornecedorId, cancellationToken, p => p.FornecedorDocumentos);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<FornecedorAdicionarDocumentoResponse>(this);
        }

        var diretorioArquivo = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fornecedor.Id.ToString());
        if (!Directory.Exists(diretorioArquivo))
            Directory.CreateDirectory(diretorioArquivo);

        var caminhoArquivo = Path.Combine(diretorioArquivo, request.Arquivo.FileName);
        if (Path.Exists(caminhoArquivo))
        {
            AddNotification("Arquivo", FornecedorResources.ArquivoJaExiste);
            return new CommandResponse<FornecedorAdicionarDocumentoResponse>(this);
        }

        await using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
        {
            await request.Arquivo.CopyToAsync(stream, cancellationToken);
        }

        var documento = new Entities.FornecedorDocumento
        (
            request.Arquivo.FileName,
            request.Arquivo.ContentType,
            request.Observacao,
            fornecedor.Id
        );

        fornecedor.AdicionarDocumento(documento);

        repositoryFornecedor.Update(fornecedor);
        await mediator.Publish(new AuditoriaAdicionarInput(documento.Id, TabelasResources.Fornecedor, "Documento Adicionado", ETipoAuditoria.Modificado));

        return new CommandResponse<FornecedorAdicionarDocumentoResponse>(
            new FornecedorAdicionarDocumentoResponse(documento.Id, FornecedorResources.DocumentoAdicionadoComSucesso, documento),
            this);
    }
}
