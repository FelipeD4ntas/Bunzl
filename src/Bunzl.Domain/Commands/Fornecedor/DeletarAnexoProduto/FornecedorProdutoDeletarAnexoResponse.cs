using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.Fornecedor.DeletarAnexoProduto;

public class FornecedorProdutoDeletarAnexoResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem);
