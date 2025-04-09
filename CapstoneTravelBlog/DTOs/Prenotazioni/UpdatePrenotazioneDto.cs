namespace CapstoneTravelBlog.DTOs.Prenotazioni
{
    public class UpdatePrenotazioneDto
    {
        public DateTime DataPrenotazione { get; set; }
        public string UtenteId { get; set; } = "";
        public int ViaggioId { get; set; }
    }
}
