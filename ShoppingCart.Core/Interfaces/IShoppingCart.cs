using ShoppingCart.Core.Coupons;
using ShoppingCart.Core.Discounts;
using System.Collections.Generic;

namespace ShoppingCart.Core.Interfaces
{
    public interface IShoppingCart
    {
        double TotalItemPrice { get; }
        int NumberOfDeliveries { get; }
        int NumberOfProducts { get; }
        void AddItem(Product product, int quantity = 1);
        void RemoveItem(Product product, int quantity = 1);
        void ApplyCoupon(Coupon coupon);
        void ApplyDiscounts(List<Discount> discounts);
        double GetTotalAmountAfterDiscounts();
        double GetDeliveryCost();
        string Print();
    }
}
