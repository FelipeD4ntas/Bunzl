namespace Bunzl.Infra.CrossCutting.Templates;

public static class Startup
{
    /// <summary>
    /// Initializes the domain services and configurations.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    public static void InitTemplates(this IServiceCollection services)
    {
        services.AddRazorPages();
    }
}
