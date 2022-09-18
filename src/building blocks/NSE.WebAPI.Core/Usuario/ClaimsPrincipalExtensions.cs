using System.Security.Claims;

namespace NSE.WebAPI.Core.Usuario
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal is null)
                throw new ArgumentException(nameof(principal));
            return principal.FindFirstValue("sub");
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal is null)
                throw new ArgumentException(nameof(principal));
            return principal.FindFirstValue("email");
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal is null)
                throw new ArgumentException(nameof(principal));
            return principal.FindFirstValue("JWT");
        }
    }
}