namespace CapstoneTravelBlog.DTOs.Prenotazioni
{
    public class AddPrenotazioneDto
    {
        public DateTime? DataPrenotazione { get; set; }

        public string UtenteId { get; set; } = "";
        public int ViaggioId { get; set; }
    }

}
