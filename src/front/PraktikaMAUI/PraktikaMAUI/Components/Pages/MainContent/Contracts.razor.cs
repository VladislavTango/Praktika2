using AntDesign;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Text.Json;
using PraktikaMAUI.Models.ModalModels;
using System.Text;
using PraktikaMAUI.Models;
using PraktikaMAUI.Components.Interface;

namespace PraktikaMAUI.Components.Pages.MainContent
{
    public partial class Contracts
    {
        private bool _visible = false;
        private bool RedactVisible = false;

        private bool _submitting = false;
        private bool RedactSubmitting = false;

        private int PageIndex = 1;

        private List<ContractModel> ContractList = new List<ContractModel>();

        private Form<ContractModel> redactForm;
        private Form<AddContract> _form;

        private ContractModel redactModel = new ContractModel();
        private AddContract model = new AddContract();

        private List<SelectModels> list = new List<SelectModels>
        {
            new SelectModels {Value = 0 , Name = "Разовый"},
            new SelectModels {Value = 1 , Name = "Долгосрочный"},
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


        private async Task GetContracts()
        {
            var url = $"https://localhost:7012/api/Contract?ClientId=1&PageLength=5&PageNumber={PageIndex}";

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                ContractList = JsonSerializer.Deserialize<List<ContractModel>>(
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
        private async Task DeleteContract(int id)
        {
            var url = "https://localhost:7012/api/Contract";
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
                await GetContracts();
            }
            else
            {
                string error = await ResponseErrorCatcher.ErrorCatcher(response);
                Error(error);
            }
        }
        private async Task UpdateContract(ContractModel contractModel)
        {
            redactModel = new()
            {
                Id = contractModel.Id,
                Status = contractModel.Status,
                ContractTerms = contractModel.ContractTerms,
                ContractType = contractModel.ContractType
            };
            RedactVisible = true;
        }

        protected void OnChange(PaginationEventArgs args)
        {
            PageIndex = args.Page;
            StateHasChanged();
            GetContracts();
        }
        protected override async Task OnInitializedAsync()
        {
            token = (await _localStorage.GetItemAsStringAsync("Token")).Trim('\"');
            await GetContracts();
        }

        private async Task HandleOk(MouseEventArgs e)
        {
            _submitting = true;
            if (model.contractTerms == null)
            {
                Error("не все поля заполненны");
                return;
            }
            var url = "https://localhost:7012/api/Contract";

            var requestData = new
            {
                contractTerms = model.contractTerms,
                contractType = model.contractType,
                contractDate = model.StartDate,
                expirationDate = model.EndDate
            };

            var jsonContent = JsonSerializer.Serialize(requestData);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization =
   new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                await GetContracts();
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
            var url = "https://localhost:7012/api/Contract";

            var ContractUpdate = new
            {
                Id = redactModel.Id,
                OrderId = redactModel.OrderId,
                Status = redactModel.Status,
                ContractTerms = redactModel.ContractTerms
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(ContractUpdate),
                Encoding.UTF8,
                "application/json"
            );

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization =
   new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.PutAsync(url, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                await GetContracts();
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
