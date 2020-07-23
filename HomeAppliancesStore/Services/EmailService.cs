using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Services
{
    public class EmailService
    {
        private readonly ILogger<EmailService> logger;

        public EmailService(ILogger<EmailService> logger)
        {
            this.logger = logger;
        }
        public void SendEmail(string recipient, string textMessage)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = false;
                message.From = new MailAddress("maxim.bondaruk2000@gmail.com", "5Element");
                message.To.Add(recipient);
                message.Subject = "Подтверждение заказа";
                message.Body = textMessage;

                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Credentials = new NetworkCredential("maxim.bondaruk2000@gmail.com", "37529913603434Vazubu22062000");
                    client.Port = 587;
                    client.EnableSsl = true;

                    client.Send(message);
                    logger.LogInformation("Сообщение успешно отправлено!");
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.GetBaseException().Message);
            }
        }
    }
}
