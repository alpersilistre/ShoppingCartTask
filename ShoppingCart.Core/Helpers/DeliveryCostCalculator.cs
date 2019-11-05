using Ardalis.GuardClauses;
using ShoppingCart.Core.Interfaces;

namespace ShoppingCart.Core.Helpers
{
    public class DeliveryCostCalculator : IDeliveryCostCalculator
    {
        public double CostPerDelivery { get; }

        public double CostPerProduct { get; }

        private readonly double _fixedCost = 2.99;

        public DeliveryCostCalculator(double costPerDelivery, double costPerProduct)
        {
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
        }

        public DeliveryCostCalculator(double costPerDelivery, double costPerProduct, double fixedCost)
        {
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
            _fixedCost = fixedCost;
        }

        public double CalculateFor(IShoppingCart shoppingCart)
        {
            Guard.Against.Null(shoppingCart, nameof(shoppingCart));

            if (shoppingCart.NumberOfDeliveries == 0 && shoppingCart.NumberOfProducts == 0)
            {
                return 0;
            }

            return (CostPerDelivery * shoppingCart.NumberOfDeliveries) + (CostPerProduct * shoppingCart.NumberOfProducts)
                + _fixedCost;
        }
    }
}
