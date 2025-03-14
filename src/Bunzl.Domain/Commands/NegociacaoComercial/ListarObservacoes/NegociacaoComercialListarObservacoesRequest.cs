using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using DevExtreme.AspNet.Data;
using MediatR;

namespace Bunzl.Domain.Commands.NegociacaoComercial.ListarObservacoes;

public class NegociacaoComercialListarObservacoesRequest : DataSourceLoadOptionsBase, IRequest<CommandResponse<DataSourcePageResponse>>
{
    public Guid NegociacaoComercialId { get; set; }
}