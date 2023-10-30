using System.ComponentModel.DataAnnotations;

namespace UserSpying.Shared.Models
{
    public record UpsertUser
    {
        public int GenderId { get; set; }
        [MaxLength(50, ErrorMessage = "Maksymalna długość pola Imię wynosi 50 znaków")]
        public string FirstName { get; set; }
        [MaxLength(150, ErrorMessage = "Maksymalna długość pola Imię wynosi 150 znaków")]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
