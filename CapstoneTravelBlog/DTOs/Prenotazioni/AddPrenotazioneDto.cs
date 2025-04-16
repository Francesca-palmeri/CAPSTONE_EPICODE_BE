namespace CapstoneTravelBlog.DTOs.Prenotazioni
{
    public class AddPrenotazioneDto
    {
        public DateTime? DataPrenotazione { get; set; }

        public string UtenteId { get; set; } = "";
        public int ViaggioId { get; set; }

        public int NumeroPartecipanti { get; set; }
        public string Tipologia { get; set; } = "";

        public string Note { get; set; } = "";
    }

}
