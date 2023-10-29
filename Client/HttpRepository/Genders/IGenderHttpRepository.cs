using UserSpying.Shared.Models;

namespace UserSpying.Client.HttpRepository.Genders
{
    public interface IGenderHttpRepository
    {
        Task<Response<IEnumerable<Gender>?>> GetGenders();
    }
}
