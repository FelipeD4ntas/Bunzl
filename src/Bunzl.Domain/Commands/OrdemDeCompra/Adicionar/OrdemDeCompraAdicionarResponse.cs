namespace Bunzl.Domain.Commands.OrdemDeCompra.Adicionar;

public class OrdemDeCompraAdicionarResponse(string mensagem)
{
    public string Mensagem { get; set; } = mensagem;
}