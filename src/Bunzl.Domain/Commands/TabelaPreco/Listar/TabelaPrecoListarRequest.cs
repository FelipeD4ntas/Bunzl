﻿using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using DevExtreme.AspNet.Data;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.Listar;

public class TabelaPrecoListarRequest : DataSourceLoadOptionsBase, IRequest<CommandResponse<DataSourcePageResponse>>
{
    public Guid FornecedorId { get; set; }
}