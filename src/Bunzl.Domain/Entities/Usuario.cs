using Bunzl.Core.Domain.Entities.Base;
using Bunzl.Core.Domain.Interfaces.Base;
using Bunzl.Domain.Entities.Validations;
using Bunzl.Domain.Enumerators;
using Bunzl.Infra.CrossCutting.Extensions;

namespace Bunzl.Domain.Entities;

public class Usuario : EntityBase<Guid>, IAggregationRoot
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public required EPerfilUsuario PerfilPermissao { get; set; }
    public bool FlagAtivo { get; set; } = true;
    public bool FlagVeioDoERP { get; set; } = false;
    public string? Senha { get; set; }
    public string? Telefone { get; set; }
    public string? Area { get; set; }
    public Guid? ChaveCadastro { get; set; }
    public Guid? ChaveResetSenha { get; set; }
    public string? CodigoOtp { get; set; }
    public DateTime? UltimoLogin { get; set; }
    public DateTime? DataGeracaoCodigoOtp { get; set; }
    public DateTime? DataPrimeiroLogin { get; set; }
    public DateTime? DataChaveResetSenha { get; set; }
    public List<Empresa> Empresas { get; set; } = [];
    public List<Fornecedor> Fornecedores { get; set; } = [];

    public void Atualizar(string nome, string email, string? telefone)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        AddNotifications(new UsuarioValidator().Validate(this));
    }

    public void Ativar()
    {
        FlagAtivo = true;
    }

    public void Inativar()
    {
        FlagAtivo = false;
    }

    public void AlterarSenha(string senha)
    {
        Senha = senha.EncryptPassword();
        AddNotifications(new UsuarioValidator().Validate(this));
    }

    public void GerarChaveCadastro()
    {
        ChaveCadastro = Guid.NewGuid();
    }

    public void LimparChaveCadastro()
    {
        ChaveCadastro = null;
    }

    public void GerarChaveResetSenha()
    {
        ChaveResetSenha = Guid.NewGuid();
        DataChaveResetSenha = DateTime.UtcNow;
    }

    public void LimparChaveResetSenha()
    {
        ChaveResetSenha = null;
        DataChaveResetSenha = null;
    }

    public void RelacionarEmpresas(List<Empresa> empresas)
    {
        empresas.ForEach(empresa =>
        {
            Empresas.Add(empresa);
        });
    }

    public void RelacionarEmpresa(Empresa empresa)
    {
        Empresas.Add(empresa);
    }
}