using Bunzl.Infra.CrossCutting.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bunzl.Infra.CrossCutting.Security.Token;

public class TokenBuilder(IConfiguration configuration)
{
    private readonly string _jwtIssuer = configuration["Jwt:Issuer"] ?? "Lyncas";
    private readonly string _jwtAudience = configuration["Jwt:Audience"] ?? "Lyncas";
    private readonly string _jwtKey = configuration["Jwt:Key"] ?? "ef55d1ef-a0f1-4ee9-ae54-5fd56d57d643";
    private readonly byte _jwtDurationInHours = TryParseHelper.ByteTryParseOrDefault(configuration["Jwt:DurationInHours"], 1);

    private string _userId = string.Empty;
    private string _userName = string.Empty;
    private string _userCompany = string.Empty;
    private string _userProfile = string.Empty;
    private string _fornecedorId = string.Empty;
    private string _cnpjEmpresa = string.Empty;
    private string _idioma = string.Empty;
    private string _flagPrimeiroLogin = "false";

    public TokenBuilder WithCnpjEmpresa(string cnpjEmpresa) 
    {
        _cnpjEmpresa = cnpjEmpresa;
        return this;
    }

    public TokenBuilder WithUserId(string userId)
    {
        _userId = userId;
        return this;
    }

    public TokenBuilder WithUserName(string userName)
    {
        _userName = userName;
        return this;
    }

    public TokenBuilder WithUserCompany(string userCompany)
    {
        _userCompany = userCompany;
        return this;
    }

    public TokenBuilder WithProfile(string userProfile)
    {
        _userProfile = userProfile;
        return this;
    }

    public TokenBuilder WithFlagPrimeiroLogin(string flagPrimeiroLogin)
    {
        _flagPrimeiroLogin = flagPrimeiroLogin;
        return this;
    }

    public TokenBuilder WithFornecedorId(string fornecedorId)
    {
        _fornecedorId = fornecedorId;
        return this;
    }

    public TokenBuilder WithIdioma(string idioma)
    {
        _idioma = idioma;
        return this;
    }

    public string Build(DateTime? expires = null)
    {
        var claims = new List<Claim>
        {
            new(CustomClaims.UserId, _userId),
            new(CustomClaims.UserName, _userName),
            new(CustomClaims.UserCompany, _userCompany),
            new(CustomClaims.Profile, _userProfile),
            new(CustomClaims.FlagPrimeiroLogin, _flagPrimeiroLogin),
            new(CustomClaims.FornecedorId, _fornecedorId),
            new(CustomClaims.CnpjEmpresa, _cnpjEmpresa),
            new(CustomClaims.Idioma, _idioma)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtIssuer,
            audience: _jwtAudience,
            claims: claims,
            expires: expires ?? DateTime.UtcNow.AddHours(_jwtDurationInHours),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
