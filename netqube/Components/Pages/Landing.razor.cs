using Microsoft.AspNetCore.Components;
using netqube.Models;
using netqube.Services.Contracts;

namespace netqube.Components.Pages;

public partial class Landing : ComponentBase
{
    [Inject]
    public IEmailService EmailService { get; set; } = null!;
    
    private ContactModel _contactModel = new ();
    private bool _isSuccess = true;
    private bool _showSuccess = false;

    private async Task HandleSubmit()
    {
        _isSuccess = true;
        var response = await EmailService.SendEmailAsync(contactModel: _contactModel);

        if (!response)
        {
            _isSuccess = false;
        }
        else
        {
            _contactModel = new ContactModel();
            _isSuccess = true;
            _showSuccess = true;
        }
    }
}