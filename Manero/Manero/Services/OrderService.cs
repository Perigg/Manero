using Manero.Components.Models;

public class OrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> CreateOrderAsync(List<CartItem> cartItems)
    {
        var response = await _httpClient.PostAsJsonAsync("orders", cartItems);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();
        return result["orderId"];
    }

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

    public async Task<List<CartItem>> GetCartItemsAsync()
    {
        var apiUrl = "cart";
        return await _httpClient.GetFromJsonAsync<List<CartItem>>(apiUrl) ?? new List<CartItem>();
    }

    public async Task ClearCartAsync()
    {
        var apiUrl = "cart/clear";
        await _httpClient.PostAsync(apiUrl, null);
    }

    public async Task<int> TransferCartToOrderAsync()
    {
        // Step 1: Fetch cart items
        var cartItems = await GetCartItemsAsync();

        if (cartItems == null || cartItems.Count == 0)
        {
            throw new InvalidOperationException("Cart is empty.");
        }

        // Step 2: Create order
        var orderId = await CreateOrderAsync(cartItems);

        // Step 4: Clear cart
        await ClearCartAsync();

        return orderId;
    }
}