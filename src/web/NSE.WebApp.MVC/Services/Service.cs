using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
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

        protected async Task<ResponseResult> TratarErrosResponseERetornarResponseResultAsync(HttpResponseMessage? response)
        {
            if (!TratarErrosResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);
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
