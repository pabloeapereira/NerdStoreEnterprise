using NSE.Core.Comunication;
using NSE.Core.Extensions;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NSE.Bff.Compras.Services
{
    public abstract class Service
    {
        protected readonly HttpClient _httpClient;

        protected Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        protected StringContent GetContent(object content) =>
            new(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");

        protected async Task<T> DeserializeObjectResponse<T>(HttpResponseMessage responseMessage) =>
            await JsonSerializer.DeserializeAsync<T>(await responseMessage.Content.ReadAsStreamAsync(), JsonOptions);

        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest) return false;

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