using Flurl;
using Flurl.Http;
using UserSpying.Shared.Models;

namespace UserSpying.Client.HttpRepository.Genders
{
    public class Gender : IGender
    {
        private readonly HttpClient _httpClient;

        public Gender(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<IEnumerable<UserSpying.Shared.Models.Gender>?>> GetGenders()
        {
            try
            {
                Response<IEnumerable<UserSpying.Shared.Models.Gender>?> response = await _httpClient.BaseAddress
                    .AppendPathSegment("genders")
                    .WithHeader("Accept", "*/*")
                    .WithHeader("Content-Type", "application/json")
                    .GetJsonAsync<Response<IEnumerable<UserSpying.Shared.Models.Gender>?>>();

                return response;
            }
            catch (FlurlHttpException flurlHttpException)
            {
                var test = await flurlHttpException.GetResponseJsonAsync();
                return await flurlHttpException.GetResponseJsonAsync<Response<IEnumerable<UserSpying.Shared.Models.Gender>?>>();
            }
        }
    }
}
