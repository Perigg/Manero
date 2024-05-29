using Manero.Components.Models;

namespace Manero.Services
{
    public class PhotoService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<string> GetUserPhotoUrlAsync(int userId)
        {
            try
            {
                string functionUrl = "https://photo-provider.azurewebsites.net/api/GetPhoto";
                string functionKey = "vU6rzzJ1OLqRenYbi4q9jLg4T11t-Qc-9rUGrTcjKkdxAzFug4h8yw==";

                _httpClient.DefaultRequestHeaders.Add("x-functions-key", functionKey);

                var result = await _httpClient.GetFromJsonAsync<GetImageModel>($"{functionUrl}?userId={userId}");
                if (result == null || string.IsNullOrEmpty(result.FileUrl))
                {
                    return "error.jpg";
                }
                else
                {
                    return result.FileUrl;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return "error.jpg";
            }
        }
    }
}
