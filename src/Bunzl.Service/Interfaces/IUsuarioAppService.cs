using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Commands.Usuario.Adicionar;
using Bunzl.Domain.Commands.Usuario.AtualizarSenhaTelefone;
using Bunzl.Domain.Commands.Usuario.Ativar;
using Bunzl.Domain.Commands.Usuario.Atualizar;
using Bunzl.Domain.Commands.Usuario.ConfirmarCadastro;
using Bunzl.Domain.Commands.Usuario.Deletar;
using Bunzl.Domain.Commands.Usuario.Inativar;
using Bunzl.Domain.Commands.Usuario.Listar;
using Bunzl.Domain.Commands.Usuario.Obter;
using Bunzl.Domain.Commands.Usuario.ResetSenha;
using Bunzl.Domain.Commands.Usuario.SolicitarResetSenha;
using Bunzl.Domain.Commands.Usuario.VerificarResetSenha;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;

namespace Bunzl.Application.Interfaces;

public interface IUsuarioAppService
{
    Task<CommandResponse<UsuarioAdicionarResponse>> Adicionar(UsuarioAdicionarRequest request);
    Task<CommandResponse<DataSourcePageResponse>> ListarUsuario(UsuarioListarRequest request);
    Task<CommandResponse<UsuarioObterResponse>> ObterUsuario(UsuarioObterRequest request);
    Task<CommandResponse<UsuarioAtualizarResponse>> AtualizarUsuario(UsuarioAtualizarRequest request);
    Task<CommandResponse<UsuarioAtivarResponse>> AtivarUsuario(Guid id);
    Task<CommandResponse<UsuarioInativarResponse>> InativarUsuario(Guid id);
    Task<CommandResponse<UsuarioDeletarResponse>> DeletarUsuario(Guid id);
    Task<CommandResponse<UsuarioAtualizarSenhaTelefoneResponse>> AtualizarSenhaTelefone(UsuarioAtualizarSenhaTelefoneRequest request);
    Task<CommandResponse<UsuarioConfirmarCadastroResponse>> ConfirmarCadastro(UsuarioConfirmarCadastroRequest request);
    Task<CommandResponse<UsuarioSolicitarResetSenhaResponse>> SolicitarResetSenha(UsuarioSolicitarResetSenhaRequest request);
    Task<CommandResponse<UsuarioResetSenhaResponse>> ResetSenha(UsuarioResetSenhaRequest request);
    Task<CommandResponse<UsuarioVerificarResetSenhaResponse>> VerificarResetSenha(UsuarioVerificarResetSenhaRequest request);
}