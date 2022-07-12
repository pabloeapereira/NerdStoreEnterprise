using NSE.WebApp.MVC.Extensions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected StringContent GetContent(object content) =>
            new(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");

        protected async Task<T> DeserializeObjectResponse<T>(HttpResponseMessage responseMessage) =>
            await JsonSerializer.DeserializeAsync<T>(await responseMessage.Content.ReadAsStreamAsync(), JsonOptions);

        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.InternalServerError:
                    throw new CustomHttpRequestException(response.StatusCode);
                case HttpStatusCode.BadRequest:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        protected JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
