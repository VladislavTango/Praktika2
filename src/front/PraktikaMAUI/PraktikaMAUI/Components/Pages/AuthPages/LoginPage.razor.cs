using AntDesign;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace PraktikaMAUI.Components.Pages.AuthPages
{
    public partial class LoginPage
    {
        private string UserName { get; set; } = string.Empty;
        private string Password { get; set; } = string.Empty;
        private string UserEmail { get; set; } = string.Empty;

        private async Task EnterNoIconLoading()
        {
            if (string.IsNullOrEmpty(UserName) ||
            string.IsNullOrEmpty(UserEmail) ||
            string.IsNullOrEmpty(Password))
            {
                Error("Нужно заполнить все поля");
                return;
            }
            var url = "https://localhost:7012/api/Client/enter";

            var requestData = new
            {
                name = UserName,
                email = UserEmail,
                password = Password,
            };

            var jsonContent = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var document = JsonDocument.Parse(responseBody);
                    string token = document.RootElement.GetProperty("response").GetString();
                    await _localStoragee.SetItemAsync("Token", token);
                    NavigationManager.NavigateTo("/profile", forceLoad: true);
                }
                else
                {
                    string ErrorStr = await ResponseErrorCatcher.ErrorCatcher(response);
                    Error(ErrorStr);
                }
            }
        }

        private async Task Error(string ErrorMessage)
        {
            await _message.Error(ErrorMessage);
        }


    }
}
