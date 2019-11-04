using ShoppingCart.Core.Helpers;

namespace ShoppingCart.Core.Coupons
{
    public class AmountCoupon : Coupon
    {
        public AmountCoupon(double discountAmount, int minimumAmountToApply)
            : base(discountAmount, DiscountType.Amount, minimumAmountToApply)
        {
        }

        public override double GetDiscountPrice(double totalPrice)
        {            
            return DiscountAmount;
        }
    }
}
