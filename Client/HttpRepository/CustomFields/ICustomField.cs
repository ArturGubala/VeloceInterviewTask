using UserSpying.Shared.Models;

namespace UserSpying.Client.HttpRepository.CustomFields
{
    public interface ICustomField
    {
        Task<Response<int?>> CreateCustomField(int userId, UpsertCustomField customField);
        Task<Response<UserSpying.Shared.Models.CustomField?>> updateCustomFieldAsync(int userId, UpsertCustomField upsertCustomField);
    }
}
