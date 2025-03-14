using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.ObterAnexo;

public class OrdemDeCompraObterAnexoHandler(IRepositoryOrdemDeCompra repositoryOrdemDeCompra)
	: Notifiable, IRequestHandler<OrdemDeCompraObterAnexoRequest, CommandResponse<OrdemDeCompraObterAnexoResponse>>
{
	public async Task<CommandResponse<OrdemDeCompraObterAnexoResponse>> Handle(OrdemDeCompraObterAnexoRequest request, CancellationToken cancellationToken)
    {
        var anexo = await repositoryOrdemDeCompra.ObterAnexoAsync(request.Id);

		if (anexo == null)
		{
			AddNotification("Anexo", OrdemDeCompraResources.AnexoNaoEncontrado);
			return new CommandResponse<OrdemDeCompraObterAnexoResponse>(this);
		}

		try
		{
			var diretorioArquivo = Path.Combine(Directory.GetCurrentDirectory(), "uploads", anexo.OrdemDeCompraId.ToString());
			var caminhoArquivo = Path.Combine(diretorioArquivo, anexo.Nome);

			byte[] arquivoBytes;

			if (File.Exists(caminhoArquivo))
			{
				arquivoBytes = await File.ReadAllBytesAsync(caminhoArquivo, cancellationToken);
			}
			else
			{
				arquivoBytes = Array.Empty<byte>();
			}

			var arquivoBase64 = Convert.ToBase64String(arquivoBytes);
			var anexoResponse = new OrdemDeCompraObterAnexoResponse(anexo.Id, anexo.OrdemDeCompraId, anexo.Nome, anexo.Tipo, anexo.DataCriacao, arquivoBase64);

			return new CommandResponse<OrdemDeCompraObterAnexoResponse>(anexoResponse, this);
		}
		catch (Exception ex)
		{
			var anexoResponse = new OrdemDeCompraObterAnexoResponse(anexo.Id, anexo.OrdemDeCompraId, anexo.Nome, anexo.Tipo, anexo.DataCriacao);
			return new CommandResponse<OrdemDeCompraObterAnexoResponse>(anexoResponse, this);
		}
	}
}
