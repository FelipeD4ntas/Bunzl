using Bunzl.Application;
using Bunzl.Application.Extensions;
using Bunzl.Domain;
using Bunzl.Infra.CrossCutting.ConexaoClient;
using Bunzl.Infra.CrossCutting.Email;
using Bunzl.Infra.CrossCutting.IoC;
using Bunzl.Infra.CrossCutting.MediatoR;
using Bunzl.Infra.CrossCutting.Middlewares;
using Bunzl.Infra.CrossCutting.Security;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using Bunzl.Infra.CrossCutting.Sms;
using Bunzl.Infra.CrossCutting.Swagger;
using Bunzl.Infra.CrossCutting.Templates;
using Bunzl.Infra.Data.Auth;
using Bunzl.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bunzl.Infra.CrossCutting.HttpClientsExtensions;
using DevExpress.XtraReports.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.SetupSwagger();

builder.Services.AddAuth(builder.Configuration);

builder.Services.AddScoped<BunzlContext>(provider =>
{
    var optionsBuilder = new DbContextOptionsBuilder<BunzlContext>();
    optionsBuilder.EnableSensitiveDataLogging();
    var options = optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("BunzlConnection")).Options;
    var userContext = provider.GetService<IUsuarioAutenticado>();
    return new BunzlContext(options, userContext!);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(config =>
    {
        config.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.InitApplication();
builder.Services.InitDomain();
builder.Services.InitConexaoClient();
builder.Services.InitTemplates();
builder.Services.InitEmail();
builder.Services.InitSms();
builder.Services.AddHttpClients(builder.Configuration);

var assemblies = AppDomain.CurrentDomain.GetAssemblies();

builder.Services
    .AddInject(assemblies)
    .AddCurrentUser()
    .AddMediator(assemblies)
    .ConfigureMapster();

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.UseCurrentUser();

app.MapControllers();

app.UseMiddleware<ValidationExceptionMiddleware>();

ScriptPermissionManager.GlobalInstance = new ScriptPermissionManager(ExecutionMode.Unrestricted);

app.Run();