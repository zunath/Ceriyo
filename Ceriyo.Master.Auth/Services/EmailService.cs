using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ceriyo.Master.Auth.Services
{
    public class EmailService: IIdentityMessageService
    {
        private static async Task SendMessageAsync(IdentityMessage identityMessage)
        {
            string apiKey = ConfigurationManager.AppSettings["MailAPIKey"];
            var client = new SendGridClient(apiKey);
            var email = MailHelper.CreateSingleEmail(
                new EmailAddress("no-reply@ceriyo.com", "Ceriyo Game Engine"),
                new EmailAddress(identityMessage.Destination),
                identityMessage.Subject,
                identityMessage.Body,
                identityMessage.Body);

            await client.SendEmailAsync(email);
        }

        public Task SendAsync(IdentityMessage identityMessage)
        {
            return SendMessageAsync(identityMessage);
        }
    }
}