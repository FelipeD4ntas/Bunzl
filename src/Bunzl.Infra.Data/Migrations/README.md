CREATE MIGRATION:
````shell
dotnet ef migrations add Initial --project ./src/Bunzl.Infra.Data/Bunzl.Infra.Data.csproj --startup-project ./src/Bunzl.WebApi/Bunzl.WebApi.csproj -c BunzlContext
````

UPDATE DATABASE:
````shell
dotnet ef database update --project ./src/Bunzl.Infra.Data/Bunzl.Infra.Data.csproj --startup-project ./src/Bunzl.WebApi/Bunzl.WebApi.csproj -c BunzlContext
````

GERAR SCRIPT DE MIGRATION:
````shell
dotnet ef migrations script --project ./src/Bunzl.Infra.Data/Bunzl.Infra.Data.csproj --startup-project ./src/Bunzl.WebApi/Bunzl.WebApi.csproj -c BunzlContext
````

GERAR PARTE SCRIPT DE MIGRATION:
````shell
dotnet ef migrations script **MIGRATION_DE** **MIGRATION_PARA**  --project ./src/Bunzl.Infra.Data/Bunzl.Infra.Data.csproj --startup-project ./src/Bunzl.WebApi/Bunzl.WebApi.csproj -c BunzlContext
````