using ShoppingCart.Core.Helpers;
using System;

namespace ShoppingCart.Core
{
    public class Coupon : Discount
    {
        public Coupon(double discountAmount, int minimumAmountToApply, DiscountType discountType)
            : base(discountAmount, minimumAmountToApply, discountType)
        {
        }

        public override double GetDiscountPrice(double totalPrice)
        {
            if (DiscountType == DiscountType.Rate)
            {
                return totalPrice * (DiscountAmount / 100);
            }
            else if (DiscountType == DiscountType.Amount)
            {
                return DiscountAmount;
            }

            throw new InvalidOperationException();
        }
    }
}
