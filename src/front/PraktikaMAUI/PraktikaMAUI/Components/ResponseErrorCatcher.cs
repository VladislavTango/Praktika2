using System.Text.Json;

namespace PraktikaMAUI.Components
{
    public static class ResponseErrorCatcher
    {
        public static async Task<string>  ErrorCatcher(HttpResponseMessage response) 
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(errorResponse);

            document.RootElement.TryGetProperty("Response", out JsonElement responseElement);
            responseElement.TryGetProperty("ErrorMessage", out JsonElement errorStrElement);

            string errorStr = errorStrElement.GetString();

            return errorStr;
        }
    }
}
