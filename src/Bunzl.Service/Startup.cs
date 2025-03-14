using Microsoft.Extensions.DependencyInjection;

namespace Bunzl.Application;

public static class Startup
{
    /// <summary>
    /// Initializes the domain services and configurations.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    public static void InitApplication(this IServiceCollection services)
    {
        Console.WriteLine("");
    }
}