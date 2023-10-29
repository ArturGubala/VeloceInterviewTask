using UserSpying.Shared.Models;

namespace UserSpying.Server.Services.ReportService
{
    public interface IReportService
    {
        Task<byte[]> GenerateExcelReport(IEnumerable<User> users);
        Task<byte[]> GenerateCsvReport(IEnumerable<User> users);
    }
}
