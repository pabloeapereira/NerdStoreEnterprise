using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Services.Handlers
{
    public class HttpClientAuthorizationDelegatingHandler:DelegatingHandler
    {
        private readonly IUser _user;

        public HttpClientAuthorizationDelegatingHandler(IUser user)
        {
            _user = user;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _user.ObterHttpContext().Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader))
                request.Headers.Add("Authorization", new List<string> { authorizationHeader });

            var token = _user.ObterUserToken();
            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new("Bearer", token);

            return base.SendAsync(request, cancellationToken);
        }
    }
}