using UserSpying.Shared.Models;

namespace UserSpying.Client.Models
{
    public record CustomUser : User
    {
        public bool ShowDetails = false;
    }
}
