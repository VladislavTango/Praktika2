using AntDesign;
using PraktikaMAUI.Models;
using PraktikaMAUI.Models.ModalModels;
using System.Text;
using System.Text.Json;

namespace PraktikaMAUI.Components.Pages.MainContent
{
    public partial class Invoices
    {
        private bool _visible = false;

        private int PageIndex = 1;

        private List<IvoiceShowModel> InvoiceList = new List<IvoiceShowModel>();

        private WriteInvoice Info { get; set; }
        private string token { get; set; }

        private async Task GetInvoices()
        {
            var url = $"https://localhost:7012/api/Invoice?ClientId=1&PageLength=5&PageNumber={PageIndex}";

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                InvoiceList = JsonSerializer.Deserialize<List<IvoiceShowModel>>(
                   JsonDocument.Parse(responseContent).RootElement.GetProperty("response").GetRawText(),
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                string error = await ResponseErrorCatcher.ErrorCatcher(response);
                Error(error);
            }
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            token = (await _localStorage.GetItemAsStringAsync("Token")).Trim('\"');
            await GetInvoices();
        }
        private async Task Error(string ErrorMessage)
        {
            await _message.Error(ErrorMessage);
        }
        private async Task GetInfo(IvoiceShowModel context)
        {
            string url = $"https://localhost:7012/api/Invoice/info?Id={context.Id}&TransportationId={context.TransportationId}";
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                Info = JsonSerializer.Deserialize<WriteInvoice>(
                    JsonDocument.Parse(responseContent).RootElement.GetProperty("response").GetRawText(),
                    options);

                StateHasChanged();
            }
            else
            {
                string error = await ResponseErrorCatcher.ErrorCatcher(response);
                Error(error);
            }
            _visible = true;
        }

        private async Task UpdateStatus(IvoiceShowModel context)
        {
            var url = "https://localhost:7012/api/Invoice";

            var data = new
            {
                id = context.Id,
                status = true
            };

            var jsonContent = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                GetInvoices();
            }
            else
            {
                string error = await ResponseErrorCatcher.ErrorCatcher(response);
                Error(error);
            }

        }
        protected void OnChange(PaginationEventArgs args)
        {
            PageIndex = args.Page;
            StateHasChanged();
            GetInvoices();
        }
    }
}
