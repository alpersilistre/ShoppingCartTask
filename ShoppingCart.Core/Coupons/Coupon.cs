using ShoppingCart.Core.Helpers;
using ShoppingCart.Core.Interfaces;

namespace ShoppingCart.Core.Coupons
{
    public abstract class Coupon : IDeductible
    {
        public double DiscountAmount { get; }
        public DiscountType DiscountType { get; }
        public double MinimumAmountToApply { get; }

        public Coupon(double discountAmount, DiscountType discountType, double minimumAmountToApply)
        {
            DiscountAmount = discountAmount;
            DiscountType = discountType;
            MinimumAmountToApply = minimumAmountToApply;
        }

        public abstract double GetDiscountPrice(double totalPrice);
    }
}
