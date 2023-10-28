using Flurl.Http;
using Flurl;
using UserSpying.Shared.Models;
using static UserSpying.Client.Shared.Dialogs.AddUserDialog;

namespace UserSpying.Client.HttpRepository.CustomFields
{
    public class CustomField : ICustomField
    {
        private readonly HttpClient _httpClient;

        public CustomField(HttpClient httpClient)
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

        public async Task<Response<UserSpying.Shared.Models.CustomField?>> updateCustomFieldAsync(int customFieldId, UpsertCustomField upsertCustomField)
        {
            try
            {
                Response<UserSpying.Shared.Models.CustomField?> response = await _httpClient.BaseAddress
                    .AppendPathSegment($"custom-fields")
                    .AppendPathSegment($"{customFieldId}")
                    .WithHeader("Accept", "*/*")
                    .WithHeader("Content-Type", "application/json")
                    .PutJsonAsync(upsertCustomField)
                    .ReceiveJson<Response<UserSpying.Shared.Models.CustomField?>>();

                return response;
            }
            catch (FlurlHttpException flurlHttpException)
            {
                var test = await flurlHttpException.GetResponseJsonAsync();
                return await flurlHttpException.GetResponseJsonAsync<Response<UserSpying.Shared.Models.CustomField?>>();
            }
        }
    }
}
