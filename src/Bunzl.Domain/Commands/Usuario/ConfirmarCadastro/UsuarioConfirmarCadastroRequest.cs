using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.ConfirmarCadastro;

public class UsuarioConfirmarCadastroRequest : IRequest<CommandResponse<UsuarioConfirmarCadastroResponse>>
{
    public required Guid ChaveCadastro { get; set; }
    public required string Telefone { get; set; }
    public required string NovaSenha { get; set; }
    public required string ConfirmacaoNovaSenha { get; set; }
    public required string Area { get; set; }
}