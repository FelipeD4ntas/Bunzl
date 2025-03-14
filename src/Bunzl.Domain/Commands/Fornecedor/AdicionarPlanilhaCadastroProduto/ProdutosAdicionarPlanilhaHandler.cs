using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;
using ClosedXML.Excel;
using Bunzl.Domain.Entities;
using FluentValidation;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.DTOs;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using System.Globalization;
using Bunzl.Domain.Commands.Fornecedor.AtualizarProduto;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaCadastroProduto;

public class ProdutosAdicionarPlanilhaHandler(
    IPublisher mediator, 
    IValidator<FornecedorProduto> _validator, 
    IRepositoryFornecedor repositoryFornecedor, 
    IRepositoryUsuario repositoryUsuario,
    IRepositoryIncoterm repositoryIncoterm,
    IRepositoryProduto repositoryProduto,
    IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<ProdutosAdicionarPlanilhaRequest, CommandResponse<ProdutosAdicionarPlanilhaResponse>>
{
    public async Task<CommandResponse<ProdutosAdicionarPlanilhaResponse>> Handle(ProdutosAdicionarPlanilhaRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(true, f => f.Id == request.FornecedorId, cancellationToken, p => p.FornecedorProdutos);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<ProdutosAdicionarPlanilhaResponse>(this);
        }

        var usuarios = await repositoryUsuario.ListAsync(true, u => u.PerfilPermissao == EPerfilUsuario.CompradorKeyUser || u.PerfilPermissao == EPerfilUsuario.AdministradorSuperUser, u => u.Empresas);
        var usuariosAutenticados = usuarios
             .Where(u => u.Empresas.Any(e => e.Id == usuarioAutenticado.UsuarioEmpresa))
             .ToList();

        if (request.Arquivo == null || request.Arquivo.Length == 0)
        {
            AddNotification("Arquivo", FornecedorResources.PlanilhaNaoEnviado);
            return new CommandResponse<ProdutosAdicionarPlanilhaResponse>(this);
        }

        var diretorioPlanilha = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "planilha_produtos");
        if (!Directory.Exists(diretorioPlanilha))
            Directory.CreateDirectory(diretorioPlanilha);

        var dataString = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss", CultureInfo.InvariantCulture);
        var caminhoArquivo = Path.Combine(diretorioPlanilha, $"1-PRODUCT_REGISTRATION - SUPPLIER_{fornecedor.Id}_{dataString}.xlsx");

        await using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
        {
            await request.Arquivo.CopyToAsync(stream, cancellationToken);
        }

        var produtosComErro = new List<FornecedorProdutoErrosDto>();
        int totalProdutos = 0;

        using (var workbook = new XLWorkbook(caminhoArquivo))
        {
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RowsUsed();
            var expectedHeaders = new List<string>
            {                                               // Coluna na planilha  
                "Supplier Product Code",                    // 2
                "Supplier Product Description",             // 3
                "Size / Dimension",                         // 4
                "Color",                                    // 5
                "HS Code (N.C.M)",                          // 6
                "Inner Packaging Type",                     // 7
                "Qty per Inner Packaging",                  // 8
                "Master Case Type",                         // 9
                "Qty per Master Case",                      // 10
                "Master Carton Gross Weight (kg)",          // 11
                "Mainly Applications",                      // 12
                "Composition",                              // 13
                "Packing Details",                          // 14
                "Packaging Development Cost",               // 15 
                "Label & Packaging Cost",                   // 16
                "MOQ (Minimum Order Quantity)",             // 17
                "Unit of Measure (MOQ)",                    // 18
                "Price",                                    // 19
                "Unit of Measure (Price)",                  // 20
                "Port of Loading",                          // 21
                "Lead Time (days)",                         // 22
                "Incoterm",                                 // 23
                "Factory Monthly Capacity",                 // 24
                "UoM (Monthly Capacity)",                   // 25
                "Raw Material (%)",                         // 26
                "Fuel (%)",                                 // 27
                "Packaging (%)",                            // 28
                "Labor (%)",                                // 29
                "Energy (%)",                               // 30
                "Transportation (%)",                       // 31
                "Factory Name",                             // 32
                "Payment Terms",                            // 33
                "Remarks",                                  // 34
                "Length (Cm)",                              // 35
                "Width (Cm)",                               // 36
                "Height (Cm)",                              // 37
                "20ft",                                     // 38
                "40ft",                                     // 39
                "40HC"                                      // 40
            };

            var headerRow = worksheet.Row(5);
            var actualHeaders = headerRow.Cells(2, 40).Select(c => c.GetValue<string>().Trim()).ToList();

            if (!actualHeaders.SequenceEqual(expectedHeaders))
            {
	            AddNotification("Planilha", PlanilhaResources.ColunasPlanilhaInvalidas);
				return new CommandResponse<ProdutosAdicionarPlanilhaResponse>(this);
			}

			foreach (var row in rows)
            {
                if (row.RowNumber() <= 6) continue;

                totalProdutos++;
                bool isValid = true;
                var erros = new List<string>();

                var fornecedorId = request.FornecedorId;
                var codigoFornecedor = row.Cell(2).GetValue<string>();
                var descricaoCompletaFornecedor = row.Cell(3).GetValue<string>();
                var tamanho = row.Cell(4).GetValue<string>();
                var cor = row.Cell(5).GetValue<string>();
                var codigoNcm = row.Cell(6).GetValue<string>();
                var tipoEmbalagemInterna = row.Cell(7).GetValue<string>();
                var quantidadePorEmbalagemInternaEhValida = row.Cell(8).TryGetValue<int>(out var quantidadePorEmbalagemInterna);
                var tipoCaixaMaster = row.Cell(9).GetValue<string>();
                var quantidadePorCaixaMasterEhValida = row.Cell(10).TryGetValue<int>(out var quantidadePorCaixaMaster);
                var pesoBrutoEhValido = row.Cell(11).TryGetValue<decimal>(out var pesoBruto);
                var aplicacoesPrincipais = row.Cell(12).GetValue<string>();
                var composicao = row.Cell(13).GetValue<string>();
                var detalhesEmbalagem = row.Cell(14).GetValue<string>();
                var custoDesenvolvimentoEmbalagemEhValido = row.Cell(15).TryGetValue<decimal>(out var custoDesenvolvimentoEmbalagem);
                var custoRotulagemEmbalagemEhValido = row.Cell(16).TryGetValue<decimal>(out var custoRotulagemEmbalagem);
                var quantidadeMinimaPedidoEhValido = row.Cell(17).TryGetValue<int>(out var quantidadeMinimaPedido);
                var unidadeMedidaFornecedorMOQ = row.Cell(18).GetValue<string>();
                var precoEhValido = row.Cell(19).TryGetValue<decimal>(out var preco);
                var unidadeMedidaFornecedorPreco = row.Cell(20).GetValue<string>();
                var portoEmbarque = row.Cell(21).GetValue<string>();
                var tempoEntregaEhValido = row.Cell(22).TryGetValue<int>(out var tempoEntrega);
                var incotermString = row.Cell(23).GetValue<string>().Trim();
                var capacidadeMensalFabricaEhValida = row.Cell(24).TryGetValue<decimal>(out var capacidadeMensalFabrica);
                var unidadeMedidaCapacidadeMensal = row.Cell(25).GetValue<string>();
                var materiaPrimaPercentualEhValida = row.Cell(26).TryGetValue<decimal>(out var materiaPrimaPercentual);
                var combustivelPercentualEhValida = row.Cell(27).TryGetValue<decimal>(out var combustivelPercentual);
                var embalagemPercentualEhValida = row.Cell(28).TryGetValue<decimal>(out var embalagemPercentual);
                var maoDeObraPercentualEhValida = row.Cell(29).TryGetValue<decimal>(out var maoDeObraPercentual);
                var energiaPercentualEhValida = row.Cell(30).TryGetValue<decimal>(out var energiaPercentual);
                var transportePercentualEhValida = row.Cell(31).TryGetValue<decimal>(out var transportePercentual);
                var nomeFabrica = row.Cell(32).GetValue<string>();
                var termoPagamento = row.Cell(33).GetValue<string>();
                var observacoes = row.Cell(34).GetValue<string>();
                var comprimentoEhValido = row.Cell(35).TryGetValue<decimal>(out var comprimento);
                var larguraEhValido = row.Cell(36).TryGetValue<decimal>(out var largura);
                var alturaEhValido = row.Cell(37).TryGetValue<decimal>(out var altura);
                var quantidadeCarregamentoContainer20FtEhValido = row.Cell(38).TryGetValue<int>(out var quantidadeCarregamentoContainer20Ft);
                var quantidadeCarregamentoContainer40FtEhValido = row.Cell(39).TryGetValue<int>(out var quantidadeCarregamentoContainer40Ft);
                var quantidadeCarregamentoContainer40HcEhValido = row.Cell(40).TryGetValue<int>(out var quantidadeCarregamentoContainer40Hc);

                if (string.IsNullOrWhiteSpace(codigoFornecedor))
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.CampoVazio, row.RowNumber(), PlanilhaResources.CodigoFornecedor));
                }

                if (string.IsNullOrWhiteSpace(descricaoCompletaFornecedor))
                {
	                isValid = false;
	                erros.Add(string.Format(PlanilhaResources.CampoVazio, row.RowNumber(), PlanilhaResources.DescricaoCompletaFornecedor));
                }

                if (string.IsNullOrWhiteSpace(codigoNcm))
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.CampoVazio, row.RowNumber(), PlanilhaResources.CodigoNcm));
                }

                if (!quantidadePorEmbalagemInternaEhValida)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterInt, row.RowNumber(), PlanilhaResources.QuantidadeEmbalagemInterna));
                }

                if (!quantidadePorCaixaMasterEhValida)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterInt, row.RowNumber(), PlanilhaResources.QuantidadePorCaixaMaster));
                }

                if (!pesoBrutoEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.PesoBruto));
                }

                if (!custoDesenvolvimentoEmbalagemEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.CustoDesenvolvimentoEmbalagem));
                }

                if (!custoRotulagemEmbalagemEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.CustoRotulagemEmbalagem));
                }

                if (!quantidadeMinimaPedidoEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterInt, row.RowNumber(), PlanilhaResources.QuantidadeMinimaPedido));
                }

                if (string.IsNullOrWhiteSpace(unidadeMedidaFornecedorMOQ))
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.CampoVazio, row.RowNumber(), PlanilhaResources.UnidadeMedidaFornecedorMOQ));
                }

                if (!precoEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.Preco));
                }

                if (string.IsNullOrWhiteSpace(unidadeMedidaFornecedorPreco))
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.CampoVazio, row.RowNumber(), PlanilhaResources.UnidadeMedidaFornecedorPreco));
                }

                if (!tempoEntregaEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterInt, row.RowNumber(), PlanilhaResources.TempoEntrega));
                }

                var incoterm = await repositoryIncoterm.GetByAsync(true, p => p.Sigla.ToLower() == incotermString.ToLower(), true, cancellationToken);
                if (incoterm is null)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.IncontermInvalidoLinha, row.RowNumber(), PlanilhaResources.Incorterm));
                }

                if (!capacidadeMensalFabricaEhValida)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.CapacidadeMensalFabrica));
                }

                if (!materiaPrimaPercentualEhValida)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.MateriaPrimaPercentual));
                }

                if (!combustivelPercentualEhValida)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.CombustivelPercentual));
                }

                if (!embalagemPercentualEhValida)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.EmbalagemPercentual));
                }

                if (!maoDeObraPercentualEhValida)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.MaoDeObraPercentual));
                }

                if (!energiaPercentualEhValida)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.EnergiaPercentual));
                }

                if (!transportePercentualEhValida)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.TransportePercentual));
                }

                if (!comprimentoEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.Comprimento));
                }

                if (!larguraEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.Largura));
                }

                if (!alturaEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.Altura));
                }

                if (!quantidadeCarregamentoContainer20FtEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.QuantidadeCarregamentoContainer20Ft));
                }

                if (!quantidadeCarregamentoContainer40FtEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.QuantidadeCarregamentoContainer40Ft));
                }

                if (!quantidadeCarregamentoContainer40HcEhValido)
                {
                    isValid = false;
                    erros.Add(string.Format(PlanilhaResources.ErroConverterDecimal, row.RowNumber(), PlanilhaResources.QuantidadeCarregamentoContainer40Hc));
                }

                if (!isValid)
                {
                    produtosComErro.Add(new FornecedorProdutoErrosDto
                    {
                        DescricaoProduto = descricaoCompletaFornecedor,
                        Erros = erros
                    });
                    continue;
                }

                var produtoExistente = fornecedor.FornecedorProdutos.FirstOrDefault(p => p.CodigoFornecedor == codigoFornecedor);

                if (produtoExistente != null)
                {
                    produtoExistente.FornecedorId = fornecedorId;
                    produtoExistente.CodigoFornecedor = codigoFornecedor;
                    produtoExistente.DescricaoCompletaFornecedor = descricaoCompletaFornecedor;
                    produtoExistente.Tamanho = tamanho;
                    produtoExistente.Cor = cor;
                    produtoExistente.CodigoNCM = codigoNcm;
                    produtoExistente.TipoEmbalagemInterna = tipoEmbalagemInterna;
                    produtoExistente.QuantidadePorEmbalagemInterna = quantidadePorEmbalagemInterna;
                    produtoExistente.TipoCaixaMaster = tipoCaixaMaster;
                    produtoExistente.QuantidadePorCaixaMaster = quantidadePorCaixaMaster;
                    produtoExistente.PesoBruto = pesoBruto;
                    produtoExistente.AplicacoesPrincipais = aplicacoesPrincipais;
                    produtoExistente.Composicao = composicao;
                    produtoExistente.DetalhesEmbalagem = detalhesEmbalagem;
                    produtoExistente.CustoDesenvolvimentoEmbalagem = custoDesenvolvimentoEmbalagem;
                    produtoExistente.CustoRotulagemEmbalagem = custoRotulagemEmbalagem;
                    produtoExistente.QuantidadeMinimaPedido = quantidadeMinimaPedido;
                    produtoExistente.UnidadeMedidaFornecedorMOQ = unidadeMedidaFornecedorMOQ;
                    produtoExistente.Preco = preco;
                    produtoExistente.UnidadeMedidaFornecedorPreco = unidadeMedidaFornecedorPreco;
                    produtoExistente.PortoEmbarque = portoEmbarque;
                    produtoExistente.TempoEntrega = tempoEntrega;
                    produtoExistente.IncotermId = incoterm!.Id;
                    produtoExistente.CapacidadeMensalFabrica = capacidadeMensalFabrica;
                    produtoExistente.UnidadeMedidaCapacidadeMensal = unidadeMedidaCapacidadeMensal;
                    produtoExistente.CustoDetalhadoMateriaPrima = materiaPrimaPercentual;
                    produtoExistente.CustoDetalhadoCombustivel = combustivelPercentual;
                    produtoExistente.CustoDetalhadoEmbalagem = embalagemPercentual;
                    produtoExistente.CustoDetalhadoMaoDeObra = maoDeObraPercentual;
                    produtoExistente.CustoDetalhadoEnergia = energiaPercentual;
                    produtoExistente.CustoDetalhadoTransporte = transportePercentual;
                    produtoExistente.NomeFabrica = nomeFabrica;
                    produtoExistente.TermoPagamento = termoPagamento;
                    produtoExistente.Observacoes = observacoes;
                    produtoExistente.Comprimento = comprimento;
                    produtoExistente.Largura = largura;
                    produtoExistente.Altura = altura;
                    produtoExistente.QuantidadeCarregamentoContainer20Ft = quantidadeCarregamentoContainer20Ft;
                    produtoExistente.QuantidadeCarregamentoContainer40Ft = quantidadeCarregamentoContainer40Ft;
                    produtoExistente.QuantidadeCarregamentoContainer40Hc = quantidadeCarregamentoContainer40Hc;
                    produtoExistente.Status = EStatusProduto.NaoHomologado;

                    repositoryProduto.Update(produtoExistente);
                }
                else
                {
                    var produto = new FornecedorProduto(
                        fornecedorId,
                        codigoFornecedor,
                        descricaoCompletaFornecedor,
                        aplicacoesPrincipais,
                        composicao,
                        tamanho,
                        cor,
                        codigoNcm,
                        unidadeMedidaFornecedorMOQ,
                        unidadeMedidaFornecedorPreco,
                        quantidadeMinimaPedido,
                        preco,
                        incoterm!.Id,
                        termoPagamento,
                        observacoes,
                        detalhesEmbalagem,
                        pesoBruto,
                        comprimento,
                        largura,
                        altura,
                        tempoEntrega,
                        custoDesenvolvimentoEmbalagem,
                        custoRotulagemEmbalagem,
                        portoEmbarque,
                        quantidadeCarregamentoContainer20Ft,
                        quantidadeCarregamentoContainer40Ft,
                        quantidadeCarregamentoContainer40Hc,
                        tipoEmbalagemInterna,
                        quantidadePorEmbalagemInterna,
                        tipoCaixaMaster,
                        quantidadePorCaixaMaster,
                        capacidadeMensalFabrica,
                        unidadeMedidaCapacidadeMensal,
                        nomeFabrica,
                        materiaPrimaPercentual,
                        combustivelPercentual,
                        embalagemPercentual,
                        maoDeObraPercentual,
                        energiaPercentual,
                        transportePercentual

                    );

                    var validationResult = _validator.Validate(produto);
                    if (!validationResult.IsValid)
                    {
                        produtosComErro.Add(new FornecedorProdutoErrosDto
                        {
                            DescricaoProduto = produto.DescricaoCompletaFornecedor,
                            Erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                        });
                        continue;
                    }

                    fornecedor.AdicionarProduto(produto);
                }
            }
        }

        if (IsInvalid())
            return await Task.FromResult(new CommandResponse<ProdutosAdicionarPlanilhaResponse>(this));

        repositoryFornecedor.Update(fornecedor);

        await mediator.Publish(new AuditoriaAdicionarInput(new Guid(), TabelasResources.FornecedorProduto, FornecedorResources.PlanilhaAdicionadoComSucesso, ETipoAuditoria.Modificado));

        if (produtosComErro.Count != 0 && produtosComErro.Count == totalProdutos)
        {
            AddNotification("Produto", FornecedorResources.TodosProdutosPlanilhaFalharam);
            return await Task.FromResult(new CommandResponse<ProdutosAdicionarPlanilhaResponse>(new ProdutosAdicionarPlanilhaResponse(fornecedor.Id, FornecedorResources.TodosProdutosPlanilhaFalharam, produtosComErro), this));
        }

        totalProdutos -= produtosComErro.Count;

        return new CommandResponse<ProdutosAdicionarPlanilhaResponse>(
            new ProdutosAdicionarPlanilhaResponse(fornecedor.Id, FornecedorResources.PlanilhaAdicionadoComSucesso, produtosComErro, totalProdutos, fornecedor.NomeFantasia, usuariosAutenticados),
            this
        );
    }
}
