using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions
{
    public interface IUser
    {
        string Name { get; }
        Guid ObterUserId();
        string ObterUserEmail();
        string ObterUserToken();
        bool IsAuthenticated();
        bool PossuiRole(string role);
        IEnumerable<Claim> ObterClaims();
        HttpContext ObterHttpContext();
    }

    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;
        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User?.Identity.Name;

        public bool IsAuthenticated() => _accessor.HttpContext.User.Identity.IsAuthenticated;

        public IEnumerable<Claim> ObterClaims() => _accessor.HttpContext.User.Claims;

        public HttpContext ObterHttpContext() => _accessor.HttpContext;

        public string ObterUserEmail() => IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : string.Empty;

        public Guid ObterUserId() => IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;

        public string ObterUserToken() => IsAuthenticated() ?_accessor.HttpContext.User.GetUserToken() : string.Empty;

        public bool PossuiRole(string role) => _accessor.HttpContext.User.IsInRole(role);
    }

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