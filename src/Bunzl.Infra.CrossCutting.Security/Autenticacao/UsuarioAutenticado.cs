using Bunzl.Infra.CrossCutting.Security.Token;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Bunzl.Infra.CrossCutting.Security.Autenticacao;

public class UsuarioAutenticado(IHttpContextAccessor accessor) : IUsuarioAutenticado
{
    public Guid UsuarioId => Guid.Parse(ObterClaim(CustomClaims.UserId) ?? "00000000-0000-0000-0000-000000000000");
    public string UsuarioNome => ObterClaim(CustomClaims.UserName) ?? "Falha ao Recuperar o Nome do Usuário";
    public Guid UsuarioEmpresa => Guid.Parse(ObterClaim(CustomClaims.UserCompany) ?? "00000000-0000-0000-0000-000000000000");
    public string Permissoes => ObterClaim(CustomClaims.Profile) ?? "Falha ao Recuperar as Permissões do Usuário";
    public DateTime Expiracao => ObterDataExpiracao();
    public string Idioma => ObterClaim(CustomClaims.Idioma) ?? "pt-BR";
    public string Profile => ObterClaim(CustomClaims.Profile) ?? "Falha ao Recuperar o Perfil do Usuário";
    public string CnpjEmpresa => ObterClaim(CustomClaims.CnpjEmpresa) ?? "Falha ao Recuperar Cnpj da Empresa";
    public string FlagPrimeiroLogin => ObterClaim(CustomClaims.FlagPrimeiroLogin) ?? "Falha ao Recuperar Flag Primeiro Login";

    private string? ObterClaim(string claimType)
    {
        var claim = accessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == claimType);
        return claim?.Value;
    }

    private DateTime ObterDataExpiracao()
    {
        var expClaim = ObterClaim(JwtRegisteredClaimNames.Exp);
        if (expClaim == null)
            return DateTime.MinValue;

        // Converter o valor do claim de expiração para DateTime
        // O valor do claim 'exp' é em segundos desde o Unix epoch
        if (long.TryParse(expClaim, out var exp))
            return DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;

        return DateTime.MinValue;
    }
}