using Manero.Components.Models;

namespace Manero.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Methods for Cart
        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            var apiUrl = "api/GetCartItems?code=zgpZKFJ5E4w3k2G9xYQLZUPC6HrvvbSjReuYJrudkTkwAzFu5t6u8g==";
            return await _httpClient.GetFromJsonAsync<List<CartItem>>(apiUrl) ?? new List<CartItem>();
        }
    }
}
