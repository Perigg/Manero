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

        
        // Methods for Order
        public async Task<List<OrderItem>> GetOrderItemsAsync(int orderId)
        {
            var apiUrl = $"orders/{orderId}/items";
            return await _httpClient.GetFromJsonAsync<List<OrderItem>>(apiUrl) ?? new List<OrderItem>();
        }

        public async Task UpdateOrderItemAsync(OrderItem item)
        {
            var apiUrl = $"orders/items/{item.OrderItemId}";
            await _httpClient.PutAsJsonAsync(apiUrl, item);
        }

        public async Task RemoveOrderItemAsync(int orderItemId)
        {
            var apiUrl = $"orders/items/{orderItemId}";
            await _httpClient.DeleteAsync(apiUrl);
        }
    }
}
