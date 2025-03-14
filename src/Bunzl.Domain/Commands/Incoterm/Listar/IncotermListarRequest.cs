using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using DevExtreme.AspNet.Data;
using MediatR;

namespace Bunzl.Domain.Commands.Incoterm.Listar;

public class IncotermListarRequest : DataSourceLoadOptionsBase, IRequest<CommandResponse<DataSourcePageResponse>>;