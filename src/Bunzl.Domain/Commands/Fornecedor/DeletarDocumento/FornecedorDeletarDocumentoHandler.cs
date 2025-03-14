using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.DeletarDocumento;

public class FornecedorDeletarDocumentoHandler(IPublisher mediator, IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<FornecedorDeletarDocumentoRequest, CommandResponse<FornecedorDeletarDocumentoResponse>>
{
    public async Task<CommandResponse<FornecedorDeletarDocumentoResponse>> Handle(FornecedorDeletarDocumentoRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(true, f => f.Id == request.Id, cancellationToken, p => p.FornecedorDocumentos);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<FornecedorDeletarDocumentoResponse>(this);
        }

        var documento = fornecedor.FornecedorDocumentos.Where(x => x.Id == request.DocumentoId).FirstOrDefault();
        if (documento == null)
        {
            AddNotification("Documento", FornecedorResources.DocumentoNaoEncontrado);
            return new CommandResponse<FornecedorDeletarDocumentoResponse>(this);
        }

        fornecedor.DeletarDocumento(documento);

        if (IsInvalid())
            return new CommandResponse<FornecedorDeletarDocumentoResponse>(this);

        repositoryFornecedor.Update(fornecedor);
        await mediator.Publish(new AuditoriaAdicionarInput(fornecedor.Id, TabelasResources.Fornecedor, "Documento Deletado", ETipoAuditoria.Modificado));

        var diretorioArquivo = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fornecedor.Id.ToString());
        var caminhoArquivo = Path.Combine(diretorioArquivo, documento.Nome);

        if (File.Exists(caminhoArquivo))
        {
            File.Delete(caminhoArquivo);
        }

        var response = new FornecedorDeletarDocumentoResponse(documento.Id, FornecedorResources.DocumentoDeletadoComSucesso);
        return new CommandResponse<FornecedorDeletarDocumentoResponse>(response, this);
    }
}