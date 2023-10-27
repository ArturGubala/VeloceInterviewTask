namespace UserSpying.Shared.Models
{
    public record UpsertUser
    {
        public int GenderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
