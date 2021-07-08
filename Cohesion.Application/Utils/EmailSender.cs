using Microsoft.Extensions.Configuration;
using System;
using System.Net.Mail;


namespace Cohesion.Application.Utils
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        public EmailSender(IConfiguration configuration)
        {
            _config = configuration;
        }
                
        public void SendMail(string serviceRequestId)
        {
            SmtpClient smtpClient = new SmtpClient(_config.GetSection("MailConfig")["SmtpService"], Convert.ToInt32(_config.GetSection("MailConfig")["Port"]));

            smtpClient.Credentials = new System.Net.NetworkCredential(_config.GetSection("MailConfig")["User"], _config.GetSection("MailConfig")["Password"]);

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(_config.GetSection("MailConfig")["From"], "gmail.com");
            mail.To.Add(new MailAddress(_config.GetSection("MailConfig")["To"]));
            mail.Subject = _config.GetSection("MailConfig")["Title"];
            mail.Body = _config.GetSection("MailConfig")["Body1"] + serviceRequestId + _config.GetSection("MailConfig")["Body2"];

            smtpClient.Send(mail);            
        }
    }
}
