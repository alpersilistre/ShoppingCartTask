namespace ShoppingCart.Core.Coupons
{
    class RateCoupon : Coupon
    {
        public RateCoupon(double discountAmount, int minimumAmountToApply)
            : base(discountAmount, Helpers.DiscountType.Rate, minimumAmountToApply)
        {
        }

        public override double GetDiscountPrice(double totalPrice)
        {
            return totalPrice * (DiscountAmount / 100);
        }
    }
}
