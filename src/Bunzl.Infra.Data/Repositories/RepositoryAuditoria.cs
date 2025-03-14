﻿using Bunzl.Domain.Entities;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.Data.Context;
using Bunzl.Infra.Data.Repositories.Base;

namespace Bunzl.Infra.Data.Repositories;

public class RepositoryAuditoria(BunzlContext context) : RepositoryBase<Auditoria, BunzlContext, Guid>(context), IRepositoryAuditoria, IInjectScoped;