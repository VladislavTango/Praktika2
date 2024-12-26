using MailKit.Net.Smtp;
using MimeKit;
using PraktikaDomain.Interfaces;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace PraktikaInfrastructure.Email
{
    public class EmailSenderService : IEmailService
    {
        public async Task SendConfirmCode(string mail, int code)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("kkkk", "turbovlat@gmail.com"));
            message.To.Add(new MailboxAddress("Client Confirm Code", $"{mail}"));
            message.Subject = "dasdasdasdasda";

            message.Body = new TextPart("plain")
            {
                Text = $"Код для подтверждения вашего аккаунта: {code}"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                await client.AuthenticateAsync("turbovlat@gmail.com", "zgff auce qazk duqp");

                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }

        public async Task SendTransportationStatus(string TransportationStatus, string mail)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("kkkk", "turbovlat@gmail.com"));
            message.To.Add(new MailboxAddress("Client Confirm Code", $"{mail}"));
            message.Subject = "dasdasdasdasda";

            message.Body = new TextPart("plain")
            {
                Text = $"Статус вашего заказа изменился: {TransportationStatus}"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                await client.AuthenticateAsync("turbovlat@gmail.com", "zgff auce qazk duqp");

                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
    }
}
