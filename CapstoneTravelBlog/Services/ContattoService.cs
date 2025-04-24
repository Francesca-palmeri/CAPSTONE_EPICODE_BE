using CapstoneTravelBlog.DTOs;
using CapstoneTravelBlog.Settings;
using FluentEmail.Core;
using Microsoft.Extensions.Options;

namespace CapstoneTravelBlog.Services
{
    public class ContattoService
    {

        private readonly IFluentEmail _email;
        private readonly MailSettings _mailSettings;

        public ContattoService(IFluentEmail email, IOptions<MailSettings> options)
        {
            _email = email;
            _mailSettings = options.Value;
        }

        public async Task<bool> InviaMessaggioAsync(ContattoRequestDto dto)
        {
            try
            {
                var bodyPerTe = $"""
            Hai ricevuto un nuovo messaggio dal sito Travel Blog:

            Nome: {dto.Nome}
            Email: {dto.Email}
            Telefono: {dto.Telefono}

            Messaggio:
            {dto.Messaggio}
        """;

                var bodyPerUtente = $"""
            Ciao {dto.Nome},

            Grazie per averci contattato!
            Abbiamo ricevuto il tuo messaggio e ti risponderemo al più presto.

            Riepilogo:
            -----------------------
            Email: {dto.Email}
            Telefono: {dto.Telefono}
            Messaggio: {dto.Messaggio}
            -----------------------

            A presto!
            Il team di Travel Blog Japan
        """;

                // Mail per l'amministratore
                var toAdmin = await _email
                    .To("infotadaimanihon@gmail.com") 
                    .Subject("📩 Nuovo messaggio dal sito Travel Blog")
                    .Body(bodyPerTe, isHtml: false)
                    .SendAsync();

                // Mail di risposta al mittente
                var toUser = await _email
                    .To(dto.Email)
                    .Subject("Grazie per averci contattato – Travel Blog Japan")
                    .Body(bodyPerUtente, isHtml: false)
                    .SendAsync();

                return toAdmin.Successful && toUser.Successful;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante invio delle email:");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


    }
}
