namespace WebApi.Models
{
    public class UserNumber
    {
        public string IdNumber { get; set; } = default!;
        public string UserIdNumber { get; set; } = default!;
        public User User { get; set; } = default!;
    }
}
