namespace Manero.Components.Models
{
    public class OrderDetails
    {
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Total { get; set; }
    }
}
