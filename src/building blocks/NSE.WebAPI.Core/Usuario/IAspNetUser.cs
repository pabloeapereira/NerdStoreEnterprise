using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace NSE.WebAPI.Core.Usuario
{
    public interface IAspNetUser
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
}
