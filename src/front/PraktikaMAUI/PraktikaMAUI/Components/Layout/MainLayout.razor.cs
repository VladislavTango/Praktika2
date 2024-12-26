namespace PraktikaMAUI.Components.Layout
{
    public partial class MainLayout
    {
        private string value = null;

        protected override async Task OnInitializedAsync()
        {
            value = await _localStorage.GetItemAsStringAsync("Token");
        }
    }
}
