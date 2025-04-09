using System.ComponentModel.DataAnnotations;

public class Viaggio
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Titolo del viaggio")]
    public string Titolo { get; set; } = "";
    
    [Url]
    [Display(Name = "URL immagine copertina")]
    public string ImmagineCopertina { get; set; } = "";

    [Required]
    [Display(Name = "Descrizione dettagliata")]
    public string Descrizione { get; set; } = "";

   
    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Data di partenza")]
    public DateTime DataPartenza { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Data di ritorno")]
    public DateTime DataRitorno { get; set; }

    [Range(1, 60)]
    [Display(Name = "Durata (giorni)")]
    public int DurataGiorni { get; set; }

    [Range(100, 10000)]
    [DataType(DataType.Currency)]
    [Display(Name = "Prezzo")]
    public decimal Prezzo { get; set; }

    [Required]
    [Display(Name = "Tipo di viaggio")]
    public string Tipologia { get; set; } = ""; // gruppo, autonomo

    public ICollection<Prenotazione>? Prenotazioni { get; set; }

    public ICollection<GiornoViaggio>? ProgrammaGiorni { get; set; }
}
