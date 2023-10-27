using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserSpying.Shared.Models
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
        [JsonIgnore]
        public virtual IEnumerable<User> Users { get; set; }
    }
}
