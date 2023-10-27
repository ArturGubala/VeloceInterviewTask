using UserSpying.Shared.Models;

namespace UserSpying.Client.HttpRepository.Genders
{
    public interface IGender
    {
        Task<Response<IEnumerable<UserSpying.Shared.Models.Gender>?>> GetGenders();
    }
}
