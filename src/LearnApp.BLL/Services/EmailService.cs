using LearnApp.BLL.Interfaces;
using LearnApp.Common.Config;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;

namespace LearnApp.BLL.Services
{
    /// <summary>
    /// Service for sending email approval for registration.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Constructor which resolves services below.
        /// </summary>
        /// <param name="appSettings">Application settings.</param>
        public EmailService(IOptions<AppSettings> appSettings)
        {
            if (appSettings is null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }
            _appSettings = appSettings.Value;
        }

        /// <inheritdoc/>
        public void Send(string to, string subject, string html, string from = null)
        {
            // Create message
            var email = new MimeMessage();

            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };
            email.Sender = MailboxAddress.Parse(from ?? _appSettings.EmailFrom);

            //Send email
            using (var smtp = new SmtpClient())
            {
                smtp.CheckCertificateRevocation = false;

                smtp.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort, SecureSocketOptions.Auto);
                smtp.Authenticate(_appSettings.SmtpUser, _appSettings.SmtpPass);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}
