using Flurl;
using Flurl.Http;
using UserSpying.Client.Models;

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
                var data = await _httpClient.BaseAddress
                    .AppendPathSegment("users")
                    .WithHeader("Accept", "*/*")
                    .WithHeader("Content-Type", "application/json")
                    .GetJsonAsync<IEnumerable<CustomUser>>();

                var response = new Response<IEnumerable<CustomUser>>()
                {
                    Data = data,
                    StatusCode = null,
                    Message = ""
                };

                return response;
            }
            catch (FlurlHttpException flurlHttpException)
            {
                var test = await flurlHttpException.GetResponseJsonAsync();
                return await flurlHttpException.GetResponseJsonAsync<Response<IEnumerable<CustomUser>>>();
            }
        }

        public async Task CreateUserAsync(CustomUser user)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserAsync(CustomUser user)
        {
            throw new NotImplementedException();
        }
    }
}
