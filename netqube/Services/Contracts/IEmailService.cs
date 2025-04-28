using netqube.Models;

namespace netqube.Services.Contracts;

public interface IEmailService
{
    Task<bool> SendEmailAsync(ContactModel contactModel);
}