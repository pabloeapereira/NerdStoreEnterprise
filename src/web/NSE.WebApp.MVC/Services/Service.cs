using Microsoft.Extensions.Options;
using NSE.Core.Comunication;
using NSE.Core.Extensions;
using NSE.WebApp.MVC.Extensions;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NSE.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected readonly HttpClient _httpClient;
        protected readonly AppSettings _appSettings;

        protected Service(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _appSettings = settings.Value;
        }


        protected StringContent GetContent(object content) =>
            new(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");

        protected async Task<T> DeserializeObjectResponseAsync<T>(HttpResponseMessage responseMessage, CancellationToken cancellationToken = default) =>
            await responseMessage.Content.ReadFromJsonAsync<T>(JsonOptions, cancellationToken);

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

        protected async Task<ResponseResult> TratarErrosResponseERetornarResponseResultAsync(HttpResponseMessage? response)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            if (!TratarErrosResponse(response)) return await DeserializeObjectResponseAsync<ResponseResult>(response);
            return new ResponseResult();
        }

        protected JsonSerializerOptions JsonOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Default
        };
    }
}
