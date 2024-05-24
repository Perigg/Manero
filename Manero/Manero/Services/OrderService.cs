using Manero.Components.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manero.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _orderHttpClient;
        private readonly HttpClient _cartHttpClient;
        private readonly ILogger<OrderService> _logger;
        private readonly string _connectionString;

        private const string CartApiKey = "zgpZKFJ5E4w3k2G9xYQLZUPC6HrvvbSjReuYJrudkTkwAzFu5t6u8g==";
        private const string OrderApiKey = "yUhK1HJBedut_HuQTN6u_U4KMzen9Bx5R6i-7g7wRhrqAzFuZoJ-Ug==";

        public OrderService(HttpClient cartHttpClient, HttpClient orderHttpClient, IConfiguration configuration, ILogger<OrderService> logger)
        {
            _cartHttpClient = cartHttpClient;
            _orderHttpClient = orderHttpClient;
            _logger = logger;
            _connectionString = configuration["SqlConnectionString"] ?? throw new ArgumentNullException("Connection string not found");
        }

        public async Task<int> CreateOrderAsync(List<CartItem> cartItems)
        {
            int orderId;  // Deklarera orderId här

            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using var transaction = conn.BeginTransaction();
                try
                {
                    // Insert into Orders table
                    var insertOrderQuery = "INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) OUTPUT INSERTED.OrderID VALUES (@CustomerID, @OrderDate, @TotalAmount)";
                    using (var cmd = new SqlCommand(insertOrderQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", 1); // Replace with actual CustomerID
                        cmd.Parameters.AddWithValue("@OrderDate", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@TotalAmount", cartItems.Sum(item => item.Price * item.Quantity));

                        orderId = (int)await cmd.ExecuteScalarAsync();
                    }

                    // Insert into OrderItems table
                    var insertOrderItemQuery = "INSERT INTO OrderItems (OrderId, ProductId, Name, Price, Color, Size, Quantity) VALUES (@OrderId, @ProductId, @Name, @Price, @Color, @Size, @Quantity)";
                    foreach (var item in cartItems)
                    {
                        using var cmd = new SqlCommand(insertOrderItemQuery, conn, transaction);
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        cmd.Parameters.AddWithValue("@ProductId", item.ProductID);
                        cmd.Parameters.AddWithValue("@Name", item.Name ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Price", item.Price);
                        cmd.Parameters.AddWithValue("@Color", item.Color ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Size", item.Size ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Quantity", item.Quantity);

                        await cmd.ExecuteNonQueryAsync();
                    }

                    // Commit transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex, "Error creating order");
                    throw;
                }
            }

            return orderId;
        }

        public async Task<List<OrderItem>> GetOrderItemsAsync(int orderId)
        {
            var apiUrl = $"https://order-provider.azurewebsites.net/api/orders/{orderId}/items?code={OrderApiKey}";
            _logger.LogInformation($"Fetching order items from URL: {apiUrl}");
            return await _orderHttpClient.GetFromJsonAsync<List<OrderItem>>(apiUrl) ?? new List<OrderItem>();
        }

        public async Task UpdateOrderItemAsync(OrderItem item)
        {
            var apiUrl = $"https://order-provider.azurewebsites.net/api/orders/items/{item.OrderItemId}?code={OrderApiKey}";
            await _orderHttpClient.PutAsJsonAsync(apiUrl, item);
        }

        public async Task RemoveOrderItemAsync(int orderItemId)
        {
            var apiUrl = $"https://order-provider.azurewebsites.net/api/orders/items/{orderItemId}?code={OrderApiKey}";
            await _orderHttpClient.DeleteAsync(apiUrl);
        }

        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            var apiUrl = $"https://cart-product-provider.azurewebsites.net/api/GetCartItems?code={CartApiKey}";
            _logger.LogInformation($"Fetching cart items from URL: {apiUrl}");
            var response = await _cartHttpClient.GetAsync(apiUrl);
            _logger.LogInformation($"Response status code: {response.StatusCode}");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<CartItem>>() ?? new List<CartItem>();
        }

        public async Task ClearCartAsync()
        {
            var apiUrl = $"https://cart-product-provider.azurewebsites.net/api/cart/clear?code={CartApiKey}";
            await _cartHttpClient.PostAsync(apiUrl, null);
        }

        public async Task<int> TransferCartToOrderAsync()
        {
            var cartItems = await GetCartItemsAsync();

            if (cartItems == null || cartItems.Count == 0)
            {
                throw new InvalidOperationException("Cart is empty.");
            }

            var orderId = await CreateOrderAsync(cartItems);
            // await ClearCartAsync();

            return orderId;
        }
    }
}