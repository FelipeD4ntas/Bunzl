using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.Empresa.ObterDataUltimaAtualizacao;

public class ObterDataUltimaAtualizacaoHandler(IRepositoryEmpresa repositoryEmpresa, IUsuarioAutenticado usuarioAutenticado) : Notifiable, IRequestHandler<ObterDataUltimaAtualizacaoRequest, CommandResponse<ObterDataUltimaAtualizacaoResponse>>
{
    public async Task<CommandResponse<ObterDataUltimaAtualizacaoResponse>> Handle(ObterDataUltimaAtualizacaoRequest request, CancellationToken cancellationToken)
    {
        var empresa = await repositoryEmpresa.GetByAsync(false, e => e.Id == request.Id, cancellationToken: cancellationToken);

        if (empresa == null)
        {
            AddNotification("Empresa", EmpresaResources.EmpresaNaoEncontrada);
            return new CommandResponse<ObterDataUltimaAtualizacaoResponse>(this);
        }

        return new CommandResponse<ObterDataUltimaAtualizacaoResponse>(new ObterDataUltimaAtualizacaoResponse(empresa.Id, empresa.Nome, empresa.DataUltimaAtualizacao, empresa.DataUltimaAtualizacaoOrdemDeCompra), this);
    }
}
