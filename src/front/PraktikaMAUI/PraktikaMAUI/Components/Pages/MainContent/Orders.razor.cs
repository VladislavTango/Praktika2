using AntDesign;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Text.Json;
using PraktikaMAUI.Models.ModalModels;
using System.Text;
using PraktikaMAUI.Models;

namespace PraktikaMAUI.Components.Pages.MainContent
{
    public partial class Orders
    {
        private bool _visible = false;
        private bool RedactVisible = false;

        private bool _submitting = false;
        private bool RedactSubmitting = false;

        private int PageIndex = 1;

        private List<OrderModel> OrderList = new List<OrderModel>();

        private Form<OrderModel> redactForm;
        private Form<AddOrder> _form;

        private OrderModel redactModel = new OrderModel();
        private AddOrder model = new AddOrder();
        private string token {  get; set; }

        private void ShowModal()
        {
            _visible = true;
        }

        private async Task GetOrders()
        {
            var url = $"https://localhost:7012/api/Order?ClientId=1&PageLength=5&PageNumber={PageIndex}";

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                 OrderList = JsonSerializer.Deserialize<List<OrderModel>>(
                    JsonDocument.Parse(responseContent).RootElement.GetProperty("response").GetRawText(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else 
            {
                string error= await ResponseErrorCatcher.ErrorCatcher(response);
                Error(error);
            }
            StateHasChanged();
        }
        private async Task DeleteOrder(int id)
        {
            var url = "https://localhost:7012/api/Order";
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
                await GetOrders();
            }
            else
            {
                string error = await ResponseErrorCatcher.ErrorCatcher(response);
                Error(error);
            }
        }
        private async Task UpdateOrder(OrderModel order)
        {
            redactModel = new()
            {
                id = order.id,
                cargoDescription = order.cargoDescription,
                orderName = order.orderName,
                Status = order.Status,
            };
            RedactVisible = true;
        }
            
        protected void OnChange(PaginationEventArgs args)
        {
            PageIndex = args.Page;
            StateHasChanged();
            GetOrders();
        }
        protected override async Task OnInitializedAsync()
        {
            token = (await _localStorage.GetItemAsStringAsync("Token")).Trim('\"');
            await GetOrders();
        }

        private async Task HandleOk(MouseEventArgs e)
        {
            _submitting = true;
            if (model.CargoDescription == null || model.OrderName == null)
            {
                Error("не все поля заполненны");
                return;
            }
            var url = "https://localhost:7012/api/Order";

            var requestData = new
            {
                clientId = 1,
                orderName = model.OrderName,
                cargoDescription = model.CargoDescription
            };

            var jsonContent = JsonSerializer.Serialize(requestData);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
               await GetOrders();
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
            var url = "https://localhost:7012/api/Order";

            var orderUpdate = new
            {
                id = redactModel.id,
                orderName = redactModel.orderName,
                status = redactModel.Status,
                cargoDescription = redactModel.cargoDescription
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(orderUpdate),
                Encoding.UTF8,
                "application/json"
            );

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PutAsync(url, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                await GetOrders();
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
            _visible = false;
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
