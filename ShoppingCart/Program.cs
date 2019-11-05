using ShoppingCart.Core;
using ShoppingCart.Core.Coupons;
using ShoppingCart.Core.Discounts;
using ShoppingCart.Core.Helpers;
using System;
using System.Collections.Generic;

namespace ShoppingCart
{
    class Program
    {
        static void Main()
        {
            try
            {
                var tShirtCategory = new Category("TShirt");
                var sportTShirtCategory = new Category("TShirt", tShirtCategory);
                var jacketCategory = new Category("Jacket");

                var campaigns = new List<Discount>();

                var tShirtCampaign = new RateCampaign(20, 4, tShirtCategory);

                campaigns.Add(tShirtCampaign);

                var coupon = new AmountCoupon(50, 300);

                var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);
                var kotonTShirt = new Product("Koton TShirt", 40, tShirtCategory);
                var lacosteTShirt = new Product("Lacoste TShirt", 220, tShirtCategory);

                var hummelTShirt = new Product("Hummel Sport TShirt", 120, sportTShirtCategory);

                var zaraJacket = new Product("Zara Jacket", 260, jacketCategory);

                var cart = new Core.ShoppingCart(new DeliveryCostCalculator(3.5, 2));

                cart.AddItem(poloTShirt, 1);
                cart.AddItem(poloTShirt, 1);
                cart.AddItem(kotonTShirt);
                cart.AddItem(lacosteTShirt);

                cart.AddItem(zaraJacket);

                cart.ApplyDiscounts(campaigns);
                cart.ApplyCoupon(coupon);

                Console.WriteLine(cart.Print());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
