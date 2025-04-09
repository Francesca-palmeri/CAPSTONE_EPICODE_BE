using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CapstoneTravelBlog.Models.Account
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(RoleId))]
        public ApplicationRole Role { get; set; } = null!;
    }
    }
