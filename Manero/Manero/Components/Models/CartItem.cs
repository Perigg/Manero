namespace Manero.Components.Models;

public class CartItem
{
    public int CartItemID { get; set; }
    public int ProductID { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Color { get; set; }
    public string? Size { get; set; }
    public int Quantity { get; set; }


}

