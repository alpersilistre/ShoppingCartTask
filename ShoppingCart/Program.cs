using ShoppingCart.Core;
using ShoppingCart.Core.Helpers;
using System;
using System.Collections.Generic;

namespace ShoppingCart
{
    class Program
    {
        static void Main()
        {
            var tShirtCategory = new Category("TShirt");
            var jacketCategory = new Category("Jacket");

            var campaigns = new List<Campaign>();

            var tShirtCampaign = new Campaign(20, 3, DiscountType.Rate, tShirtCategory);

            campaigns.Add(tShirtCampaign);

            var coupon = new Coupon(50, 300, DiscountType.Amount);

            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);
            var kotonTShirt = new Product("Koton TShirt", 40, tShirtCategory);
            var lacosteTShirt = new Product("Lacoste TShirt", 220, tShirtCategory);

            var zaraJacket = new Product("Zara Jacket", 260, jacketCategory);

            var cart = new Core.ShoppingCart(new DeliveryCostCalculator(3.5, 2));

            cart.AddItem(poloTShirt, 2);
            cart.AddItem(kotonTShirt);
            cart.AddItem(lacosteTShirt);

            cart.AddItem(zaraJacket);

            cart.ApplyDiscounts(campaigns);
            cart.ApplyCoupon(coupon);

            Console.WriteLine(cart.Print());

            Console.ReadLine();
        }
    }
}
