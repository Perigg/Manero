using Manero.Components.Models;

namespace Manero.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(List<CartItem> cartItems);
        Task<List<OrderItem>> GetOrderItemsAsync(int orderId);
        Task UpdateOrderItemAsync(int orderItemId, OrderItem item);
        Task RemoveOrderItemAsync(int orderItemId);
        Task<List<CartItem>> GetCartItemsAsync();
        Task ClearCartAsync();
        Task<int> TransferCartToOrderAsync();
    }
}