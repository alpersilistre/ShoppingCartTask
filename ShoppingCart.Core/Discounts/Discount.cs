using ShoppingCart.Core.Helpers;
using ShoppingCart.Core.Interfaces;

namespace ShoppingCart.Core.Discounts
{
    public abstract class Discount : IDeductible
    {
        public double DiscountAmount { get; }
        public DiscountType DiscountType { get; }
        public int MinimumQuantityToApply { get; }
        public Category Category { get; }

        public Discount(double discountAmount, DiscountType discountType, int minimumQuantityToApply)
        {
            DiscountAmount = discountAmount;
            DiscountType = discountType;
            MinimumQuantityToApply = minimumQuantityToApply;
        }

        public Discount(double discountAmount, DiscountType discountType, int minimumQuantityToApply, Category category)
            : this(discountAmount, discountType, minimumQuantityToApply)
        {
            Category = category;
        }

        public abstract double GetDiscountPrice(double totalPrice);
    }
}
