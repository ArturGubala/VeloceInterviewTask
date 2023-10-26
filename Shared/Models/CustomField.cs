using System.ComponentModel.DataAnnotations;

namespace UsersSpying.Shared.Models
{
    public record CustomField : UpsertCustomField
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
