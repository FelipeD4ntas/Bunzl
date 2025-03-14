using Bunzl.Domain.Entities;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.Extensions;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.AdicionarAnexo;

public class OrdemDeCompraAdicionarAnexoHandler(
	IPublisher mediator, 
	IRepositoryOrdemDeCompra repositoryOrdemDeCompra,
	IRepositoryEmpresa repositoryEmpresa,
	IRepositoryFornecedor repositoryFornecedor,
	IRepositoryUsuario repositoryUsuario,
	IUsuarioAutenticado usuarioAutenticado)
	: Notifiable, IRequestHandler<OrdemDeCompraAdicionarAnexoRequest, CommandResponse<OrdemDeCompraAdicionarAnexoResponse>>
{
    public async Task<CommandResponse<OrdemDeCompraAdicionarAnexoResponse>> Handle(OrdemDeCompraAdicionarAnexoRequest request, CancellationToken cancellationToken)
    {
        var tamanhoMaximoMB = 25 * 1024 * 1024;
        if (request.Arquivo.Length > tamanhoMaximoMB)
        {
            AddNotification("Arquivo", FornecedorResources.DocumentoTamanhoMaximo);
            return new CommandResponse<OrdemDeCompraAdicionarAnexoResponse>(this);
        }

        var ordemDeCompra = await repositoryOrdemDeCompra.GetByAsync(true, f => f.Id == request.OrdemDeCompraId, cancellationToken, p => p.Anexos);
        if (ordemDeCompra == null)
        {
            AddNotification("Fornecedor", OrdemDeCompraResources.OrdemDeCompraNaoEncontrada);
            return new CommandResponse<OrdemDeCompraAdicionarAnexoResponse>(this);
        }

		var diretorioArquivo = Path.Combine(Directory.GetCurrentDirectory(), "uploads", ordemDeCompra.Id.ToString());
        if (!Directory.Exists(diretorioArquivo))
            Directory.CreateDirectory(diretorioArquivo);

        var caminhoArquivo = Path.Combine(diretorioArquivo, request.Arquivo.FileName);
        if (Path.Exists(caminhoArquivo))
        {
            AddNotification("Arquivo", FornecedorResources.ArquivoJaExiste);
            return new CommandResponse<OrdemDeCompraAdicionarAnexoResponse>(this);
        }

        await using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
        {
            await request.Arquivo.CopyToAsync(stream, cancellationToken);
        }

        var documento = new Entities.OrdemDeCompraAnexo
		(
            request.Arquivo.FileName,
            request.Arquivo.ContentType,
            ordemDeCompra.Id
        );

        ordemDeCompra.AdicionarAnexo(documento);

        var fornecedor = await repositoryFornecedor.GetByAsync(false, f => f.Id == ordemDeCompra.FornecedorId, cancellationToken, p => p.Usuarios);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<OrdemDeCompraAdicionarAnexoResponse>(this);
        }

        var usuario = await repositoryUsuario.GetByAsync(false, u => u.Id == usuarioAutenticado.UsuarioId, cancellationToken, u => u.Empresas);
        if (usuario == null)
        {
            AddNotification("Fornecedor", UsuarioResources.UsuarioNaoEncontrado);
            return new CommandResponse<OrdemDeCompraAdicionarAnexoResponse>(this);
        }

        var empresa = await repositoryEmpresa.GetByAsync(false, u => u.Id == ordemDeCompra.EmpresaId, false, cancellationToken, e => e.Usuarios);
        if (empresa == null)
        {
            AddNotification("Empresa", EmpresaResources.EmpresaNaoEncontrada);
            return new CommandResponse<OrdemDeCompraAdicionarAnexoResponse>(this);
        }

        var usuariosEmpresaEmail = empresa.Usuarios.Where(u => u.PerfilPermissao is EPerfilUsuario.AdministradorSuperUser or EPerfilUsuario.CompradorKeyUser).ToList();

        await mediator.Publish(new AuditoriaAdicionarInput(documento.Id, TabelasResources.OrdemCompra, "Anexo Adicionado", ETipoAuditoria.Modificado));

        var ehPerfilFornecedor = usuarioAutenticado.Profile == "FornecedorEndUser";

        if (usuarioAutenticado.Permissoes == EPerfilUsuario.FornecedorEndUser.ToString() && ordemDeCompra.Status == EStatusOrdemDeCompra.AguardandoPi)
        {
            ordemDeCompra.Status = EStatusOrdemDeCompra.AguardandoAssinatura;
            repositoryOrdemDeCompra.Update(ordemDeCompra);
            return new CommandResponse<OrdemDeCompraAdicionarAnexoResponse>(
                new OrdemDeCompraAdicionarAnexoResponse(ordemDeCompra.Id, OrdemDeCompraResources.AnexoAdicionadoComSucesso, documento, fornecedor, ordemDeCompra.NumeroOrdem, usuarioAutenticado.UsuarioNome, empresa, usuariosEmpresaEmail, ordemDeCompra.Status.GetDescriptionLanguage(usuarioAutenticado.Idioma), ehPerfilFornecedor), this);
        }
        else if (usuarioAutenticado.Permissoes == EPerfilUsuario.FornecedorEndUser.ToString() && ordemDeCompra.Status == EStatusOrdemDeCompra.AguardandoAssinatura)
        {
            repositoryOrdemDeCompra.Update(ordemDeCompra);
            return new CommandResponse<OrdemDeCompraAdicionarAnexoResponse>(
                new OrdemDeCompraAdicionarAnexoResponse(ordemDeCompra.Id, OrdemDeCompraResources.AnexoAdicionadoComSucesso, documento, fornecedor, ordemDeCompra.NumeroOrdem, usuarioAutenticado.UsuarioNome, empresa, usuariosEmpresaEmail, ordemDeCompra.Status.GetDescriptionLanguage(usuarioAutenticado.Idioma), ehPerfilFornecedor), this);
        }
        else
        {
	        ordemDeCompra.Status = EStatusOrdemDeCompra.AguardandoAssinatura;
	        repositoryOrdemDeCompra.Update(ordemDeCompra);
			return new CommandResponse<OrdemDeCompraAdicionarAnexoResponse>(
            new OrdemDeCompraAdicionarAnexoResponse(ordemDeCompra.Id, OrdemDeCompraResources.AnexoAdicionadoComSucesso, documento, fornecedor, ordemDeCompra.NumeroOrdem, usuarioAutenticado.UsuarioNome, empresa, usuariosEmpresaEmail, ordemDeCompra.Status.GetDescriptionLanguage(usuarioAutenticado.Idioma), ehPerfilFornecedor),
            this);
		}
    }
}
