namespace WebApi.Models.DTOs
{
    public class UserDTO
    {
        public string IdNumber { get; set; } = default!;
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public List<UserNumberDTO>? UserNumbers { get; set; }
    }
}
