using UserSpying.Shared.Models;

namespace UserSpying.Client.HttpRepository.CustomFields
{
    public interface ICustomFieldHttpRepository
    {
        Task<Response<int?>> CreateCustomField(int userId, UpsertCustomField customField);
        Task<Response<CustomField?>> updateCustomFieldAsync(int userId, UpsertCustomField upsertCustomField);
    }
}
