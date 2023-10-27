using UserSpying.Client.Models;
using UserSpying.Shared.Models;

namespace UserSpying.Client.HttpRepository.Users
{
    public interface IUsers
    {
        Task<Response<IEnumerable<CustomUser>>> GetUsersAsync();
        Task<Response<int?>> CreateUserAsync(UpsertUser user);
        Task<Response<int?>> UpdateUserAsync(int userId, UpsertUser user);
    }
}
