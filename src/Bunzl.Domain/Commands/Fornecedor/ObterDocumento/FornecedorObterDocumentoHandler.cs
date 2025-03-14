using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ObterDocumento;

public class FornecedorObterDocumentoHandler(IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<FornecedorObterDocumentoRequest, CommandResponse<FornecedorObterDocumentoResponse>>
{
    public async Task<CommandResponse<FornecedorObterDocumentoResponse>> Handle(FornecedorObterDocumentoRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(
            false,
            f => f.Id == request.Id,
            cancellationToken,
            f => f.FornecedorDadoBancario,
            f => f.FornecedorDocumentos);

        if (fornecedor is not null)
        {
            var documento = fornecedor.FornecedorDocumentos.Where(x => x.Id == request.DocumentoId).FirstOrDefault();

            if (documento is not null)
            {
                try
                {
                    var arquivoPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", request.Id.ToString()!, documento.Nome);
                    byte[] arquivoBytes;

                    if (File.Exists(arquivoPath))
                    {
                        arquivoBytes = await File.ReadAllBytesAsync(arquivoPath, cancellationToken);
                    }
                    else
                    {
                        arquivoBytes = Array.Empty<byte>();
                    }

                    var arquivoBase64 = Convert.ToBase64String(arquivoBytes);
                    var documentoResponse = new FornecedorObterDocumentoResponse(documento.Id, documento.Nome, documento.Tipo, documento.Observacao, documento.FornecedorId, documento.DataCriacao, arquivoBase64);

                    return new CommandResponse<FornecedorObterDocumentoResponse>(documentoResponse, this);
                }
                catch (Exception ex)
                {
                    var documentoResponse = new FornecedorObterDocumentoResponse(documento.Id, documento.Nome, documento.Tipo, documento.Observacao, documento.FornecedorId, documento.DataCriacao);
                    return new CommandResponse<FornecedorObterDocumentoResponse>(documentoResponse, this);
                }
            }
        }

        AddNotification("Documento", FornecedorResources.DocumentoNaoEncontrado);
        return new CommandResponse<FornecedorObterDocumentoResponse>(this);
    }
}
