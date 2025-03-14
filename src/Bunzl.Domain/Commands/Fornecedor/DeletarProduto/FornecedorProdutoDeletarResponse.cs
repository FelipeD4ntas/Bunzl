using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.Fornecedor.DeletarProduto;

public class FornecedorProdutoDeletarResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem)
{
}
