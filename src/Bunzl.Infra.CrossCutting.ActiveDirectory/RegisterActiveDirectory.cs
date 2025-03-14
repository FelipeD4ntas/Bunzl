using System.Runtime.Versioning;
using Bunzl.Infra.CrossCutting.ActiveDirectory.DTOs;
using Bunzl.Infra.CrossCutting.ActiveDirectory.Interfaces;
using Bunzl.Infra.CrossCutting.ActiveDirectory.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bunzl.Infra.CrossCutting.ActiveDirectory;

public static class RegisterActiveDirectory
{
    [SupportedOSPlatform("windows")]
    public static IServiceCollection AddActiveDirectory(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IRepositoryActiveDirectory>(_ => new RepositoryActiveDirectory(new AdConfig
        {
            Domain = configuration["ActiveDirectory:Domain"]!,
            Container = configuration["ActiveDirectory:Container"]!,
            SkipValidation = Convert.ToBoolean(configuration["ActiveDirectory:SkipValidation"])
        }));

        return services;
    }
}