using Manero.Components.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Manero.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method to fetch cart items
        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            var apiUrl = "api/GetCartItems?code=zgpZKFJ5E4w3k2G9xYQLZUPC6HrvvbSjReuYJrudkTkwAzFu5t6u8g==";
            return await _httpClient.GetFromJsonAsync<List<CartItem>>(apiUrl) ?? new List<CartItem>();
        }
    }
}