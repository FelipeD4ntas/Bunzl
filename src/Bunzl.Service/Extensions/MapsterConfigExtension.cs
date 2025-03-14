using Microsoft.Extensions.DependencyInjection;

namespace Bunzl.Application.Extensions;

public static class MapsterConfigExtension
{
    public static IServiceCollection ConfigureMapster(this IServiceCollection services)
    {
        //TypeAdapterConfig<ETipoSolicitacaoDeCredito, EnumDto<ETipoSolicitacaoDeCredito>>
        //    .NewConfig()
        //    .Map(dest => dest.Id, src => Convert.ToInt32(src))
        //    .Map(dest => dest.Description, src => GetDescriptionFromEnumValue(src));

        //TypeAdapterConfig<EStatusSolicitacaoDeCredito, EnumDto<EStatusSolicitacaoDeCredito>>
        //    .NewConfig()
        //    .Map(dest => dest.Id, src => Convert.ToInt32(src))
        //    .Map(dest => dest.Description, src => GetDescriptionFromEnumValue(src));

        //TypeAdapterConfig<EFormaDePagamentoSolicitacaoDeCredito, EnumDto<EFormaDePagamentoSolicitacaoDeCredito>>
        //    .NewConfig()
        //    .Map(dest => dest.Id, src => Convert.ToInt32(src))
        //    .Map(dest => dest.Description, src => GetDescriptionFromEnumValue(src));

        //TypeAdapterConfig<EPerfilUsuario, EnumDto<EPerfilUsuario>>
        //    .NewConfig()
        //    .Map(dest => dest.Id, src => Convert.ToInt32(src))
        //    .Map(dest => dest.Description, src => GetDescriptionFromEnumValue(src));

        return services;
    }

    //private static string GetDescriptionFromEnumValue(Enum value)
    //{
    //    return value.GetType()
    //        .GetField(value.ToString())
    //        ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
    //        .SingleOrDefault() is not DescriptionAttribute attribute
    //        ? value.ToString()
    //        : attribute.Description;
    //}
}