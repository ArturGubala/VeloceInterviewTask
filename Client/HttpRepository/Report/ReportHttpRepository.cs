using Flurl;
using Flurl.Http;
using Flurl.Util;
using Microsoft.JSInterop;

namespace UserSpying.Client.HttpRepository.Report
{
    public class ReportHttpRepository : IReportHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public ReportHttpRepository(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task GenerateReportAsync(string type)
        {
            try
            {
                var response = await _httpClient.BaseAddress
                    .AppendPathSegment("users")
                    .AppendPathSegment("generate-report")
                    .SetQueryParam("type", type)
                    .GetAsync();

                IReadOnlyNameValueList<string> headers = response.Headers;
                foreach (var VARIABLE in headers)
                {
                    Console.WriteLine($"{VARIABLE.Name} {VARIABLE.Value}");
                }

                string contentDisposition = response.Headers.FirstOrDefault(h => h.Name == "Content-Disposition").Value;
                var filename = contentDisposition?.Split(";")
                    .FirstOrDefault(part => part.TrimStart().StartsWith("filename"))
                    ?.Split("=")[1]
                    ?.Trim('"') ?? "";

                using var stream = await response.GetStreamAsync();
                await SaveAs(filename, stream);
            }
            catch (FlurlHttpException ex)
            {
                // Handle exception (e.g., show an error message to the user)
            }
        }
        private async Task SaveAs(string filename, Stream content)
        {
            var buffer = new byte[content.Length];
            await content.ReadAsync(buffer, 0, buffer.Length);
            var base64Data = Convert.ToBase64String(buffer);

            await _jsRuntime.InvokeVoidAsync("saveAsFile", filename, base64Data);
        }
    }
}
