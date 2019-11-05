using Moq;
using ShoppingCart.Core;
using ShoppingCart.Core.Coupons;
using ShoppingCart.Core.Discounts;
using ShoppingCart.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class ShoppingCartUnitTests
    {
        #region Add Item

        [Fact]
        public void Add_Item_With_Negative_Quantity_Should_Return_Argument_Exception()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);

            Assert.Throws<ArgumentException>(() => shoppingCart.AddItem(poloTShirt, -3));
        }

        [Fact]
        public void Add_Item_With_Null_Object_Should_Return_Argument_Null_Exception()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            Product product = null;

            Assert.Throws<ArgumentNullException>(() => shoppingCart.AddItem(product));
        }

        [Fact]
        public void Add_Item_Should_Add_The_Item_To_The_Cart()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);

            shoppingCart.AddItem(poloTShirt);

            Assert.Contains(shoppingCart.Items, y => y.Product.Title == poloTShirt.Title);
        }

        [Fact]
        public void Add_Item_With_Quantity_Should_Add_Correct_Amount_Of_Items_To_The_Cart()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);

            shoppingCart.AddItem(poloTShirt, 3);

            Assert.True(shoppingCart.Items.Single(y => y.Product.Title == poloTShirt.Title).Quantity == 3);
        }

        [Fact]
        public void Add_Item_Should_Increase_Quantity_If_The_Product_Is_Exist_In_The_Cart()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);

            shoppingCart.AddItem(poloTShirt, 3);
            shoppingCart.AddItem(poloTShirt);

            Assert.True(shoppingCart.Items.Single(y => y.Product.Title == poloTShirt.Title).Quantity == 4);
        }

        #endregion

        #region Remove Item

        [Fact]
        public void Remove_Item_With_Negative_Quantity_Should_Return_Argument_Exception()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);

            Assert.Throws<ArgumentException>(() => shoppingCart.RemoveItem(poloTShirt, -3));
        }

        [Fact]
        public void Remove_Item_With_Null_Object_Should_Return_Argument_Null_Exception()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            Product product = null;

            Assert.Throws<ArgumentNullException>(() => shoppingCart.RemoveItem(product));
        }

        [Fact]
        public void Remove_Item_Should_Remove_The_Item_From_The_Cart()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);

            shoppingCart.AddItem(poloTShirt);
            shoppingCart.RemoveItem(poloTShirt);

            Assert.True(shoppingCart.Items.Single(y => y.Product.Title == poloTShirt.Title).Quantity == 0);
        }

        [Fact]
        public void Remove_Item_With_Quantity_That_Is_More_Than_Whats_In_The_Cart_Should_Return_Zero()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);

            shoppingCart.AddItem(poloTShirt);
            shoppingCart.RemoveItem(poloTShirt, 2);

            Assert.True(shoppingCart.Items.Single(y => y.Product.Title == poloTShirt.Title).Quantity == 0);
        }

        #endregion

        #region Apply Coupon

        [Fact]
        public void Apply_Coupon_With_Null_Object_Should_Return_Argument_Null_Exception()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            Coupon coupon = null;

            Assert.Throws<ArgumentNullException>(() => shoppingCart.ApplyCoupon(coupon));
        }

        [Fact]
        public void Apply_Coupon_Should_Add_The_Coupon_To_The_Cart()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var coupon = new AmountCoupon(50, 300);
            shoppingCart.ApplyCoupon(coupon);

            Assert.NotNull(shoppingCart.Coupon);
        }

        [Fact]
        public void Apply_Coupon_Should_Replace_The_Previous_Coupon_In_The_Cart()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var coupon = new AmountCoupon(50, 300);
            shoppingCart.ApplyCoupon(coupon);

            var coupon2 = new AmountCoupon(50, 200);
            shoppingCart.ApplyCoupon(coupon2);

            Assert.True(shoppingCart.Coupon.MinimumAmountToApply == 200);
        }

        #endregion

        #region Get Coupon Discount

        [Fact]
        public void Get_Coupon_Discount_With_Null_Coupon_Object_Should_Return_Zero()
        {
            var calc = new Mock<IDeliveryCostCalculator>();

            Type type = typeof(Core.ShoppingCart);
            var shoppingCart = Activator.CreateInstance(type, calc.Object);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "GetCouponDiscount" && x.IsPrivate).First();

            var output = method.Invoke(shoppingCart, new object[] { 200 });

            Assert.True(Convert.ToInt32(output) == 0);
        }

        [Fact]
        public void Get_Coupon_Discount_With_Total_Price_Is_Smaller_Than_Minimum_Amount_Should_Return_Zero()
        {
            var calc = new Mock<IDeliveryCostCalculator>();

            Type type = typeof(Core.ShoppingCart);
            var shoppingCart = Activator.CreateInstance(type, calc.Object);

            var coupon = new AmountCoupon(50, 300);

            ((Core.ShoppingCart)shoppingCart).ApplyCoupon(coupon);

            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "GetCouponDiscount" && x.IsPrivate).First();

            var output = method.Invoke(shoppingCart, new object[] { 120 });

            Assert.True(Convert.ToInt32(output) == 0);
        }

        [Fact]
        public void Get_Coupon_Discount_With_Total_Price_Is_Bigger_Than_Minimum_Amount_Should_Return_Discount_Price()
        {
            var calc = new Mock<IDeliveryCostCalculator>();

            Type type = typeof(Core.ShoppingCart);
            var shoppingCart = Activator.CreateInstance(type, calc.Object);

            int discountAmount = 50;

            var coupon = new AmountCoupon(discountAmount, 300);

            ((Core.ShoppingCart)shoppingCart).ApplyCoupon(coupon);

            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "GetCouponDiscount" && x.IsPrivate).First();

            var output = method.Invoke(shoppingCart, new object[] { 320 });

            Assert.True(Convert.ToInt32(output) == discountAmount);
        }

        [Fact]
        public void Get_Coupon_Discount_With_Amount_Coupon_Should_Return_The_Discount_Amount_Of_The_Applied_Coupon()
        {
            var calc = new Mock<IDeliveryCostCalculator>();

            Type type = typeof(Core.ShoppingCart);
            var shoppingCart = Activator.CreateInstance(type, calc.Object);

            int discountAmount = 150;

            var coupon = new AmountCoupon(discountAmount, 600);

            ((Core.ShoppingCart)shoppingCart).ApplyCoupon(coupon);

            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "GetCouponDiscount" && x.IsPrivate).First();

            var output = method.Invoke(shoppingCart, new object[] { 600 });

            Assert.True(Convert.ToInt32(output) == discountAmount);
        }

        [Fact]
        public void Get_Coupon_Discount_With_Rate_Coupon_Should_Return_The_Percentage_Of_The_Specified_Rate_Of_The_Applied_Coupon()
        {
            var calc = new Mock<IDeliveryCostCalculator>();

            Type type = typeof(Core.ShoppingCart);
            var shoppingCart = Activator.CreateInstance(type, calc.Object);

            int discountPercentage = 10;
            int totalCartAmount = 500;

            var coupon = new RateCoupon(discountPercentage, 100);

            ((Core.ShoppingCart)shoppingCart).ApplyCoupon(coupon);

            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "GetCouponDiscount" && x.IsPrivate).First();

            var output = method.Invoke(shoppingCart, new object[] { totalCartAmount });

            Assert.True(Convert.ToInt32(output) == totalCartAmount * discountPercentage / 100);
        }

        #endregion

        #region Apply Discounts

        [Fact]
        public void Apply_Discounts_Should_Not_Add_Any_Discount_To_The_Cart_If_No_Campaign_Is_Available()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 100, tShirtCategory);
            
            var campaigns = new List<Discount>();

            shoppingCart.AddItem(poloTShirt, 2);
            
            shoppingCart.ApplyDiscounts(campaigns);

            Assert.Null(shoppingCart.Campaign);
        }

        [Fact]
        public void Apply_Discounts_With_A_Campaign_That_Satisfy_Minimum_Product_Quantity_Should_Add_The_Discount_To_The_Cart()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);

            var campaigns = new List<Discount>();

            shoppingCart.AddItem(poloTShirt, 2);
            
            var tShirtCampaign = new RateCampaign(20, 1, tShirtCategory);

            campaigns.Add(tShirtCampaign);

            shoppingCart.ApplyDiscounts(campaigns);

            Assert.NotNull(shoppingCart.Campaign);
        }

        [Fact]
        public void Apply_Discounts_With_A_Campaign_That_Does_Not_Satisfy_Minimum_Product_Quantity_Should_Not_Add_The_Discount_To_The_Cart()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);

            var campaigns = new List<Discount>();

            shoppingCart.AddItem(poloTShirt);

            var tShirtCampaign = new RateCampaign(20, 2, tShirtCategory);

            campaigns.Add(tShirtCampaign);

            shoppingCart.ApplyDiscounts(campaigns);

            Assert.Null(shoppingCart.Campaign);
        }

        [Fact]
        public void Apply_Discounts_Should_Add_A_Discount_To_The_Cart_Which_Has_The_Maximum_Discount_Among_All_Campaigns()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 100, tShirtCategory);
            var kotonTShirt = new Product("Koton TShirt", 40, tShirtCategory);

            var campaigns = new List<Discount>();

            shoppingCart.AddItem(poloTShirt, 2);
            shoppingCart.AddItem(kotonTShirt, 5);

            var tShirtCampaign = new RateCampaign(10, 3, tShirtCategory);
            var tShirtCampaign2 = new AmountCampaign(100, 5, tShirtCategory);

            campaigns.Add(tShirtCampaign);
            campaigns.Add(tShirtCampaign2);

            shoppingCart.ApplyDiscounts(campaigns);

            Assert.True(shoppingCart.Campaign == tShirtCampaign2);
        }

        #endregion

        #region Get Campaign Discount

        [Fact]
        public void Get_Campaign_Discount_With_Null_Campaign_Object_Should_Return_Zero()
        {
            var calc = new Mock<IDeliveryCostCalculator>();

            Type type = typeof(Core.ShoppingCart);
            var shoppingCart = Activator.CreateInstance(type, calc.Object);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "GetCampaignDiscount" && x.IsPrivate).First();

            var output = method.Invoke(shoppingCart, new object[] { });

            Assert.True(Convert.ToInt32(output) == 0);
        }

        [Fact]
        public void Get_Campaign_Discount_With_Amount_Campaign_Should_Return_The_Discount_Amount_Of_The_Applied_Campaign()
        {
            var calc = new Mock<IDeliveryCostCalculator>();

            Type type = typeof(Core.ShoppingCart);
            var shoppingCart = Activator.CreateInstance(type, calc.Object);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "GetCampaignDiscount" && x.IsPrivate).First();

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);

            ((Core.ShoppingCart)shoppingCart).AddItem(poloTShirt);

            var campaigns = new List<Discount>();

            var campaign = new AmountCampaign(50, 1, tShirtCategory);
            campaigns.Add(campaign);

            ((Core.ShoppingCart)shoppingCart).ApplyDiscounts(campaigns);            

            var output = method.Invoke(shoppingCart, new object[] { });

            Assert.True(Convert.ToInt32(output) == 50);
        }

        [Fact]
        public void Get_Campaign_Discount_With_Rate_Campaign_Should_Return_The_Percentage_Of_The_Specified_Rate_Of_The_Applied_Campaign()
        {
            var calc = new Mock<IDeliveryCostCalculator>();

            Type type = typeof(Core.ShoppingCart);
            var shoppingCart = Activator.CreateInstance(type, calc.Object);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "GetCampaignDiscount" && x.IsPrivate).First();

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 200, tShirtCategory);

            ((Core.ShoppingCart)shoppingCart).AddItem(poloTShirt);

            var campaigns = new List<Discount>();

            var campaign = new RateCampaign(50, 1, tShirtCategory);
            campaigns.Add(campaign);

            ((Core.ShoppingCart)shoppingCart).ApplyDiscounts(campaigns);

            var output = method.Invoke(shoppingCart, new object[] { });

            Assert.True(Convert.ToInt32(output) == 100);
        }

        #endregion

        #region Get Total Amount After Discounts

        [Fact]
        public void Get_Total_Amount_After_Discounts_With_Campaign_And_Coupon_Should_Return_Correct_Price()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var jacketCategory = new Category("Jacket");

            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);
            var kotonTShirt = new Product("Koton TShirt", 40, tShirtCategory);
            var lacosteTShirt = new Product("Lacoste TShirt", 220, tShirtCategory);
            var zaraJacket = new Product("Zara Jacket", 260, jacketCategory);

            shoppingCart.AddItem(poloTShirt, 2);
            shoppingCart.AddItem(kotonTShirt);
            shoppingCart.AddItem(lacosteTShirt);
            shoppingCart.AddItem(zaraJacket);

            var campaigns = new List<Discount>();
            var tShirtCampaign = new RateCampaign(20, 3, tShirtCategory);
            campaigns.Add(tShirtCampaign);

            var coupon = new AmountCoupon(50, 300);

            shoppingCart.ApplyDiscounts(campaigns);
            shoppingCart.ApplyCoupon(coupon);

            var totalAmount = shoppingCart.GetTotalAmountAfterDiscounts();

            // ( ( (120 * 2) + 40 + 220 ) * %20 ) + 260 => 660 - 50 => 610

            Assert.True(totalAmount == 610);
        }

        [Fact]
        public void Get_Total_Amount_After_Discounts_With_Campaign_Should_Return_Correct_Price()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var jacketCategory = new Category("Jacket");

            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);
            var kotonTShirt = new Product("Koton TShirt", 40, tShirtCategory);
            var lacosteTShirt = new Product("Lacoste TShirt", 220, tShirtCategory);
            var zaraJacket = new Product("Zara Jacket", 260, jacketCategory);

            shoppingCart.AddItem(poloTShirt, 2);
            shoppingCart.AddItem(kotonTShirt);
            shoppingCart.AddItem(lacosteTShirt);
            shoppingCart.AddItem(zaraJacket);

            var campaigns = new List<Discount>();

            var tShirtCampaign = new RateCampaign(20, 3, tShirtCategory);
            campaigns.Add(tShirtCampaign);

            shoppingCart.ApplyDiscounts(campaigns);

            var totalAmount = shoppingCart.GetTotalAmountAfterDiscounts();

            // ( ( (120 * 2) + 40 + 220 ) * %20 ) + 260 => 400 + 260 => 660

            Assert.True(totalAmount == 660);
        }

        [Fact]
        public void Get_Total_Amount_After_Discounts_With_Coupon_Should_Return_Correct_Price()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var couponDiscountAmount = 50;

            var tShirtCategory = new Category("TShirt");
            var jacketCategory = new Category("Jacket");

            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);
            var kotonTShirt = new Product("Koton TShirt", 40, tShirtCategory);
            var lacosteTShirt = new Product("Lacoste TShirt", 220, tShirtCategory);
            var zaraJacket = new Product("Zara Jacket", 260, jacketCategory);

            shoppingCart.AddItem(poloTShirt, 2);
            shoppingCart.AddItem(kotonTShirt);
            shoppingCart.AddItem(lacosteTShirt);
            shoppingCart.AddItem(zaraJacket);

            var campaigns = new List<Discount>();            
            var coupon = new AmountCoupon(couponDiscountAmount, 300);

            shoppingCart.ApplyDiscounts(campaigns);
            shoppingCart.ApplyCoupon(coupon);

            var totalAmount = shoppingCart.GetTotalAmountAfterDiscounts();

            // ( (120 * 2) + 40 + 220 ) + 260 => 760 - 50 => 710

            Assert.True(totalAmount == shoppingCart.TotalItemPrice - couponDiscountAmount);
        }

        #endregion
    }
}
