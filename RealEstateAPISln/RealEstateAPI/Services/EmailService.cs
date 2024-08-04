using Azure.Communication.Email;
using Microsoft.Extensions.Configuration;
using RealEstateAPI.Migrations;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail.Model;
using System.Net.Mail;
using System.Threading.Tasks;
using Azure;
namespace RealEstateAPI.Services
{


    public class EmailService
    {
        private readonly EmailClient emailClient;

        public EmailService(EmailClient configuration)
        {
            emailClient = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {

            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                    Azure.WaitUntil.Started,
                    senderAddress: "DoNotReply@4196b2a0-7f2e-4153-9661-0c6bcfa9848a.azurecomm.net",
                    recipientAddress:  toEmail,
                    subject: subject,
                    htmlContent: htmlContent);



        }
    }

}
