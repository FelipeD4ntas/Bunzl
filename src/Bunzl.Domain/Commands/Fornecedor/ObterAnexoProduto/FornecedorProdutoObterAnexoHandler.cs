using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ObterAnexoProduto;

public class FornecedorProdutoObterAnexoHandler(IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<FornecedorProdutoObterAnexoRequest, CommandResponse<FornecedorProdutoObterAnexoResponse>>
{
    public async Task<CommandResponse<FornecedorProdutoObterAnexoResponse>> Handle(FornecedorProdutoObterAnexoRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(
            true,
            f => f.Id == request.FornecedorId,
            cancellationToken,
            "FornecedorProdutos", "FornecedorProdutos.FornecedorProdutoAnexos"
        );

        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<FornecedorProdutoObterAnexoResponse>(this);
        }

        var produto = fornecedor.FornecedorProdutos
            .FirstOrDefault(p => p.Id == request.FornecedorProdutoId);

        if (produto == null)
        {
            AddNotification("Produto", FornecedorResources.ProdutoNaoEncontrado);
            return new CommandResponse<FornecedorProdutoObterAnexoResponse>(this);
        }

        var anexo = produto.FornecedorProdutoAnexos
            .FirstOrDefault(a => a.Id == request.AnexoId);

        if (anexo == null)
        {
            AddNotification("Anexo", FornecedorResources.AnexoNaoEncontrado);
            return new CommandResponse<FornecedorProdutoObterAnexoResponse>(this);
        }

        try
        {
            var arquivoPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fornecedor.Id.ToString(), produto.Id.ToString(), anexo.Nome);
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
            var anexoResponse = new FornecedorProdutoObterAnexoResponse(anexo.Id, anexo.Nome, anexo.Tipo, anexo.TipoDocumento, anexo.Observacao, anexo.DataCriacao, anexo.FornecedorProdutoId, arquivoBase64);

            return new CommandResponse<FornecedorProdutoObterAnexoResponse>(anexoResponse, this);
        }
        catch (Exception ex)
        {
            var anexoResponse = new FornecedorProdutoObterAnexoResponse(anexo.Id, anexo.Nome, anexo.Tipo, anexo.TipoDocumento, anexo.Observacao, anexo.DataCriacao, anexo.FornecedorProdutoId);
            return new CommandResponse<FornecedorProdutoObterAnexoResponse>(anexoResponse, this);
        }   
    }
}
