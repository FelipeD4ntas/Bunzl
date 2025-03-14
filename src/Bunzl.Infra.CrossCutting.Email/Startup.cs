using Microsoft.Extensions.DependencyInjection;

namespace Bunzl.Infra.CrossCutting.Email;

public static class Startup
{
    /// <summary>
    /// Initializes the domain services and configurations.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    public static void InitEmail(this IServiceCollection services)
    {
        Console.WriteLine("");
    }
}