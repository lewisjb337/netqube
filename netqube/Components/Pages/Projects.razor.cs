using Microsoft.AspNetCore.Components;

namespace netqube.Components.Pages;

public partial class Projects : ComponentBase
{
    private bool _isLoading = true;
    private string SearchText { get; set; } = string.Empty;
    
    protected override async Task OnInitializedAsync()
    {
        _isLoading = true;
        
        await LoadData();
        
        _isLoading = false;
    }

    private async Task LoadData()
    {
        
    }
    
    protected async Task Refresh()
    {
        await LoadData();

        StateHasChanged();
    }
}