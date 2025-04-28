using netqube.Models;
using netqube.Services.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace netqube.Services;

public class EmailService(IConfiguration configuration) : IEmailService
{
    public async Task<bool> SendEmailAsync(ContactModel contactModel)
    {
        var apiKey = configuration["SendGrid:Key"];
        var client = new SendGridClient(apiKey);
        
        var from = new EmailAddress("[ADD-EMAIL-HERE]", contactModel.Name);
        const string subject = "Contact Form - Message";
        var to = new EmailAddress("[ADD-EMAIL-HERE]", "[ADD-TITLE]");
        var plainTextContent = contactModel.Message;
        var htmlContent = "<div>"+contactModel.Message+" from "+contactModel.Email+"</div>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        
        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }
}