using Flurl;
using Flurl.Http;
using UserSpying.Client.Models;
using UserSpying.Shared.Models;

namespace UserSpying.Client.HttpRepository.Users
{
    public class Users : IUsers
    {
        private readonly HttpClient _httpClient;

        public Users(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<IEnumerable<CustomUser>>> GetUsersAsync()
        {
            try
            {
                Response<IEnumerable<CustomUser>> response = await _httpClient.BaseAddress
                    .AppendPathSegment("users")
                    .WithHeader("Accept", "*/*")
                    .WithHeader("Content-Type", "application/json")
                    .GetJsonAsync<Response<IEnumerable<CustomUser>>>();

                return response;
            }
            catch (FlurlHttpException flurlHttpException)
            {
                var test = await flurlHttpException.GetResponseJsonAsync();
                return await flurlHttpException.GetResponseJsonAsync<Response<IEnumerable<CustomUser>>>();
            }
        }

        public async Task<Response<int?>> CreateUserAsync(UpsertUser user)
        {
            try
            {
                Response<int?> response = await _httpClient.BaseAddress
                    .AppendPathSegment("users")
                    .WithHeader("Accept", "*/*")
                    .WithHeader("Content-Type", "application/json")
                    .PostJsonAsync(user)
                    .ReceiveJson<Response<int?>>();

                return response;
            }
            catch (FlurlHttpException flurlHttpException)
            {
                var test = await flurlHttpException.GetResponseJsonAsync();
                return await flurlHttpException.GetResponseJsonAsync<Response<int?>>();
            }
        }

        public async Task<Response<int?>> UpdateUserAsync(int userId, UpsertUser user)
        {
            try
            {
                Response<int?> response = await _httpClient.BaseAddress
                    .AppendPathSegment($"users/{userId}")
                    .WithHeader("Accept", "*/*")
                    .WithHeader("Content-Type", "application/json")
                    .PutJsonAsync(user)
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
