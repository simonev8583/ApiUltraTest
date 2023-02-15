using ApiUltraTest.Infrastructure.Providers.Configs;
using ApiUltraTest.Application.Interfaces;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using System.Net.Mail;
using System.Net;
using MimeKit;
using System;

namespace ApiUltraTest.Infrastructure.Providers.MailService
{
    public class MailKitService : IMailNotificationService
    {
        private readonly MailSettings _settings;

        public MailKitService(IOptions<MailSettings> mailSettings)
        {
            _settings = mailSettings.Value;
        }

        public void SendNotification(string subject, string body, List<string> mailAddresses)
        {
            MimeMessage message = new MimeMessage();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            message.From.Add(new MailboxAddress("no-reply", _settings.User));
            mailAddresses.ForEach((mail) =>
            {
                message.To.Add(new MailboxAddress("", mail));
            });

            message.Subject = subject;

            message.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = body
            };

            var smtpClient = new MailKit.Net.Smtp.SmtpClient();

            smtpClient.Connect(_settings.Server, _settings.Port, MailKit.Security.SecureSocketOptions.None);
            smtpClient.Authenticate(_settings.User, _settings.Password);
            smtpClient.Send(message);
            smtpClient.Disconnect(true);
        }
    }
}

