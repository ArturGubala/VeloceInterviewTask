namespace UserSpying.Shared.Models 
{
    public record User : UpsertUser
    {
        public int Id { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual IEnumerable<CustomField> CustomFields { get; set; }
    }
}