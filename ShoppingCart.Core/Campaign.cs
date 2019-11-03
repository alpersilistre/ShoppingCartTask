using ShoppingCart.Core.Helpers;
using System;

namespace ShoppingCart.Core
{
    public class Campaign : Discount
    {
        public Category Category { get; }

        public Campaign(double discountAmount, int minimumAmountToApply, DiscountType discountType, Category category)
            : base(discountAmount, minimumAmountToApply, discountType)
        {
            Category = category;
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
