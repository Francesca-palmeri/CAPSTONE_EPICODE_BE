namespace CapstoneTravelBlog.DTOs
{
    public class LoginRequestDto
    {
        public required string Email { get; set; }

        public string Password { get; set; }
    }
}
