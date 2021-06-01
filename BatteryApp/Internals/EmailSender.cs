using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BatteryApp.Internals
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private string _host;
        private int _port;
        private bool _enableSSL;
        private string _userName;
        private string _password;

        // Get our parameterized configuration
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            GetSmtpSettings();
            GetUserName();
            GetPassword();
        }

        // Use our configuration to send the email by using SmtpClient
        public Task SendEmailAsync(string emailTo, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = _enableSSL
            };
            return client.SendMailAsync(
                new MailMessage(_userName, emailTo, subject, htmlMessage) { IsBodyHtml = true }
            );
        }

        private void GetSmtpSettings()
        {
            _host = _configuration["EmailSender:Host"];
            _port = _configuration.GetValue<int>("EmailSender:Port");
            _enableSSL = _configuration.GetValue<bool>("EmailSender:EnableSSL");
        }

        private void GetUserName()
        {
            _userName = _configuration["EmailProviders:GMail:UserName"];
        }

        private void GetPassword()
        {
            _password = _configuration["EmailProviders:GMail:Password"];
        }
    }
}
