using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SendEmailFunction.Configuration;
using System.Runtime;
using System.Threading;
using System.Threading.Tasks;

namespace SendEmailFunction.Services
{
    public class SendEmailService
    {
        protected MailSettings mailSettings;
        public SendEmailService(MailSettings _mailSettings)
        {
            this.mailSettings = _mailSettings;
        }
        public async Task<bool> SendMail(string fileName, string toEmail)
        {
            CancellationToken cancellationToken = default;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.From));
            message.To.Add(new MailboxAddress("User", toEmail));

            message.Subject = "New file uploaded";

            message.Body = new TextPart("plain")
            {
                Text = $"A new file with name {fileName} has been uploaded to the Blob Storage."
            };

            using (var client = new SmtpClient())
            {              
                client.CheckCertificateRevocation = false;
                await client.ConnectAsync(mailSettings.Host, mailSettings.Port, SecureSocketOptions.SslOnConnect, cancellationToken);
                await client.AuthenticateAsync(mailSettings.UserEmailName, mailSettings.Password, cancellationToken);
                var result =  await client.SendAsync(message, cancellationToken);
                await client.DisconnectAsync(true, cancellationToken);
                return true;
            }
        }
    }
}
