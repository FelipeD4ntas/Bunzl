using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.NegociacaoComercial.ObterAnexoNegociacaoComercial;

public class NegociacaoComercialObterAnexoHandler(IRepositoryNegociacaoComercial repositoryNegociacaoComercial)
	: Notifiable, IRequestHandler<NegociacaoComercialObterAnexoRequest, CommandResponse<NegociacaoComercialObterAnexoResponse>>
{
	public async Task<CommandResponse<NegociacaoComercialObterAnexoResponse>> Handle(NegociacaoComercialObterAnexoRequest request, CancellationToken cancellationToken)
	{
		var negociacaoComercial = await repositoryNegociacaoComercial.GetByAsync(
			false,
			nc => nc.Id == request.Id,
			cancellationToken,
			nc => nc.Anexos);

		if (negociacaoComercial == null)
		{
			AddNotification("NegociacaoComercial", NegociacaoComercialResources.NegociacaoComercialNaoEncontrada);
			return new CommandResponse<NegociacaoComercialObterAnexoResponse>(this);
		}

		var anexo = negociacaoComercial.Anexos
			.FirstOrDefault(a => a.Id == request.AnexoId);

		if (anexo == null)
		{
			AddNotification("Anexo", NegociacaoComercialResources.AnexoNaoEncontrado);
			return new CommandResponse<NegociacaoComercialObterAnexoResponse>(this);
		}

		try
		{
			var diretorioArquivo = Path.Combine(Directory.GetCurrentDirectory(), "uploads", negociacaoComercial.Codigo.ToString());
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
			var anexoResponse = new NegociacaoComercialObterAnexoResponse(anexo.Id, negociacaoComercial.Id, anexo.Nome, anexo.Tipo, anexo.DataCriacao, arquivoBase64);

			return new CommandResponse<NegociacaoComercialObterAnexoResponse>(anexoResponse, this);
		}
		catch (Exception ex)
		{
			var anexoResponse = new NegociacaoComercialObterAnexoResponse(anexo.Id, negociacaoComercial.Id, anexo.Nome, anexo.Tipo, anexo.DataCriacao);
			return new CommandResponse<NegociacaoComercialObterAnexoResponse>(anexoResponse, this);
		}
	}
}
