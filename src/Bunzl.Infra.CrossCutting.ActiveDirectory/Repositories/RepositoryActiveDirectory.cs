using System.DirectoryServices.AccountManagement;
using System.Runtime.Versioning;
using Bunzl.Infra.CrossCutting.ActiveDirectory.DTOs;
using Bunzl.Infra.CrossCutting.ActiveDirectory.Interfaces;

namespace Bunzl.Infra.CrossCutting.ActiveDirectory.Repositories;

[SupportedOSPlatform("windows")]
public class RepositoryActiveDirectory(AdConfig config) : IRepositoryActiveDirectory
{
    private readonly AdConfig _adConfig = config ?? throw new ArgumentNullException(nameof(config));

    public bool EmailExists(string email)
    {
        var exists = false;

        if (_adConfig.SkipValidation) return true;

        using var context = new PrincipalContext(ContextType.Domain, _adConfig.Domain, _adConfig.Container);
        var userCriteria = new UserPrincipal(context)
        {
            Enabled = true,
            EmailAddress = email
        };

        using var searcher = new PrincipalSearcher(userCriteria);

        if (searcher.FindOne() is UserPrincipal user) exists = true;

        return exists;
    }

    public bool ValidarCredencial(string email, string senha)
    {
        if (_adConfig.SkipValidation) return true;

        using var context = new PrincipalContext(ContextType.Domain, _adConfig.Domain, _adConfig.Container);
        var userCriteria = new UserPrincipal(context)
        {
            Enabled = true,
            EmailAddress = email
        };

        using var searcher = new PrincipalSearcher(userCriteria);

        var userAd = searcher.FindOne() as UserPrincipal;
        return userAd is not null && context.ValidateCredentials(userAd.UserPrincipalName, senha);
    }
}