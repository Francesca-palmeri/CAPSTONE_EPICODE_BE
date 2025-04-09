using System.ComponentModel.DataAnnotations;

namespace CapstoneTravelBlog.DTOs
{
    public class UserDto
    {
        public int? UserId { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }
    }
}
