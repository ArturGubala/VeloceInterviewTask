using UserSpying.Shared.Models;

namespace UserSpying.Client.HttpRepository.CustomFields
{
    public interface ICustomField
    {
        Task<Response<int?>> CreateCustomField(int userId, UpsertCustomField customField);
    }
}
