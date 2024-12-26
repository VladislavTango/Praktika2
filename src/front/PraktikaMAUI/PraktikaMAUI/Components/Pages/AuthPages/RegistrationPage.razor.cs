using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace PraktikaMAUI.Components.Pages.AuthPages
{
    public partial class RegistrationPage
    {

        private string UserName { get; set; } = string.Empty;
        private string Fpassword { get; set; } = string.Empty;
        private string Spassword { get; set; } = string.Empty;
        private string UserContacts { get; set; } = string.Empty;
        private string UserEmail { get; set; } = string.Empty;
        private string Code { get; set; } = string.Empty;


        string SendCodeUrl = "https://localhost:7012/api/Client/confirm code";

        string ClientAddUrl = "https://localhost:7012/api/Client/add";


        private async Task EnterNoIconLoading()
        {
            if (string.IsNullOrEmpty(UserName) ||
            string.IsNullOrEmpty(Fpassword) ||
            string.IsNullOrEmpty(Spassword) ||
            string.IsNullOrEmpty(UserContacts) ||
            string.IsNullOrEmpty(UserEmail) ||
            string.IsNullOrEmpty(Code))
            {
                Error("Нужно заполнить все поля");
                return;
            }

            if (Fpassword != Spassword)
            {
                Error("Пароли не совпадают");
                return;
            }

            var requestData = new
            {
                name = UserName,
                email = UserEmail,
                password = Fpassword,
                code = Code,
                contacts = UserContacts
            };

            var jsonContent = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {

                var response = await client.PostAsync(ClientAddUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var document = JsonDocument.Parse(responseBody);
                    string token = document.RootElement.GetProperty("response").GetString();
                    await _localStorage.SetItemAsync("Token", token);
                    NavigationManager.NavigateTo("/profile", forceLoad: true);
                }
                else
                {
                    string ErrorStr = await ResponseErrorCatcher.ErrorCatcher(response);
                    Error(ErrorStr);
                }
            }
        }
        private async Task SendEmailCode()
        {
            if (string.IsNullOrEmpty(UserEmail))
            {
                Error("Поле Email не заполненно");
                return;
            }
            if (string.IsNullOrEmpty(UserName))
            {
                Error("Поле UserName не заполненно");
                return;
            }
            var requestData = new
            {
                email = UserEmail,
                name = UserName
            };
            var jsonContent = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(SendCodeUrl, content);

                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
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
