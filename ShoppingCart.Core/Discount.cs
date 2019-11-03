using ShoppingCart.Core.Helpers;

namespace ShoppingCart.Core
{
    public abstract class Discount
    {
        public double DiscountAmount { get; }
        public double MinimumAmountToApply { get; private set; }
        public DiscountType DiscountType { get; }

        public Discount(double discountAmount, double minimumAmountToApply, DiscountType discountType)
        {
            DiscountAmount = discountAmount;
            MinimumAmountToApply = minimumAmountToApply;
            DiscountType = discountType;
        }

        public abstract double GetDiscountPrice(double totalPrice);
    }
}
