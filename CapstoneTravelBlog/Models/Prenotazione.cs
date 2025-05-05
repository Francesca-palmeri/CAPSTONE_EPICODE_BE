using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CapstoneTravelBlog.Models.Account;

public class Prenotazione
{
    public int Id { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Data della prenotazione")]
    public DateTime DataPrenotazione { get; set; } = DateTime.Now;

    [Required]
    [Display(Name = "Utente")]
    public string UtenteId { get; set; } = "";
    public ApplicationUser? Utente { get; set; }

    
    [Display(Name = "Viaggio")]
    [ForeignKey("ViaggioId")]
    public int? ViaggioId { get; set; }
    public Viaggio? Viaggio { get; set; }

    public int NumeroPartecipanti { get; set; }
    public string Tipologia { get; set; }
    public string? Note { get; set; }
    public string? DescrizionePersonalizzata { get; set; }

}
