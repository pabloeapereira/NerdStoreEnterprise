using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace NSE.WebAPI.Core.Usuario
{
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor _accessor;
        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor?.HttpContext?.User?.Identity?.Name;

        public bool IsAuthenticated() => _accessor.HttpContext.User.Identity.IsAuthenticated;

        public IEnumerable<Claim> ObterClaims() => _accessor.HttpContext.User.Claims;

        public HttpContext ObterHttpContext() => _accessor.HttpContext;

        public string ObterUserEmail() => IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : string.Empty;

        public Guid ObterUserId() => IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;

        public string ObterUserToken() => IsAuthenticated() ? _accessor.HttpContext.User.GetUserToken() : string.Empty;

        public bool PossuiRole(string role) => _accessor.HttpContext.User.IsInRole(role);
    }
}