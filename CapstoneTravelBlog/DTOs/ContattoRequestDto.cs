using System.ComponentModel.DataAnnotations;

namespace CapstoneTravelBlog.DTOs
{
    public class ContattoRequestDto
    {
        [Required(ErrorMessage = "Il campo Nome è obbligatorio")]
        public string Nome { get; set; } = "";
        [Required(ErrorMessage = "Il campo Email è obbligatorio")]
        public string Email { get; set; } = "";
        [Required]
        [RegularExpression(@"^\+?\d{8,15}$", ErrorMessage = "Numero di telefono non valido")]
        public string Telefono { get; set; } = "";
        [Required(ErrorMessage = "Il campo Messaggio è obbligatorio")]
        public string Messaggio { get; set; } = "";
    }
}
