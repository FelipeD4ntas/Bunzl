using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using Mapster;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.Atualizar;

public class FornecedorAtualizarHandler(
    IPublisher mediator,
    IRepositoryFornecedor repositoryFornecedor,
    IRepositoryUsuario repositoryUsuario,
    IRepositoryEmpresa repositoryEmpresa,
    IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<FornecedorAtualizarRequest, CommandResponse<FornecedorAtualizarResponse>>
{
    public async Task<CommandResponse<FornecedorAtualizarResponse>> Handle(FornecedorAtualizarRequest request,
        CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(true, c => c.Id == request.Id, true, cancellationToken, p => p.Usuarios, p => p.FornecedorDadoBancario, p => p.Empresas);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<FornecedorAtualizarResponse>(this);
        }

        var usuariosFornecedor = fornecedor.Usuarios.ToList();
        var empresasFornecedor = fornecedor.Empresas.ToList();
        var statusAtual = fornecedor.Status;
        fornecedor.Atualizar(request.Adapt<Entities.Fornecedor>());
        fornecedor.Usuarios = usuariosFornecedor;
        fornecedor.Empresas = empresasFornecedor;

        var empresa = await repositoryEmpresa.GetByAsync(true, c => c.Id == usuarioAutenticado.UsuarioEmpresa, true, cancellationToken, p => p.Usuarios);

        if (empresa == null)
        {
            AddNotification("Fornecedor", EmpresaResources.NenhumaEmpresaVinculadaFornecedor);
            return new CommandResponse<FornecedorAtualizarResponse>(this);
        }

        var empresaJaRelacionada = fornecedor.Empresas.Any(e => e.Id == empresa.Id);

        if (fornecedor.FornecedorDadoBancario == null)
        {
            var fornecedorDadoBancario = request.FornecedorDadoBancario.Adapt<Entities.FornecedorDadoBancario>();
            fornecedor.AdicionarDadoBancario(fornecedorDadoBancario);
        }
        else
        {
            request.FornecedorDadoBancario.Adapt(fornecedor.FornecedorDadoBancario);
            fornecedor.AtualizarDadoBancario(fornecedor.FornecedorDadoBancario);
        }

        var usuarioFornecedor = usuariosFornecedor.FirstOrDefault();

        if (usuarioFornecedor is not null && usuarioFornecedor.FlagVeioDoERP)
        {
            usuariosFornecedor.First().Email = fornecedor.Email;
            repositoryUsuario.Update(usuariosFornecedor.First());
        }

        if (usuarioAutenticado.Permissoes == EPerfilUsuario.FornecedorEndUser.ToString() && statusAtual == EStatusFornecedor.Homologado)
        {
            var usuariosEmpresaEmail = empresa.Usuarios.Where(u => u.PerfilPermissao != Enumerators.EPerfilUsuario.BunzlCorporativoMasterUser && u.PerfilPermissao != EPerfilUsuario.FornecedorEndUser).ToList();
            fornecedor.Status = EStatusFornecedor.NaoHomologado;

            if (!empresaJaRelacionada)
            {
                fornecedor.RelacionarEmpresa(empresa);
            }

            return await AtualizarFornecedorAsync(fornecedor, request, new FornecedorAtualizarResponse(fornecedor.Id, FornecedorResources.FornecedorAtualizadoComSucesso, true, usuariosEmpresaEmail, fornecedor), cancellationToken);
        }
        else if (statusAtual != EStatusFornecedor.Homologado && request.Status == EStatusFornecedor.Homologado) 
        {
            if (usuarioFornecedor is not null && usuarioFornecedor.Senha is null)
            {
                if (!empresaJaRelacionada)
                {
                    fornecedor.RelacionarEmpresa(empresa);
                }
                return await AtualizarFornecedorAsync(fornecedor, request, new FornecedorAtualizarResponse(fornecedor.Id, FornecedorResources.FornecedorAtualizadoComSucesso, usuarioFornecedor, fornecedor), cancellationToken);
            }

            fornecedor.Status = EStatusFornecedor.Homologado;

            if (!empresaJaRelacionada)
            {
                fornecedor.RelacionarEmpresa(empresa);
            }

            fornecedor.Atualizar(fornecedor);
            return await AtualizarFornecedorAsync(fornecedor, request, new FornecedorAtualizarResponse(fornecedor.Id, FornecedorResources.FornecedorAtualizadoComSucesso, fornecedor, false), cancellationToken);
        } 
        else if (request.Status == EStatusFornecedor.NaoHomologado && usuarioFornecedor is not null && usuarioFornecedor.Senha is null)
        {
            if (!empresaJaRelacionada)
            {
                fornecedor.RelacionarEmpresa(empresa);
            }
            return await AtualizarFornecedorAsync(fornecedor, request, new FornecedorAtualizarResponse(fornecedor.Id, FornecedorResources.FornecedorAtualizadoComSucesso, usuarioFornecedor, fornecedor), cancellationToken);
        }   

        if (!empresaJaRelacionada)
        {
            fornecedor.RelacionarEmpresa(empresa);
        }

        return await AtualizarFornecedorAsync(fornecedor, request, new FornecedorAtualizarResponse(fornecedor.Id, FornecedorResources.FornecedorAtualizadoComSucesso, fornecedor, true), cancellationToken);
    }

    private async Task<CommandResponse<FornecedorAtualizarResponse>> AtualizarFornecedorAsync(Entities.Fornecedor fornecedor, FornecedorAtualizarRequest request, FornecedorAtualizarResponse response, CancellationToken cancellationToken)
    {
        repositoryFornecedor.Update(fornecedor);
        await mediator.Publish(new AuditoriaAdicionarInput(fornecedor.Id, TabelasResources.Fornecedor, "Atualizado", ETipoAuditoria.Modificado), cancellationToken);

        return new CommandResponse<FornecedorAtualizarResponse>(response, this);
    }
}