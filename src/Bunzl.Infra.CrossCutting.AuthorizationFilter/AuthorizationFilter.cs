using Bunzl.Core.Domain.Interfaces;
using Bunzl.Domain.Enumerators;
using Bunzl.Infra.CrossCutting.Security.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bunzl.Infra.CrossCutting.AuthorizationFilter;

public class AuthorizationFilter(EPerfilUsuario perfilPermissao) : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var currentUser = (ICurrentUser)context.HttpContext.RequestServices.GetService(typeof(ICurrentUser))!;

        if (!currentUser.IsAuthenticated())
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var perfilPermissaoClaim = currentUser.GetUserClaims().FirstOrDefault(x => x.Type == CustomClaims.Profile);

        if (perfilPermissaoClaim == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var perfilUsuario = Enum.Parse<EPerfilUsuario>(perfilPermissaoClaim.Value);

        if (perfilUsuario != perfilPermissao)
        {
            context.Result = new ForbidResult();
        }
    }
}