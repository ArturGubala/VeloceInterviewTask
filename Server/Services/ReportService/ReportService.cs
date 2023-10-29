using OfficeOpenXml;
using System.Text;
using UserSpying.Shared.Models;

namespace UserSpying.Server.Services.ReportService
{
    public class ReportService : IReportService
    {
        public async Task<byte[]> GenerateExcelReport(IEnumerable<User> users)
        {
            byte[] fileContents;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Raport - zdefiniowani użytkownicy");

                foreach (User user in users)
                {
                    var index = users.ToList().IndexOf(user) + 1;
                    worksheet.Cells[index, 1].Value = user.FirstName;
                    worksheet.Cells[index, 2].Value = user.LastName;
                    worksheet.Cells[index, 3].Value = user.DateOfBirth;
                    worksheet.Cells[index, 4].Value = user.Gender.Name;
                    worksheet.Cells[index, 5].Value = user.Gender.Honorific;
                    worksheet.Cells[index, 6].Value = DateTime.Now.Year - user.DateOfBirth.Value.Year;
                }

                fileContents = package.GetAsByteArray();
                return fileContents;
            }
        }

        public async Task<byte[]> GenerateCsvReport(IEnumerable<User> users)
        {
            var csvBuilder = new StringBuilder();

            csvBuilder.AppendLine("Imię,Nazwisko,Data urodzenia, Płeć, Zwrot grzecznościowy, Wiek");
            foreach (User user in users)
            {
                csvBuilder.AppendLine($"{user.FirstName},{user.LastName},{user.DateOfBirth},{user.Gender.Name},{user.Gender.Honorific},{DateTime.Now.Year - user.DateOfBirth.Value.Year}");
            }


            byte[] buffer = Encoding.UTF8.GetBytes(csvBuilder.ToString());

            return buffer;
        }
    }
}
