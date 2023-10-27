using System.ComponentModel.DataAnnotations;

namespace UserSpying.Shared.Models
{
    public record UpsertCustomField
    {
        [Required]
        public string Name { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}
