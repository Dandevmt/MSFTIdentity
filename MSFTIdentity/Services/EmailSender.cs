﻿using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSFTIdentity.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string smtpUrl;
        private readonly int smtpPort;
        private readonly string username;
        private readonly string password;

        public EmailSender(SmtpConfig config)
        {
            this.smtpUrl = config.Server;
            this.smtpPort = config.Port;
            this.username = config.Username;
            this.password = config.Password;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new EmailMessage()
            {
                To = email,
                Subject = subject,
                Body = htmlMessage,
                From = "noreply@ofbbutte.com"
            };
            return Send(message);
        }

        private async Task Send(EmailMessage message)
        {
            var mime = new MimeMessage();
            mime.To.Add(new MailboxAddress(message.To));
            mime.From.Add(new MailboxAddress(message.From));
            mime.Subject = message.Subject;
            mime.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Body };

            //Be careful that the SmtpClient class is the one from Mailkit not the framework!
            using (var emailClient = new SmtpClient())
            {
                //The last parameter here is to use SSL (Which you should!)
                await emailClient.ConnectAsync(smtpUrl, smtpPort, true);

                await emailClient.AuthenticateAsync(username, password);

                await emailClient.SendAsync(mime);

                await emailClient.DisconnectAsync(true);
            }

        }
    }
}
