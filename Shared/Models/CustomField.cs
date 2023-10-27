using System.ComponentModel.DataAnnotations;

namespace UserSpying.Shared.Models
{
    public record CustomField : UpsertCustomField
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
