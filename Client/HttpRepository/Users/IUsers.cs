using UserSpying.Client.Models;

namespace UserSpying.Client.HttpRepository.Users
{
    public interface IUsers
    {
        Task<Response<IEnumerable<CustomUser>>> GetUsersAsync();
        Task CreateUserAsync(CustomUser user);
        Task UpdateUserAsync(CustomUser user);
    }
}
