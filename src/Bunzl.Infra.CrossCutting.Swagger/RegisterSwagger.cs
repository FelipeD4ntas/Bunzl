using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Reflection;

namespace Bunzl.Infra.CrossCutting.Swagger;

public static class RegisterSwagger
{
    public static void SetupSwagger(this IServiceCollection services)
    {
        var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        var fileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        var productVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Bunzl", 
                Version = $"File Version: {fileVersion} - Product Version: {productVersion}"
            });

            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = """
                              <b>JWT Autorização</b> <br/> 
                                                    Digite 'Bearer' [espaço] e em seguida seu token na caixa de texto abaixo.
                                                    <br/> <br/>
                                                    <b>Exemplo:</b> 'bearer 123456abcdefg...'
                              """,
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            config.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });

            config.SwaggerDoc("Fornecedores", new OpenApiInfo { Title = "API Fornecedores", Version = "v1" });
            config.SwaggerDoc("Produtos", new OpenApiInfo { Title = "API Produtos", Version = "v1" });
        });
    }
}