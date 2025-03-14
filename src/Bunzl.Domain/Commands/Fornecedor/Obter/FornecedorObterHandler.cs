using Bunzl.Domain.DTOs;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.Obter;

public class FornecedorObterHandler(IRepositoryFornecedor repositoryFornecedor, IUsuarioAutenticado usuarioAutenticado) : Notifiable, IRequestHandler<FornecedorObterRequest, CommandResponse<FornecedorObterResponse>>
{
    public async Task<CommandResponse<FornecedorObterResponse>> Handle(FornecedorObterRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(
            false, 
            f => f.Id == request.Id, 
            cancellationToken, 
            f => f.FornecedorDadoBancario, 
            f => f.FornecedorDocumentos);

        if (fornecedor is not null)
        {
            var fornecedorDadoBancarioDto = fornecedor.FornecedorDadoBancario != null
                ? new FornecedorDadoBancarioDto(
                    fornecedor.FornecedorDadoBancario.NomeBeneficiario,
                    fornecedor.FornecedorDadoBancario.Logradouro,
                    fornecedor.FornecedorDadoBancario.Numero,
                    fornecedor.FornecedorDadoBancario.ZipCode,
                    fornecedor.FornecedorDadoBancario.Bairro,
                    fornecedor.FornecedorDadoBancario.Pais,
                    fornecedor.FornecedorDadoBancario.EstadoProvincia,
                    fornecedor.FornecedorDadoBancario.Cidade,
                    fornecedor.FornecedorDadoBancario.Swift,
                    fornecedor.FornecedorDadoBancario.Observacao,
                    fornecedor.FornecedorDadoBancario.NumeroBanco,
                    fornecedor.FornecedorDadoBancario.Agencia,
                    fornecedor.FornecedorDadoBancario.NumeroContaCorrente,
                    fornecedor.FornecedorDadoBancario.Iban,
                    fornecedor.FornecedorDadoBancario.VatNumber,
                    fornecedor.FornecedorDadoBancario.NomeBanco)
                : null;

            var fornecedorDocumentosDto = fornecedor.FornecedorDocumentos != null
                ? (await Task.WhenAll(fornecedor.FornecedorDocumentos.Select(async d =>
                {
                    try
                    {
                        var arquivoPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", request.Id.ToString()!, d.Nome);
                        var arquivoBytes = await File.ReadAllBytesAsync(arquivoPath, cancellationToken);
                        var arquivoBase64 = Convert.ToBase64String(arquivoBytes);
                        return new FornecedorDocumentoDto(d.Id, d.Nome, d.Tipo, d.Observacao, d.FornecedorId, d.DataCriacao, arquivoBase64);
                    }
                    catch (FileNotFoundException)
                    {
  
                        return new FornecedorDocumentoDto(d.Id, d.Nome, d.Tipo, d.Observacao, d.FornecedorId, d.DataCriacao);
                    }
                    catch (Exception ex)
                    {
                        return new FornecedorDocumentoDto(d.Id, d.Nome, d.Tipo, d.Observacao, d.FornecedorId, d.DataCriacao);
                    }
                }))).ToList()
                : new List<FornecedorDocumentoDto>();

            return new CommandResponse<FornecedorObterResponse>(
                new FornecedorObterResponse(
                    fornecedor.CodigoFornecedor,
                    fornecedor.RazaoSocial,
                    fornecedor.NomeFantasia,
                    fornecedor.Logradouro,
                    fornecedor.Numero,
                    fornecedor.ZipCode,
                    fornecedor.Bairro,
                    fornecedor.Pais,
                    fornecedor.EstadoProvincia,
                    fornecedor.Cidade,
                    fornecedor.Contato,
                    fornecedor.Website,
                    fornecedor.Email,
                    fornecedor.TelefoneArea,
                    fornecedor.Telefone,
                    fornecedor.WhatsAppWechatArea,
                    fornecedor.WhatsAppWechat,
                    fornecedor.FlagJaEhParceiroBunzl,
                    fornecedor.FlagFabricaAuditadaBunzl,
                    fornecedor.Status == Enumerators.EStatusFornecedor.Homologado,
                    fornecedor.FabricasAuditadas,
                    fornecedor.QuaisTiposProdutosFabricam,
                    fornecedor.QuaisProdutosTercerizam,
                    fornecedor.QuaisProdutosMaisVendidos,
                    fornecedor.FazemOEM,
                    fornecedor.InformacoesGerais,
                    fornecedor.PossuemLaboratorioProprio,
                    fornecedor.InformacoesMercados,
                    fornecedor.InformacoesPoliticas,
                    fornecedor.NumeroIdentificacaoFiscal,
					fornecedor.CodigoERP,
                    fornecedor.DataFabricaAuditada,
                    fornecedor.DataAlteracao,
                    fornecedor.Status,
                    fornecedor.MoedaId,
                    fornecedor.QuantidadeAnosEmpresaMercado,
                    fornecedor.ReceitaAnualEmpresa,
                    fornecedor.QuantidadeTrabalhadoresEmpresaPossui,
                    fornecedor.QuantidadeUnidadesFabricacaoOndeEstaoLocalizadas,
                    fornecedor.CapacidadeProducaoFabricas,
                    fornecedor.QuantidadeTurnosTrabalhoRealizados,
                    fornecedor.EmpresaPossuiLaboratoriosPropriosParaPesquisaDesenvolvimento,
                    fornecedor.QuaisMercadosEmpresaOpera,
                    fornecedor.PorcentagemVendasMercadosRepresentam,
                    fornecedor.PrincipaisClientesSegmentosAtendidosEmpresa,
                    fornecedor.EmpresaPossuiClientesNoBrasilQuemSao,
                    fornecedor.EmpresaOfereceExclusividadeProdutosRegioes,
                    fornecedor.QuaisTermosPagamentosOferecidos,
                    fornecedor.EmpresaPossuiCertificacaoFabricas,
                    fornecedor.FlagBloqueadoErp,
                    fornecedor.CodigoTabelaPreco,
                    fornecedor.SiglaFornecedor,
                    fornecedorDadoBancarioDto,
                    fornecedorDocumentosDto),
                this);
        }

        AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);

        return new CommandResponse<FornecedorObterResponse>(this);
    }
}

