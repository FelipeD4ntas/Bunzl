using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Adicionar;

public class NegociacaoComercialAdicionarHandler(
    IPublisher mediator,
    IRepositoryFornecedor repositoryFornecedor,
    IRepositoryNegociacaoComercial repositoryNegociacaoComercial,
    IRepositoryEmpresa repositoryEmpresa,
    IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<NegociacaoComercialAdicionarRequest, CommandResponse<NegociacaoComercialAdicionarResponse>>
{
    public async Task<CommandResponse<NegociacaoComercialAdicionarResponse>> Handle(NegociacaoComercialAdicionarRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(true, c => c.Id == request.FornecedorId, true, cancellationToken);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<NegociacaoComercialAdicionarResponse>(this);
        }

        var empresa = await repositoryEmpresa.GetByAsync(true, c => c.Id == usuarioAutenticado.UsuarioEmpresa, true, cancellationToken);
        if (empresa == null)
        {
            AddNotification("Fornecedor", EmpresaResources.NenhumaEmpresaVinculadaFornecedor);
            return new CommandResponse<NegociacaoComercialAdicionarResponse>(this);
        }

        var codigo = await GerarCodigoNegociacao(empresa.Id);

		var negociacaoComercial = new Entities.NegociacaoComercial(
            codigo,
            request.FornecedorId,
            empresa.Id,
            request.Titulo,
            request.DataEntrega,
            request.CampoAtuacao,
            request.TermosPagamento,
            request.ValorTotal);

        if (request.Produtos is not null && request.Produtos.Count > 0)
        {
            negociacaoComercial.Produtos = request.Produtos.Select(produtoDto =>
                new Entities.NegociacaoComercialProduto(
                    produtoDto.ProdutoId,
                    produtoDto.CodigoSku,
                    produtoDto.Descricao,
                    produtoDto.ValorUnitarioOriginal,
                    produtoDto.Quantidade,
                    produtoDto.ValorUnitarioNegociado,
                    produtoDto.Observacao,
                    negociacaoComercial.Id,
                    produtoDto.ValorUnitarioAlvo,
                    produtoDto.ValorUnitarioFinal
                    )).ToList();
        }

        if (request.Observacoes is not null && request.Observacoes.Count > 0)
        {
	        negociacaoComercial.NegociacaoComercialObservacoes = request.Observacoes.Select(observacaoDto =>
		        new Entities.NegociacaoComercialObservacao(
			        observacaoDto.Observacao,
                    negociacaoComercial.Id
			        )).ToList();
        }

		if (request.Anexos is not null && request.Anexos.Count > 0)
		{
			var tamanhoMaximoMB = 25 * 1024 * 1024;
			var anexos = new List<Entities.NegociacaoComercialAnexo>();

			var diretorioArquivo = Path.Combine(Directory.GetCurrentDirectory(), "uploads", codigo.ToString());
			if (!Directory.Exists(diretorioArquivo))
			{
				Directory.CreateDirectory(diretorioArquivo);
			}

			foreach (var anexoDto in request.Anexos)
			{
				if (anexoDto.Arquivo.Length > tamanhoMaximoMB)
				{
					AddNotification("Arquivo", NegociacaoComercialResources.AnexoTamanhoMaximo);
					return new CommandResponse<NegociacaoComercialAdicionarResponse>(this);
				}

				var caminhoArquivo = Path.Combine(diretorioArquivo, anexoDto.Arquivo.FileName);

				await using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
				{
					await anexoDto.Arquivo.CopyToAsync(stream, cancellationToken);
				}

				anexos.Add(new Entities.NegociacaoComercialAnexo(
					negociacaoComercial.Id,
					anexoDto.Nome,
					anexoDto.Tipo,
					anexoDto.Observacao));
			}

			negociacaoComercial.Anexos = anexos;
		}


		if (IsInvalid())
            return await Task.FromResult(new CommandResponse<NegociacaoComercialAdicionarResponse>(this));

        await repositoryNegociacaoComercial.AddAsync(negociacaoComercial, cancellationToken);
        await mediator.Publish(new AuditoriaAdicionarInput(negociacaoComercial.Id, TabelasResources.NegociacaoComercial, "Criado", Enumerators.ETipoAuditoria.Adicionado), cancellationToken);

        return new CommandResponse<NegociacaoComercialAdicionarResponse>(
            new NegociacaoComercialAdicionarResponse(negociacaoComercial.Id, NegociacaoComercialResources.NegociacaoComercialAdicionadaComSucesso, fornecedor, empresa.Nome), this);
    }

    private async Task<long> GerarCodigoNegociacao(Guid empresaId)
    {
		var negociacoes = await repositoryNegociacaoComercial.ListAsync(true, n => n.EmpresaId == empresaId, false);
	    var ultimoCodigo = negociacoes.Any() ? negociacoes.Max(n => n.Codigo) : 0;

		return ultimoCodigo + 1;
    }
}

