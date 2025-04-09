using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class GiornoViaggio
{
    public int Id { get; set; }

    [Required]
    public int GiornoNumero { get; set; }

    [Required]
    [StringLength(100)]
    public string Titolo { get; set; } = "";

    [Required]
    [Display(Name = "Attività previste per il giorno")]
    public string Descrizione { get; set; } = "";

    [ForeignKey("ViaggioId")]
    public int ViaggioId { get; set; }

    [JsonIgnore]
    public Viaggio? Viaggio { get; set; }
}
