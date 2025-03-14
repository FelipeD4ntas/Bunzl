using Bunzl.Domain.Entities;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Interfaces.Services;

public interface IUsuarioFornecedorService
{
    Task<Usuario> AdicionarUsuarioAsync(string email, string nome, EPerfilUsuario perfilPermissao, List<Empresa> empresas, bool flagVeioERP, CancellationToken cancellationToken);
    Task<Fornecedor> AdicionarFornecedorAsync(Usuario usuario, Fornecedor fornecedor, CancellationToken cancellationToken);
}
