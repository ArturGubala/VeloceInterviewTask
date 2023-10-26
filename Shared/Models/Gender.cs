using System.ComponentModel.DataAnnotations;

namespace UsersSpying.Shared.Models
{
    public record Gender
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public char Abbreviation { get; set; }
        [Required]
        public string Honorific { get; set; }
    }
}
