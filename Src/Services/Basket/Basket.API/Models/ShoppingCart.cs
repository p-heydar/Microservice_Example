namespace Basket.API.Models;

public sealed class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice => CalculateTotalSum();
    
    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    // For Mapping
    public ShoppingCart()
    {
    }

    public decimal CalculateTotalSum()
    {
        return Items.Sum(x => x.Price * x.Quantity);
    }
}