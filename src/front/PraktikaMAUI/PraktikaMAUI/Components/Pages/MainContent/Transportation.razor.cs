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
using System.ComponentModel;

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
        private List<SelectModels> VehiclesList = new List<SelectModels>();

        private List<SelectModels> Statuslist = new List<SelectModels>
        {
            new SelectModels {Value = 0 , Name = "Новый"},
            new SelectModels {Value = 1 , Name = "В пути"},
            new SelectModels {Value = 2, Name = "Приехал"},
        };
        private List<SelectModels> CargoList = new List<SelectModels>
        {
            new SelectModels {Value = 0 , Name = "Стандарт"},
            new SelectModels {Value = 1 , Name = "Опасный"},
            new SelectModels {Value = 2, Name = "Большой"},
            new SelectModels {Value = 3, Name = "Жидкий"},
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
                OrderNumber = TransportationModel.OrderNumber,
                Status = TransportationModel.Status,
                StartDate = TransportationModel.StartDate,
                EndDate = TransportationModel.EndDate,
                Road = TransportationModel.Road,
                TransportationStatus = TransportationModel.TransportationStatus,
                cargoType = TransportationModel.cargoType,
                VehicleId = TransportationModel.VehicleId,
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
            await GetVehiclesByCargo(model , null);
            model.PropertyChanged += CargoPropertyChanged;
            redactModel.PropertyChanged += CargoPropertyChanged;
            token = (await _localStorage.GetItemAsStringAsync("Token")).Trim('\"');
            await GetTransportations();
        }
        private async void CargoPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(model.cargoType))
            {
                await GetVehiclesByCargo(model,null);
            }
            if (e.PropertyName == nameof(redactModel.cargoType)) 
            {
                await GetVehiclesByCargo(null , redactModel);
            }
        }

        private async Task GetVehiclesByCargo(AddTransportation? addModel , TransportationModel redact) 
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7012");

                client.DefaultRequestHeaders.Add("Accept", "*/*");

                dynamic obj = addModel == null ? redact : addModel;
                HttpResponseMessage response = await client.GetAsync($"/api/Vehicle/by_cargo?CargoType={obj.cargoType}");


                if (response.IsSuccessStatusCode)
                {
                    VehiclesList.Clear();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var jsonDoc = JsonDocument.Parse(responseBody);


                    if (jsonDoc.RootElement.TryGetProperty("response", out JsonElement responseElement))
                    {
                        foreach (var item in responseElement.EnumerateArray())
                        {
                            if (item.ValueKind == JsonValueKind.Number && item.TryGetInt32(out int number))
                            {
                                VehiclesList.Add(new SelectModels()
                                {
                                    Value = number,
                                    Name = number.ToString()
                                });
                            }
                        }
                    }
                    StateHasChanged();
                }
                else
                {
                    string errorStr = await ResponseErrorCatcher.ErrorCatcher(response);
                    Error(errorStr);
                }
            }
        }
        private async Task HandleOk(MouseEventArgs e)
        {
            _submitting = true;
            if (model.StartDate == null || model.EndDate == null || model.OrderNumber == null || model.Road == null)
            {
                Error("не все поля заполненны");
                return;
            }
            var url = "https://localhost:7012/api/Transportation";

            var requestData = new
            {
                orderNumber = model.OrderNumber,
                vehicleId = model.vehicleId,
                startDate = model.StartDate,
                endDate = model.EndDate,
                road = model.Road,
                cargoType = model.cargoType
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
                orderNumber = redactModel.OrderNumber,
                startDate = redactModel.StartDate,
                endDate = redactModel.EndDate,
                road = redactModel.Road,
                cargoType = redactModel.cargoType,
                vehicleId = redactModel.VehicleId,
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
