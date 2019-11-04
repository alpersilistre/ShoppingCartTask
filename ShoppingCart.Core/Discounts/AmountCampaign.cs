using ShoppingCart.Core.Helpers;

namespace ShoppingCart.Core.Discounts
{
    public class AmountCampaign : Discount
    {
        public AmountCampaign(double discountAmount, int minimumQuantityToApply, Category category)
            : base(discountAmount, DiscountType.Amount, minimumQuantityToApply, category)
        {
        }

        public override double GetDiscountPrice(double totalPrice)
        {
            return DiscountAmount;
        }
    }
}
