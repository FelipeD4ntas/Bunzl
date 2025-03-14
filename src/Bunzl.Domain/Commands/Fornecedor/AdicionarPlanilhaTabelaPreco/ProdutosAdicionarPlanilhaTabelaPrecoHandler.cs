using Bunzl.Domain.Enumerators;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;
using ClosedXML.Excel;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.DTOs;
using Bunzl.Core.Domain.DTOs.DevExpress;
using System.Globalization;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaTabelaPreco;

public class ProdutosAdicionarPlanilhaTabelaPrecoHandler(
    IPublisher mediator,
    IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<ProdutosAdicionarPlanilhaTabelaPrecoRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(ProdutosAdicionarPlanilhaTabelaPrecoRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(true, f => f.Id == request.FornecedorId, cancellationToken, p => p.FornecedorProdutos);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<DataSourcePageResponse>(this);
        }

        if (fornecedor.Status != EStatusFornecedor.Homologado)
        {
            AddNotification("Fornecedor", FornecedorResources.SomenteFornecedorHomologadoPodeAdicionarTabelaPreco);
            return new CommandResponse<DataSourcePageResponse>(this);
        }

        if (request.Arquivo == null || request.Arquivo.Length == 0)
        {
            AddNotification("Arquivo", FornecedorResources.PlanilhaNaoEnviado);
            return new CommandResponse<DataSourcePageResponse>(this);
        }

        var diretorioPlanilha = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "planilha_tabela_preco");

        if (!Directory.Exists(diretorioPlanilha))
        {
            Directory.CreateDirectory(diretorioPlanilha);
        }

        var dataString = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss", CultureInfo.InvariantCulture);
        var caminhoArquivo = Path.Combine(diretorioPlanilha, $"3-PRICE_LIST - SUPPLIER_{fornecedor.Id}_{dataString}.xlsx");

        await using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
        {
            await request.Arquivo.CopyToAsync(stream, cancellationToken);
        }

        var listaProdutos = new List<FornecedorProdutoTabelaPrecoDto>();

        using (var workbook = new XLWorkbook(caminhoArquivo))
        {
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RowsUsed();

            var expectedHeaders = new List<string>
            {                                   // Coluna na planilha  
                "ID Item Portal",               // 2
                "Supplier ID",                  // 3  
                "Supplier Name",                // 4
                "Importer Product Code",        // 5
                "Importer Product Description", // 6
                "Supplier Product Code",        // 7
                "Supplier Product Description", // 8
                "Unit of Measure",              // 9
                "Currency",                     // 10
                "Last Applied Price",           // 11
                "New Price"                     // 12
            };
            
            var headerRow = worksheet.Row(5);
            var actualHeaders = headerRow.Cells(2, 12).Select(c => c.GetValue<string>().Trim()).ToList();

            if (!actualHeaders.SequenceEqual(expectedHeaders))
            {
	            AddNotification("Planilha", PlanilhaResources.ColunasPlanilhaInvalidas);
	            return new CommandResponse<DataSourcePageResponse>(this);
            }

			foreach (var row in rows)
            {
                if (row.RowNumber() <= 6) continue;

                var erros = new List<string>();
                var isValid = true;

                var produtoId = row.Cell(2).GetValue<string>();
                var codigoSku = row.Cell(5).GetValue<string>();
                var descricaoProdutoBunzl = row.Cell(6).GetValue<string>();
                var codigoProdutoFornecedor = row.Cell(7).GetValue<string>();
                var descricaoProdutoFornecedor = row.Cell(8).GetValue<string>();
                var unidadeMedidaFornecedorPreco = row.Cell(9).GetValue<string>();
                var moeda = row.Cell(10).GetValue<string>();
                var ultimoPrecoPraticadoEhValido = row.Cell(11).TryGetValue<decimal>(out var ultimoPrecoPraticado);
                var novoPrecoEhValido = row.Cell(12).TryGetValue<decimal>(out var novoPreco);

				if (string.IsNullOrWhiteSpace(produtoId))
                {
	                isValid = false;
	                erros.Add(string.Format(PlanilhaResources.CampoVazio, row.RowNumber(), 2, "ID Item no Portal"));
                }

                if (!ultimoPrecoPraticadoEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), 11, PlanilhaResources.UltimoPrecoPraticado));
                }

                if (!novoPrecoEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), 12, PlanilhaResources.NovoPreco));
                }

                if (!isValid)
                {
                    AddNotification("Planilha", string.Join("; ", erros));
                    continue;
                }

                var produto = new FornecedorProdutoTabelaPrecoDto
                {
                    ProdutoId = Guid.Parse(produtoId),
                    CodigoSku = codigoSku,
                    DescricaoProdutoBunzl = descricaoProdutoBunzl,
                    CodigoProdutoFornecedor = codigoProdutoFornecedor,
                    DescricaoProdutoFornecedor = descricaoProdutoFornecedor,
                    UnidadeMedidaFornecedor = unidadeMedidaFornecedorPreco,
                    Moeda = moeda,
                    UltimoPrecoPraticado = ultimoPrecoPraticado,
                    NovoPreco = novoPreco
                };

                listaProdutos.Add(produto);
            }
        }

        if (listaProdutos.Any(p => p.UltimoPrecoPraticado == 0 || p.NovoPreco == 0))
        {
            AddNotification("Preço", FornecedorResources.PrecosDosProdutosDevemSerMaiorQueZero);
            return new CommandResponse<DataSourcePageResponse>(this);
        }

        if (IsInvalid())
            return await Task.FromResult(new CommandResponse<DataSourcePageResponse>(this));

        var dataSourcePageResponse = new DataSourcePageResponse
        {
            Data = listaProdutos,
            Summary = null
        };

        return new CommandResponse<DataSourcePageResponse>(dataSourcePageResponse, this);
    }
}
