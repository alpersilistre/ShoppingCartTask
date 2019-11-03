namespace ShoppingCart.Core.Interfaces
{
    public interface IDeliveryCostCalculator
    {
        double CalculateFor(IShoppingCart shoppingCart);
    }
}
