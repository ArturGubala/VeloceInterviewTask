using Flurl.Http;
using Flurl;
using UserSpying.Shared.Models;

namespace UserSpying.Client.HttpRepository.CustomFields
{
    public class CustomFieldHttpRepository : ICustomFieldHttpRepository
    {
        private readonly HttpClient _httpClient;

        public CustomFieldHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<int?>> CreateCustomField(int userId, UpsertCustomField upsertCustomField)
        {
            try
            {
                Response<int?> response = await _httpClient.BaseAddress
                    .AppendPathSegment($"users/{userId}/custom-fields")
                    .WithHeader("Accept", "*/*")
                    .WithHeader("Content-Type", "application/json")
                    .PostJsonAsync(upsertCustomField)
                    .ReceiveJson<Response<int?>>();

                return response;
            }
            catch (FlurlHttpException flurlHttpException)
            {
                var test = await flurlHttpException.GetResponseJsonAsync();
                return await flurlHttpException.GetResponseJsonAsync<Response<int?>>();
            }
        }

        public async Task<Response<CustomField?>> updateCustomFieldAsync(int customFieldId, UpsertCustomField upsertCustomField)
        {
            try
            {
                Response<CustomField?> response = await _httpClient.BaseAddress
                    .AppendPathSegment($"custom-fields")
                    .AppendPathSegment($"{customFieldId}")
                    .WithHeader("Accept", "*/*")
                    .WithHeader("Content-Type", "application/json")
                    .PutJsonAsync(upsertCustomField)
                    .ReceiveJson<Response<CustomField?>>();

                return response;
            }
            catch (FlurlHttpException flurlHttpException)
            {
                var test = await flurlHttpException.GetResponseJsonAsync();
                return await flurlHttpException.GetResponseJsonAsync<Response<CustomField?>>();
            }
        }
    }
}
