using Bunzl.Core.Domain.DTOs.Gateway;
using Bunzl.Core.Domain.Interfaces.ExternalService;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Interfaces.Services;
using Bunzl.Infra.CrossCutting.Helper;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Integracao.ObterFornecedores;

public class ObterFornecedoresHandler(
    IExternalServiceFornecedor externalServiceFornecedor,
    IRepositoryFornecedor repositoryFornecedor,
    IRepositoryUsuario repositoryUsuario,
    IRepositoryEmpresa repositoryEmpresa,
    IRepositoryMoeda repositoryMoeda,
    IUsuarioFornecedorService usuarioFornecedorService
    )
    : Notifiable, IRequestHandler<ObterFornecedoresRequest, CommandResponse<ObterFornecedoresResponse>>
{
    public async Task<CommandResponse<ObterFornecedoresResponse>> Handle(ObterFornecedoresRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<GatewayFornecedoresDto> fornecedores = [];

        var empresas = await repositoryEmpresa.ListAsync(true, 0, 100, e => request.EmpresaCnpj == e.Cnpj);
        if (empresas == null || !empresas.Any())
        {
            AddNotification("Empresas", UsuarioResources.UsuarioFalhaRelacionarEmpresas);
            return new CommandResponse<ObterFornecedoresResponse>(this);
        }

        if (!string.IsNullOrEmpty(request.CodigoFornecedor))
        {
            fornecedores = (await SafeExecutionHelper.SafeExecuteAsync(
                () => externalServiceFornecedor.ObterFornecedoresPorCodigo(request.EmpresaCnpj,
                    request.CodigoFornecedor, cancellationToken),
                AddNotification,
                "Fornecedor",
                IntegracaoResources.CnpjEmpresaNaoEncontrouFornecedores))!;
        }
        else if (request.DataAlteracaoInicio is not null && request.DataAlteracaoFim is not null)
        {
            fornecedores = (await SafeExecutionHelper.SafeExecuteAsync(
                () => externalServiceFornecedor.ObterFornecedorePorDataInicioDataFim(request.EmpresaCnpj, request.DataAlteracaoInicio, request.DataAlteracaoFim, cancellationToken),
                AddNotification,
                "Fornecedor",
                IntegracaoResources.CnpjEmpresaNaoEncontrouFornecedores))!;
        }
        else
        {
            fornecedores = (await SafeExecutionHelper.SafeExecuteAsync(
                () => externalServiceFornecedor.ObterTodosFornecedores(request.EmpresaCnpj, cancellationToken),
                AddNotification,
                "Fornecedor",
                IntegracaoResources.CnpjEmpresaNaoEncontrouFornecedores))!;

            var empresaAutenticada = empresas.FirstOrDefault();

            if (empresaAutenticada is not null && fornecedores is not null)
            {
                empresaAutenticada.DataUltimaAtualizacao = DateTime.UtcNow;

                repositoryEmpresa.Update(empresaAutenticada);
            }
        }

        if (fornecedores == null || !fornecedores.Any())
            return new CommandResponse<ObterFornecedoresResponse>(this);

        var moeda = await repositoryMoeda.ListAsync(true, 1, 1);

        foreach (var fornecedorExterno in fornecedores)
        {
            if (string.IsNullOrEmpty(fornecedorExterno.CodigoFornecedor) && string.IsNullOrEmpty(fornecedorExterno.CodigoERP))
            {
                AddNotification("Fornecedor", IntegracaoResources.FornecedorNaoPossuiCodigoFornecedorECodigoERP);
                return new CommandResponse<ObterFornecedoresResponse>(this);
            }

            var codigoBaseFornecedor = string.IsNullOrEmpty(fornecedorExterno.CodigoFornecedor) ? fornecedorExterno.CodigoERP : fornecedorExterno.CodigoFornecedor;

            //Localizando o Fornecedor
            var fornecedorExistente = await repositoryFornecedor.GetByAsync(true, f => f.CodigoFornecedor == codigoBaseFornecedor || f.CodigoERP == codigoBaseFornecedor, true, cancellationToken, f => f.Usuarios);
            if (fornecedorExistente is null)
            {
                //Criando Usuário
                var usuario = await usuarioFornecedorService.AdicionarUsuarioAsync(
                    //fornecedorExterno.Email ?? "", //Eduardo
                    "", //Eduardo
                    fornecedorExterno.NomeFantasia ?? fornecedorExterno.NomeCompleto ?? "",
                    EPerfilUsuario.FornecedorEndUser,
                    empresas.ToList(),
                    true,
                    cancellationToken);

                //Criando Fornecedor
                var fornecedor = new Entities.Fornecedor
                {
                    //Email = fornecedorExterno.Email ?? "",
                    Email = "",
                    CodigoFornecedor = fornecedorExterno.CodigoFornecedor,
                    CodigoERP = fornecedorExterno.CodigoERP,
                    Pais = fornecedorExterno.Pais ?? "",
                    RazaoSocial = fornecedorExterno.NomeCompleto ?? "",
                    NumeroIdentificacaoFiscal = fornecedorExterno.CodigoNF,
                    NomeFantasia = fornecedorExterno.NomeFantasia ?? "",
                    Logradouro = fornecedorExterno.Endereco ?? "",
                    Numero = fornecedorExterno.Numero ?? "",
                    Bairro = fornecedorExterno.Bairro ?? "",
                    EstadoProvincia = fornecedorExterno.Estado ?? "",
                    ZipCode = fornecedorExterno.Cep ?? "",
                    Cidade = fornecedorExterno.Cidade ?? "",
                    Website = fornecedorExterno.WebSite ?? "",
                    Contato = fornecedorExterno.NomeContato ?? "",
                    TelefoneArea = fornecedorExterno.TelefoneDDI ?? "",
                    Telefone = fornecedorExterno.Telefone ?? "",
                    WhatsAppWechatArea = fornecedorExterno.TelefoneDDI ?? "",
                    WhatsAppWechat = fornecedorExterno.WhatsApp ?? "",
                    SiglaFornecedor = fornecedorExterno.Sigla ?? "",
                    FlagBloqueadoErp = fornecedorExterno.IsBloqueado,
                    Status = EStatusFornecedor.AguardandoAprovacao,
                    FlagVeioDoERP = true,
                    MoedaId = moeda.First().Id
                };

                await usuarioFornecedorService.AdicionarFornecedorAsync(usuario, fornecedor, cancellationToken);
                fornecedor.RelacionarEmpresa(empresas.First());
            }
            else
            {
                var usuarioFornecedorExistente = fornecedorExistente.Usuarios.First();
                var usuarioExistente = await repositoryUsuario.GetByAsync(true, u => u.Id == usuarioFornecedorExistente.Id, true, cancellationToken, u => u.Empresas, u => u.Fornecedores);

                if (usuarioExistente is not null)
                {
                    var empresaAtual = usuarioExistente.Empresas.FirstOrDefault(e => e.Cnpj == request.EmpresaCnpj);
                    if (empresaAtual is null)
                    {
                        usuarioExistente.RelacionarEmpresas(empresas.ToList());
                        usuarioExistente.Atualizar(usuarioExistente.Nome, usuarioExistente.Email, usuarioExistente.Telefone);
                    }
                }
                else
                {
                    //Todo: Alinhar com o Marcos a trativa desse else
                }

                //Atualizando Fornecedor
                fornecedorExistente.AtualizarComDadosDoErp(
                    fornecedorExterno.CodigoFornecedor,
                    fornecedorExterno.CodigoERP,
                    fornecedorExterno.Pais,
                    fornecedorExterno.NomeCompleto,
                    fornecedorExterno.CodigoNF,
                    fornecedorExterno.NomeFantasia,
                    fornecedorExterno.Endereco,
                    fornecedorExterno.Numero,
                    fornecedorExterno.Bairro,
                    fornecedorExterno.Estado,
                    fornecedorExterno.Cep,
                    fornecedorExterno.Cidade,
                    fornecedorExterno.WebSite,
                    fornecedorExterno.NomeContato,
                    fornecedorExterno.TelefoneDDI,
                    fornecedorExterno.Telefone,
                    fornecedorExterno.TelefoneDDI,
                    fornecedorExterno.WhatsApp,
                    fornecedorExterno.Sigla,
                    fornecedorExterno.IsBloqueado);

                repositoryFornecedor.Update(fornecedorExistente);
                //fornecedorExistente.RelacionarEmpresa(empresas.First()); //??????
            }
        }


        //    foreach (var fornecedorExterno in fornecedores)
        //    {
        //        var codigoFornecedor = fornecedorExterno.CodigoFornecedor;
        //        var nome = fornecedorExterno.NomeFantasia ?? fornecedorExterno.NomeCompleto;
        //        var fornecedorExistente = await repositoryFornecedor.GetByAsync(true, f => f.CodigoFornecedor == codigoFornecedor, true, cancellationToken, f => f.Usuarios);
        //        var usuarioFornecedorExistente = fornecedorExistente?.Usuarios.FirstOrDefault();

        //        if (usuarioFornecedorExistente is not null)
        //        {
        //            var usuarioExistente = await repositoryUsuario.GetByAsync(true, u => u.Id == usuarioFornecedorExistente.Id, true, cancellationToken, u => u.Empresas, u => u.Fornecedores);
        //            if (usuarioExistente is not null)
        //            {
        //                var empresaAtual = usuarioExistente.Empresas.FirstOrDefault(e => e.Cnpj == request.EmpresaCnpj);
        //                if (empresaAtual == null)
        //                {
        //                    usuarioExistente.RelacionarEmpresas(empresas.ToList());
        //                    usuarioExistente.Atualizar(usuarioExistente.Nome, usuarioExistente.Email, usuarioExistente.Telefone);

        //                    fornecedorExistente.AtualizarComDadosDoErp(
        //                        fornecedorExterno.CodigoPessoa,
        //                        fornecedorExterno.NomeFantasia,
        //                        fornecedorExterno.NomeCompleto,
        //                        fornecedorExterno.Endereco,
        //                        fornecedorExterno.Cidade,
        //                        fornecedorExterno.Estado,
        //                        fornecedorExterno.Pais,
        //                        fornecedorExterno.Telefone,
        //                        fornecedorExterno.CodigoERP,
        //                        fornecedorExterno.CodigoFornecedor,
        //                        fornecedorExterno.IsBloqueado);

        //                    repositoryFornecedor.Update(fornecedorExistente);
        //                    fornecedorExistente.RelacionarEmpresa(empresas.First());
        //                }
        //                else
        //                {
        //                    continue;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            var usuario = await usuarioFornecedorService.AdicionarUsuarioAsync(
        //                fornecedorExterno.Email ?? "",
        //                nome ?? "",
        //                EPerfilUsuario.FornecedorEndUser,
        //                empresas.ToList(),
        //                true,
        //                cancellationToken);

        //            var fornecedor = new Entities.Fornecedor
        //            {
        //                Email = fornecedorExterno.Email ?? "",
        //                NumeroIdentificacaoFiscal = fornecedorExterno.CodigoPessoa,
        //                NomeFantasia = fornecedorExterno.NomeFantasia ?? "",
        //                RazaoSocial = fornecedorExterno.NomeCompleto ?? "",
        //                Logradouro = fornecedorExterno.Endereco ?? "",
        //                Cidade = fornecedorExterno.Cidade ?? "",
        //                EstadoProvincia = fornecedorExterno.Estado ?? "",
        //                Pais = fornecedorExterno.Pais ?? "",
        //                Telefone = fornecedorExterno.Telefone,
        //                Status = EStatusFornecedor.AguardandoAprovacao,
        //                CodigoERP = fornecedorExterno.CodigoERP,
        //                CodigoFornecedor = fornecedorExterno.CodigoFornecedor,
        //	FlagVeioDoERP = true,
        //	FlagBloqueadoErp = fornecedorExterno.IsBloqueado,
        //                MoedaId = moeda.First().Id
        //};

        //            await usuarioFornecedorService.AdicionarFornecedorAsync(usuario, fornecedor, cancellationToken);
        //            fornecedor.RelacionarEmpresa(empresas.First());
        //        }
        //    };

        var response = new ObterFornecedoresResponse(Guid.NewGuid(), IntegracaoResources.ImportacaoRealizadaComSucesso);

        return new CommandResponse<ObterFornecedoresResponse>(response, this);
    }

}
