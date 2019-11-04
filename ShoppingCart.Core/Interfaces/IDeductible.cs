using ShoppingCart.Core.Helpers;

namespace ShoppingCart.Core.Interfaces
{
    public interface IDeductible
    {
        double DiscountAmount { get; }
        DiscountType DiscountType { get; }
    }
}
