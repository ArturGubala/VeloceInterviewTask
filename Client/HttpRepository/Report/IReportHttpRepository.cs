namespace UserSpying.Client.HttpRepository.Report
{
    public interface IReportHttpRepository
    {
        Task GenerateReportAsync(string type);
    }
}
