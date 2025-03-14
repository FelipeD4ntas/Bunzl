using Microsoft.Extensions.DependencyInjection;

namespace Bunzl.Infra.CrossCutting.Sms;

public static class Startup
{
    /// <summary>
    /// Initializes the domain services and configurations.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    public static void InitSms(this IServiceCollection services)
    {
        Console.WriteLine("");
    }
}