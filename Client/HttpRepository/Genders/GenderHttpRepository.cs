using Flurl;
using Flurl.Http;
using UserSpying.Shared.Models;

namespace UserSpying.Client.HttpRepository.Genders
{
    public class GenderHttpRepository : IGenderHttpRepository
    {
        private readonly HttpClient _httpClient;

        public GenderHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<IEnumerable<Gender>?>> GetGenders()
        {
            try
            {
                Response<IEnumerable<Gender>?> response = await _httpClient.BaseAddress
                    .AppendPathSegment("genders")
                    .WithHeader("Accept", "*/*")
                    .WithHeader("Content-Type", "application/json")
                    .GetJsonAsync<Response<IEnumerable<Gender>?>>();

                return response;
            }
            catch (FlurlHttpException flurlHttpException)
            {
                var test = await flurlHttpException.GetResponseJsonAsync();
                return await flurlHttpException.GetResponseJsonAsync<Response<IEnumerable<Gender>?>>();
            }
        }
    }
}
