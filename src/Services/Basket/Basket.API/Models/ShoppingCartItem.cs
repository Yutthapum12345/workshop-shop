namespace Basket.API.Models;

public class ShoppingCartItem
{

    public Guid ProductID {get;set;}
    public int Quantity {get;set;}

    public string Color {get;set;}


    public decimal Price {get;set;}

    

    public string ProductName {get;set;} = default!;

    

}
