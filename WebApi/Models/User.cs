namespace WebApi.Models
{
    public class User
    {
        public string IdNumber { get; set; } = default!;
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public List<UserNumber>? UserNumbers { get; set; }
    }
}
