namespace UsersSpying.Shared.Models
{
    public record UpsertUser
    {
        public int GenderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
