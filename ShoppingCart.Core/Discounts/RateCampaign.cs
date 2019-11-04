using Ardalis.GuardClauses;
using ShoppingCart.Core.Helpers;

namespace ShoppingCart.Core.Discounts
{
    public class RateCampaign : Discount
    {
        public RateCampaign(double discountAmount, int minimumQuantityToApply, Category category)
            : base(discountAmount, DiscountType.Rate, minimumQuantityToApply, category)
        {
        }

        public override double GetDiscountPrice(double totalPrice)
        {
            Guard.Against.Null(DiscountType, nameof(DiscountType));

            return totalPrice * (DiscountAmount / 100);
        }
    }
}
