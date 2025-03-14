using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using Mapster;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.Adicionar;

public class FornecedorAdicionarHandler(
    IPublisher mediator,
    IRepositoryFornecedor repositoryFornecedor,
    IRepositoryUsuario repositoryUsuario,
    IRepositoryEmpresa repositoryEmpresa,
    IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<FornecedorAdicionarRequest, CommandResponse<FornecedorAdicionarResponse>>
{
    public async Task<CommandResponse<FornecedorAdicionarResponse>> Handle(FornecedorAdicionarRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = request.Adapt<Entities.Fornecedor>();
        fornecedor.Status = Enumerators.EStatusFornecedor.AguardandoAprovacao;

        var usuario = await repositoryUsuario.GetByAsync(true, u => u.Id == usuarioAutenticado.UsuarioId, false, cancellationToken, u => u.Empresas);
        if (usuario == null)
        {
            AddNotification("Usuarios", FornecedorResources.FornecedorFalhaRelacionarUsuarios);
            return new CommandResponse<FornecedorAdicionarResponse>(this);
        }

        var empresa = await repositoryEmpresa.GetByAsync(true, u => u.Id == usuarioAutenticado.UsuarioEmpresa, false, cancellationToken, e => e.Usuarios);

        if (empresa == null)
        {
            AddNotification("Empresa", FornecedorResources.FornecedorFalhaRelacionarEmpresa);
            return new CommandResponse<FornecedorAdicionarResponse>(this);
        }

        var usuariosEmpresaEmail = empresa.Usuarios.Where(u => u.PerfilPermissao != Enumerators.EPerfilUsuario.BunzlCorporativoMasterUser && u.PerfilPermissao != EPerfilUsuario.FornecedorEndUser).ToList();

        var empresaJaRelacionada = fornecedor.Empresas.Any(e => e.Id == empresa.Id);
        var usuarioJaRelacionado = fornecedor.Usuarios.Any(u => u.Id == usuario.Id);

        var emailExists = await repositoryFornecedor.ExistsAsync(p => p.Email == request.Email, true, cancellationToken);

        if (emailExists)
        {
            var fornecedorExistente = await repositoryFornecedor.GetByAsync(true, f => f.Email == request.Email, true, cancellationToken, f => f.Empresas);

            if (fornecedorExistente is not null && fornecedorExistente.Empresas.Count > 0 && request.CodigoERP is not null)
            {
                if (empresaJaRelacionada)
                {
                    return new CommandResponse<FornecedorAdicionarResponse>(
                        new FornecedorAdicionarResponse(fornecedorExistente.Id, FornecedorResources.FornecedorExistenteJaRelacionadoComEssaEmpresa),
                        this);
                }
                else
                {
                    fornecedorExistente.RelacionarEmpresa(empresa);

                    return new CommandResponse<FornecedorAdicionarResponse>(
                        new FornecedorAdicionarResponse(fornecedorExistente.Id, FornecedorResources.FornecedorExistenteRelacionadoComNovaEmpresa),
                        this);
                }
            }
        }

        if (!usuarioJaRelacionado)
        {
            fornecedor.RelacionarUsuario(usuario);
        }

        if (!empresaJaRelacionada)
        {
            fornecedor.RelacionarEmpresa(empresa);
        }

        if (IsInvalid())
            return await Task.FromResult(new CommandResponse<FornecedorAdicionarResponse>(this));

        await repositoryFornecedor.AddAsync(fornecedor, cancellationToken);
        await mediator.Publish(new AuditoriaAdicionarInput(fornecedor.Id, TabelasResources.Fornecedor, "Criado", Enumerators.ETipoAuditoria.Adicionado), cancellationToken);

        if (usuariosEmpresaEmail.Count > 0)
        {
            return new CommandResponse<FornecedorAdicionarResponse>(
                new FornecedorAdicionarResponse(fornecedor.Id, FornecedorResources.FornecedorAdicionadoComSucesso, usuariosEmpresaEmail, fornecedor.NomeFantasia),
                this);
        }

        return new CommandResponse<FornecedorAdicionarResponse>(
            new FornecedorAdicionarResponse(fornecedor.Id, FornecedorResources.FornecedorAdicionadoComSucesso),
            this);
    }
}