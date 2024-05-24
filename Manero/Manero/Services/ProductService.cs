//using Manero.Components.Models;


//namespace Manero.Services
//{
//    public class ProductService
//    {
//        private readonly HttpClient _httpClient;

//        public ProductService(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }


//        public async Task<List<CartItem>> GetCartItemsAsync()
//        {
//            var apiUrl = "api/GetCartItems?code=zgpZKFJ5E4w3k2G9xYQLZUPC6HrvvbSjReuYJrudkTkwAzFu5t6u8g==";
//            return await _httpClient.GetFromJsonAsync<List<CartItem>>(apiUrl) ?? new List<CartItem>();
//        }
//    }
//}
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

        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            var apiUrl = "api/GetCartItems?code=zgpZKFJ5E4w3k2G9xYQLZUPC6HrvvbSjReuYJrudkTkwAzFu5t6u8g==";
            var cartItems = await _httpClient.GetFromJsonAsync<List<CartItem>>(apiUrl);
            if (cartItems == null || cartItems.Count == 0)
            {
                throw new InvalidOperationException("Cart is empty or could not fetch cart items.");
            }

            foreach (var item in cartItems)
            {
                Console.WriteLine($"Fetched cart item: {item.Name}, Quantity: {item.Quantity}, Price: {item.Price}");
            }

            return cartItems ?? new List<CartItem>();
        }
    }
}