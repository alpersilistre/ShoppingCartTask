namespace ShoppingCart.Core.Interfaces
{
    public interface IDeliveryCostCalculator
    {
        double CostPerDelivery { get; }
        double CostPerProduct { get; }
        double CalculateFor(IShoppingCart shoppingCart);
    }
}
