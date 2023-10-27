using Flurl.Http;
using Flurl;
using UserSpying.Shared.Models;

namespace UserSpying.Client.HttpRepository.CustomFields
{
    public class CustomField : ICustomField
    {
        private readonly HttpClient _httpClient;

        public CustomField(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<int?>> CreateCustomField(int userId, UpsertCustomField customField)
        {
            try
            {
                Response<int?> response = await _httpClient.BaseAddress
                    .AppendPathSegment($"users/{userId}/custom-fields")
                    .WithHeader("Accept", "*/*")
                    .WithHeader("Content-Type", "application/json")
                    .PostJsonAsync(customField)
                    .ReceiveJson<Response<int?>>();

                return response;
            }
            catch (FlurlHttpException flurlHttpException)
            {
                var test = await flurlHttpException.GetResponseJsonAsync();
                return await flurlHttpException.GetResponseJsonAsync<Response<int?>>();
            }
        }
    }
}
