using CapstoneTravelBlog.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace CapstoneTravelBlog.Models
{
    public class Commento
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Testo { get; set; } = "";

        [Required]
        public DateTime DataCreazione { get; set; } = DateTime.Now;

        [Required]
        public string UtenteId { get; set; } = "";
        public ApplicationUser? Utente { get; set; }

        [Required]
        public int BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }
    }
}
