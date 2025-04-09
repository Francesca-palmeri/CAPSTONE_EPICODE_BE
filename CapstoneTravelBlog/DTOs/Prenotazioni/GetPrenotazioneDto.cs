namespace CapstoneTravelBlog.DTOs.Prenotazioni
{
    public class GetPrenotazioneDto
    {
        public int Id { get; set; }
        public DateTime DataPrenotazione { get; set; }

        public string UtenteId { get; set; } = "";
        public string? NomeUtente { get; set; }    
        public string? CognomeUtente { get; set; } 

        public int ViaggioId { get; set; }
        public string? TitoloViaggio { get; set; } 

                  
       }
}
