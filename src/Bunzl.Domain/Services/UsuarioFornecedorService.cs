using Bunzl.Core.Domain.Interfaces.UoW;
using Bunzl.Domain.Entities;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Interfaces.Services;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;

namespace Bunzl.Domain.Services;

public class UsuarioFornecedorService : Notifiable, IUsuarioFornecedorService, IInjectTransient
{
    private readonly IRepositoryUsuario _repositoryUsuario;
    private readonly IRepositoryFornecedor _repositoryFornecedor;
    private readonly IUnitOfWork _unitOfWork;
  
    public UsuarioFornecedorService(IUnitOfWork unitOfWork, IRepositoryUsuario repositoryUsuario, IRepositoryFornecedor repositoryFornecedor)
    {
        _repositoryUsuario = repositoryUsuario;
        _repositoryFornecedor = repositoryFornecedor;
        _unitOfWork = unitOfWork;
    }

    public async Task<Usuario> AdicionarUsuarioAsync(string email, string nome, EPerfilUsuario perfilPermissao, List<Empresa> empresas, bool flagVeioERP, CancellationToken cancellationToken)
    {
        try
        {
            var usuario = new Entities.Usuario
            {
                Email = email,
                Nome = nome,
                PerfilPermissao = perfilPermissao,
                FlagVeioDoERP = flagVeioERP
            };

            usuario.RelacionarEmpresas(empresas);
            usuario.GerarChaveCadastro();

            if (usuario.ChaveCadastro == null)
            {
                throw new Exception(UsuarioResources.FalhaGerarChaveCadastro);
            }

            await _repositoryUsuario.AddAsync(usuario, cancellationToken);
            await _unitOfWork.CommitAsync();
            return usuario;
        }
        catch(Exception ex)
        {
            AddNotification("AdicionarUsuarioAsync", ex.Message);
            throw new Exception(ex.Message);
        }
        finally
        {
            if (IsValid())
            {
                await _unitOfWork.CommitAsync();
            }
        }
    }

    public async Task<Fornecedor> AdicionarFornecedorAsync(Usuario usuario, Fornecedor fornecedor, CancellationToken cancellationToken)
    {
        try
        {
            fornecedor.Status = Enumerators.EStatusFornecedor.AguardandoAprovacao;
            fornecedor.RelacionarUsuario(usuario);

            await _repositoryFornecedor.AddAsync(fornecedor, cancellationToken);
            await _unitOfWork.CommitAsync();
            return fornecedor;
        }
        catch(Exception ex)
        {
            AddNotification("AdicionarFornecedorAsync", ex.Message);
            throw new Exception(ex.Message);
        }
    }
}
