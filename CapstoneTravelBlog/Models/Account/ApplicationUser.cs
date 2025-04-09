using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CapstoneTravelBlog.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required DateOnly BirthDate { get; set; }

        [Required]
        public required override string Email { get; set; }

        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; } = new List<ApplicationUserRole>();

        public ICollection<Prenotazione> Prenotazioni { get; set; } = new List<Prenotazione>();

    }
}
