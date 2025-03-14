using Bunzl.Application.Interfaces;
using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Core.Domain.Interfaces.Email;
using Bunzl.Core.Domain.Interfaces.UoW;
using Bunzl.Domain.Commands.Integracao.ObterFornecedores;
using Bunzl.Domain.Commands.Integracao.ObterProdutos;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.Data.Context;
using MediatR;

namespace Bunzl.Application.Services;

public class IntegracaoAppService(IUnitOfWork unitOfWork, ISender mediator, IEmailService emailService, BunzlContext bunzlContext, IFornecedorAppService fornecedorAppService) : Notifiable, IIntegracaoAppService, IInjectScoped
{
    public async Task<CommandResponse<ObterFornecedoresResponse>> ObterFornecedores(ObterFornecedoresRequest request)
    {
        try
        {
            var commandResponse = await mediator.Send(request);

            AddNotifications(commandResponse.Notificacoes);

            if (IsValid())
                await unitOfWork.CommitAsync();

            if (commandResponse.Dados?.Id is not null && !string.IsNullOrEmpty(commandResponse.Dados.Mensagem))
            {
                var response = new ObterFornecedoresResponse(commandResponse.Dados.Id, commandResponse.Dados.Mensagem);

                return new CommandResponse<ObterFornecedoresResponse>(response, this);
            }

            return new CommandResponse<ObterFornecedoresResponse>(this);
        }
        catch (Exception ex)
        {
            AddNotification("ObterFornecedores", ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<CommandResponse<DataSourcePageResponse>> ObterProdutos(ObterProdutosRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }
}
