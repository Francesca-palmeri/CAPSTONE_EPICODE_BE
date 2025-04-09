using Microsoft.AspNetCore.Identity;

namespace CapstoneTravelBlog.Models.Account
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; } = new List<ApplicationUserRole>();
    }
   
}
