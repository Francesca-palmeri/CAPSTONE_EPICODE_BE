using System.ComponentModel.DataAnnotations;
using CapstoneTravelBlog.Models;

public class BlogPost
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    [Display(Name = "Titolo dell'articolo")]
    public string Titolo { get; set; } = "";

    [Required]
    [Display(Name = "Contenuto")]
    public string Contenuto { get; set; } = "";

    [Url]
    [Display(Name = "Immagine di copertina")]
    public string ImmagineCopertina { get; set; } = "";

    [Required]
    [StringLength(50)]
    [Display(Name = "Categoria")]
    public string Categoria { get; set; } = "";

    [DataType(DataType.Date)]
    [Display(Name = "Data di pubblicazione")]
    public DateTime DataPubblicazione { get; set; } = DateTime.Now;

    public ICollection<Commento>? Commenti { get; set; }

}
