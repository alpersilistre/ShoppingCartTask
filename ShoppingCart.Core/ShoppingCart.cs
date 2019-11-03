using ShoppingCart.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Core
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly IDeliveryCostCalculator DeliveryCostCalculator;

        public double TotalItemPrice
        {
            get
            {
                return Items.Sum(x => x.ItemPrice);
            }
        }

        public Coupon Coupon { get; private set; }

        public Campaign Campaign { get; set; }

        private readonly List<ShoppingCartItem> items = new List<ShoppingCartItem>();
        public IReadOnlyCollection<ShoppingCartItem> Items => items.AsReadOnly();

        public int NumberOfDeliveries
        {
            get => Items.GroupBy(x => x.Product.Category).Select(x => x.First()).Count();
        }

        public int NumberOfProducts
        {
            get => Items.Count;
        }

        public ShoppingCart(IDeliveryCostCalculator deliveryCostCalculator)
        {
            DeliveryCostCalculator = deliveryCostCalculator;
        }

        public void AddItem(Product product, int quantity = 1)
        {
            ShoppingCartItem item;

            if (Items.Any(x => x.Product.Title == product.Title))
            {
                item = items.Single(x => x.Product.Title == product.Title);

                item.IncreaseQuantity(quantity);
            }
            else
            {
                item = new ShoppingCartItem(this, product, quantity);

                items.Add(item);
            }
        }

        public void RemoveItem(Product product, int quantity = 1)
        {
            if (Items.Any(x => x.Product.Title == product.Title))
            {
                var item = items.Single(x => x.Product.Title == product.Title);

                item.DecreaseQuantity(quantity);
            }
        }

        public void ApplyCoupon(Coupon coupon)
        {
            Coupon = coupon;
        }

        private double GetCouponDiscount(double totalPrice)
        {
            if (Coupon != null)
            {
                if (totalPrice >= Coupon.MinimumAmountToApply)
                {
                    return Coupon.GetDiscountPrice(totalPrice);
                }
            }

            return 0;
        }

        public void ApplyDiscounts(List<Campaign> campaigns)
        {
            double maximumDiscount = 0;

            foreach (var campaign in campaigns)
            {
                var numberOfItemByCategory = GetNumberOfItemByCategory(campaign.Category);

                if (numberOfItemByCategory >= campaign.MinimumAmountToApply)
                {
                    var discountValue = campaign.GetDiscountPrice(TotalItemPrice);

                    if (campaign.GetDiscountPrice(TotalItemPrice) > maximumDiscount)
                    {
                        maximumDiscount = discountValue;
                        Campaign = campaign;
                    }
                }
            }
        }

        private double GetCampaignDiscount(double totalPrice)
        {
            if (Campaign != null)
            {
                return Campaign.GetDiscountPrice(totalPrice);
            }

            return 0;
        }

        public double GetTotalAmountAfterDiscounts()
        {
            var totalPrice = TotalItemPrice;

            totalPrice -= GetCampaignDiscount(totalPrice);

            totalPrice -= GetCouponDiscount(totalPrice);

            return totalPrice;
        }

        public double GetDeliveryCost()
        {
            return DeliveryCostCalculator.CalculateFor(this);         
        }

        public string Print()
        {
            var output = new StringBuilder();

            var categories = Items.GroupBy(x => x.Product.Category).Select(x => x.First()).Select(x => x.Product.Category);

            foreach (var category in categories)
            {
                var categoryItems = Items.Where(x => x.Product.Category.Title == category.Title);

                foreach (var cartItem in categoryItems)
                {
                    output.AppendLine($"Category Name: {category.Title}, Product Name: {cartItem.Product.Title}, " +
                        $"Quantity: {cartItem.Quantity}, Unit Price: {cartItem.Product.Price} TL, Total Price: {cartItem.ItemPrice} TL");
                }
            }

            output.AppendLine($"Total Discount applied: {TotalItemPrice - GetTotalAmountAfterDiscounts()} TL.");

            output.AppendLine($"Total Amount: {GetTotalAmountAfterDiscounts()} TL, Delivery Cost: {GetDeliveryCost()} TL.");

            output.AppendLine($"Total Amount to pay: {GetTotalAmountAfterDiscounts() + GetDeliveryCost()} TL.");

            return output.ToString();
        }

        private int GetNumberOfItemByCategory(Category category)
        {
            return Items.Where(x => x.Product.Category.Title == category.Title).Sum(y => y.Quantity);
        }
    }
}
