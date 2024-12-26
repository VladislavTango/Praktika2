using AntDesign;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Text.Json;
using PraktikaMAUI.Models.ModalModels;
using System.Text;
using PraktikaMAUI.Models;
using PraktikaMAUI.Components.Interface;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using PraktikaMAUI.Models.Enum;

namespace PraktikaMAUI.Components.Pages.MainContent
{
    public partial class Transportation
    {
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

        private bool _visible = false;
        private bool RedactVisible = false;
        private bool MapVisible = false;
        private string Route { get; set; }

        private bool _submitting = false;
        private bool RedactSubmitting = false;

        private int PageIndex = 1;

        private List<TransportationModel> TranspotationList = new List<TransportationModel>();

        private Form<TransportationModel> redactForm;
        private Form<AddTransportation> _form;

        private TransportationModel redactModel = new TransportationModel();
        private AddTransportation model = new AddTransportation();

        private List<SelectModels> list = new List<SelectModels>
        {
            new SelectModels {Value = 0 , Name = "Новый"},
            new SelectModels {Value = 1 , Name = "В пути"},
            new SelectModels {Value = 2, Name = "Приехал"},
        };

        private string token {  get; set; }

        private DateTime?[] GetRange<T>(T model) where T : IHasDateRange
        {
            return new DateTime?[] { model.StartDate, model.EndDate };
        }

        private void SetRange<T>(T model, DateTime?[] value) where T : IHasDateRange
        {
            if (value is { Length: 2 })
            {
                model.StartDate = value[0];
                model.EndDate = value[1];
            }
        }

        private void ShowModal()
        {
            _visible = true;
        }

        private async Task GetTransportations()
        {
            var url = $"https://localhost:7012/api/Transportation?ClientId=1&PageLength=5&PageNumber={PageIndex}";

            using var httpClient = new HttpClient();

            

            httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                TranspotationList = JsonSerializer.Deserialize<List<TransportationModel>>(
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
        private async Task DeleteTransportation(int id)
        {
            var url = "https://localhost:7012/api/Transportation";
            var content = new { id = id };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(content),
                Encoding.UTF8,
                "application/json");

            using var httpClient = new HttpClient();


            httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url),
                Content = jsonContent
            };

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                await GetTransportations();
            }
            else
            {
                string error = await ResponseErrorCatcher.ErrorCatcher(response);
                Error(error);
            }
        }
        private async Task UpdateTransportation(TransportationModel TransportationModel)
        {
            redactModel = new()
            {
                Id = TransportationModel.Id,
                OrderId = TransportationModel.OrderId,
                Status = TransportationModel.Status,
                StartDate = TransportationModel.StartDate,
                EndDate = TransportationModel.EndDate,
                Road = TransportationModel.Road,
                TransportationStatus = TransportationModel.TransportationStatus,
            };
            RedactVisible = true;
        }

        protected void OnChange(PaginationEventArgs args)
        {
            PageIndex = args.Page;
            StateHasChanged();
            GetTransportations();
        }
        protected override async Task OnInitializedAsync()
        {
            token = (await _localStorage.GetItemAsStringAsync("Token")).Trim('\"');
            await GetTransportations();
        }

        private async Task HandleOk(MouseEventArgs e)
        {
            _submitting = true;
            if (model.StartDate == null || model.EndDate == null || model.OrderId == null || model.Road == null)
            {
                Error("не все поля заполненны");
                return;
            }
            var url = "https://localhost:7012/api/Transportation";

            var requestData = new
            {
                orderId = model.OrderId,
                startDate = model.StartDate,
                endDate = model.EndDate,
                road = model.Road,
            };

            var jsonContent = JsonSerializer.Serialize(requestData);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                await GetTransportations();
            }
            else
            {
                string error = await ResponseErrorCatcher.ErrorCatcher(response);
                Error(error);
            }
            _form.Submit();
        }

        private async Task RedactOk(MouseEventArgs e)
        {
            RedactSubmitting = true;
            var url = "https://localhost:7012/api/Transportation";

            var TransportationUpdate = new
            {
                id = redactModel.Id,
                Status = redactModel.Status,
                OrderId = redactModel.OrderId,
                startDate = redactModel.StartDate,
                endDate = redactModel.EndDate,
                road = redactModel.Road,
                transportationStatus = redactModel.TransportationStatus,
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(TransportationUpdate),
                Encoding.UTF8,
                "application/json"
            );

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PutAsync(url, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                await GetTransportations();
            }
            else
            {
                string error = await ResponseErrorCatcher.ErrorCatcher(response);
                Error(error);
            }
            redactForm.Submit();
        }

        private void HandleCancel(MouseEventArgs e)
        {
            _visible = false;
        }

        private void RedactCansel(MouseEventArgs e)
        {
            RedactVisible = false;
        }

        private void RedactOnFinish(EditContext editContext)
        {
            RedactSubmitting = false;
            RedactVisible = false;
            redactForm.Reset();
        }

        private void OnFinish(EditContext editContext)
        {
            _submitting = false;
            _visible = false;
            _form.Reset();
        }

        private async Task Error(string ErrorMessage)
        {
            await _message.Error(ErrorMessage);
        }
    }
}
