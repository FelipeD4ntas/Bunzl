using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;
using ClosedXML.Excel;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.DTOs;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using Bunzl.Core.Domain.Interfaces.ExternalService;
using Bunzl.Infra.CrossCutting.Helper;
using System.Globalization;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaCadastroProdutoBunzl;

public class ProdutosAdicionarPlanilhaTabelaPrecoHandler(
    IExternalServiceFornecedorProduto externalServiceFornecedorProduto,
    IPublisher mediator,
    IRepositoryFornecedor repositoryFornecedor,
    IRepositoryEmpresa repositoryEmpresa,
    IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<ProdutosAdicionarPlanilhaBunzlRequest, CommandResponse<ProdutosAdicionarPlanilhaBunzlResponse>>
{
    public async Task<CommandResponse<ProdutosAdicionarPlanilhaBunzlResponse>> Handle(ProdutosAdicionarPlanilhaBunzlRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(true, f => f.Id == request.FornecedorId, cancellationToken, p => p.FornecedorProdutos);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<ProdutosAdicionarPlanilhaBunzlResponse>(this);
        }

        var empresa = await repositoryEmpresa.GetByAsync(true, u => u.Id == usuarioAutenticado.UsuarioEmpresa, false, cancellationToken, e => e.Usuarios);

        if (empresa == null)
        {
            AddNotification("Empresa", FornecedorResources.FornecedorFalhaRelacionarEmpresa);
            return new CommandResponse<ProdutosAdicionarPlanilhaBunzlResponse>(this);
        }

        var produtosERP = await SafeExecutionHelper.SafeExecuteAsync(
            () => externalServiceFornecedorProduto.ObterTodosProdutos(empresa.Cnpj, cancellationToken),
            AddNotification,
            "Fornecedor",
            IntegracaoResources.ProdutoNaoEncontradoNoErp
		);

        if (produtosERP == null)
            return new CommandResponse<ProdutosAdicionarPlanilhaBunzlResponse>(this);

        if (request.Arquivo == null || request.Arquivo.Length == 0)
        {
            AddNotification("Arquivo", FornecedorResources.PlanilhaNaoEnviado);
            return new CommandResponse<ProdutosAdicionarPlanilhaBunzlResponse>(this);
        }

        var diretorioPlanilha = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "planilha_produtos");

        if (!Directory.Exists(diretorioPlanilha))
            Directory.CreateDirectory(diretorioPlanilha);

        var dataString = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss", CultureInfo.InvariantCulture);
        var caminhoArquivo = Path.Combine(diretorioPlanilha, $"2-PRODUCT_ASSOCIATION - BUNZL x SUPPLIER_{fornecedor.Id}_{dataString}.xlsx");

        await using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
        {
            await request.Arquivo.CopyToAsync(stream, cancellationToken);
        }

        var produtosComErro = new List<FornecedorProdutoErrosDto>();
        int totalProdutos = 0;
        int totalLinhasProcessadas = 0;

        using (var workbook = new XLWorkbook(caminhoArquivo))
        {
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RowsUsed();

            var expectedHeaders = new List<string>
            {                                   // Coluna na planilha
                "ID Item Portal",               // 2 
                "Supplier Product Code",        // 3  
                "Supplier Product Description", // 4
                "Color",                        // 5
                "Size / Dimension",             // 6
                "Unit of Measure",              // 7
                "Last Applied Price",           // 8
                "Importer Product Code",        // 9
                "Approved?"                     // 10
            };

            var headerRow = worksheet.Row(5);
            var actualHeaders = headerRow.Cells(2, 10).Select(c => c.GetValue<string>().Trim()).ToList();

            if (!actualHeaders.SequenceEqual(expectedHeaders))
            {
	            AddNotification("Planilha", PlanilhaResources.ColunasPlanilhaInvalidas);
				return new CommandResponse<ProdutosAdicionarPlanilhaBunzlResponse>(this);
			}

			foreach (var row in rows)
            {
                if (row.RowNumber() <= 6) continue;

                totalLinhasProcessadas++;

                var id = row.Cell(2).GetValue<string>();
                var codigoItemFornecedor = row.Cell(3).GetValue<string>();
                var descricaoItemFornecedor = row.Cell(4).GetValue<string>();
                var codigoSKU = row.Cell(9).GetValue<string>();
                var status = row.Cell(10).GetValue<string>().Equals("Sim", StringComparison.CurrentCultureIgnoreCase) ? EStatusProduto.Homologado : EStatusProduto.NaoHomologado;

                var produto = fornecedor.FornecedorProdutos.FirstOrDefault(x => x.Id == Guid.Parse(id));
                if (produto is null)
                {
                    produtosComErro.Add(new FornecedorProdutoErrosDto
                    {
                        DescricaoProduto = descricaoItemFornecedor,
                        Erros = [string.Format(PlanilhaResources.CodigoProdutoNaoEncontrado, codigoItemFornecedor)]
                    });
                    continue;
                }

                if (string.IsNullOrEmpty(codigoSKU))
                {
                    AddNotification("Planilha", string.Format(PlanilhaResources.CodigoSkuDeveSerPreenchidoLinha, row.RowNumber(), 9));
                    continue;
                }

                var produtoApi = produtosERP.FirstOrDefault(x => x.SKU == codigoSKU);
                if (produtoApi == null)
                {
                    produtosComErro.Add(new FornecedorProdutoErrosDto
                    {
                        DescricaoProduto = descricaoItemFornecedor,
                        Erros = [string.Format(FornecedorResources.CodigoProdutoERPNaoEncontrado, codigoSKU)]
                    });
					continue;
                }

                produto.AssociarProdutoBunzl(produtoApi.Descricao, produtoApi.UnidadeMedida, codigoSKU, produtoApi.CodigoArtigo, produtoApi.Familia, produtoApi.Cor, produtoApi.Tamanho, status);
                totalProdutos++;
			}
		}

        if (IsInvalid())
            return await Task.FromResult(new CommandResponse<ProdutosAdicionarPlanilhaBunzlResponse>(this));

        repositoryFornecedor.Update(fornecedor);

        if (produtosComErro.Count != 0 && produtosComErro.Count == totalLinhasProcessadas)
        {
            AddNotification("Produto", FornecedorResources.TodosProdutosPlanilhaFalharam);
            return await Task.FromResult(new CommandResponse<ProdutosAdicionarPlanilhaBunzlResponse>(new ProdutosAdicionarPlanilhaBunzlResponse(fornecedor.Id, FornecedorResources.TodosProdutosPlanilhaFalharam, produtosComErro), this));
        }

        await mediator.Publish(new AuditoriaAdicionarInput(new Guid(), TabelasResources.FornecedorProduto, FornecedorResources.ProdutoAssociadosEmLoteComSucesso, ETipoAuditoria.Modificado));

        return new CommandResponse<ProdutosAdicionarPlanilhaBunzlResponse>(
            new ProdutosAdicionarPlanilhaBunzlResponse(fornecedor.Id, FornecedorResources.ProdutoAssociadosEmLoteComSucesso, produtosComErro, totalProdutos),
            this
        );
    }
}
