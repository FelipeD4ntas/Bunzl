using Bunzl.Infra.CrossCutting.Security.Token;
using System.Security.Claims;

namespace Bunzl.Infra.CrossCutting.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetEmail(this ClaimsPrincipal principal)
            => principal.FindFirstValue(CustomClaims.UserCompany);

        //public static string? GetTenant(this ClaimsPrincipal principal)
        //    => principal.FindFirstValue(LyncasClaims.Tenant);

        //public static string? GetFullName(this ClaimsPrincipal principal)
        //    => principal?.FindFirst(LyncasClaims.Fullname)?.Value;

        public static string? GetFirstName(this ClaimsPrincipal principal)
            => principal?.FindFirst(ClaimTypes.Name)?.Value;

        public static string? GetSurname(this ClaimsPrincipal principal)
            => principal?.FindFirst(ClaimTypes.Surname)?.Value;

        public static string? GetPhoneNumber(this ClaimsPrincipal principal)
            => principal.FindFirstValue(ClaimTypes.MobilePhone);

        public static string? GetUserId(this ClaimsPrincipal principal)
           => principal.FindFirstValue(CustomClaims.UserId);

        //public static string? GetImageUrl(this ClaimsPrincipal principal)
        //   => principal.FindFirstValue(LyncasClaims.ImageUrl);

        //public static DateTimeOffset GetExpiration(this ClaimsPrincipal principal) =>
        //    DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(
        //        principal.FindFirstValue(LyncasClaims.Expiration)));

        private static string? FindFirstValue(this ClaimsPrincipal principal, string claimType) =>
            principal is null
                ? throw new ArgumentNullException(nameof(principal))
                : principal.FindFirst(claimType)?.Value;
    }
}
