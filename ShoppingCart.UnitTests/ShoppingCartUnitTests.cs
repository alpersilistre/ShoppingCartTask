using Moq;
using ShoppingCart.Core;
using ShoppingCart.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class ShoppingCartUnitTests
    {
        [Fact]
        public void Add_Item_Should_Add_Cart_Item_To_The_Cart()
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
        public void Add_Item_With_Negative_Quantity_Should_Not_Add_Item_To_The_Cart()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);

            Assert.Throws<ArgumentException>(() => shoppingCart.AddItem(poloTShirt, -3));            
        }

        [Fact]
        public void Add_Item_Should_Increase_Quanitiy_If_The_Product_Is_Exist_In_The_Cart()
        {
            var calc = new Mock<IDeliveryCostCalculator>();
            var shoppingCart = new Core.ShoppingCart(calc.Object);

            var tShirtCategory = new Category("TShirt");
            var poloTShirt = new Product("Polo TShirt", 120, tShirtCategory);

            shoppingCart.AddItem(poloTShirt, 3);

            Assert.True(shoppingCart.Items.Single(y => y.Product.Title == poloTShirt.Title).Quantity == 3);

            shoppingCart.AddItem(poloTShirt);

            Assert.True(shoppingCart.Items.Single(y => y.Product.Title == poloTShirt.Title).Quantity == 4);
        }
    }
}
