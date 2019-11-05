using Moq;
using ShoppingCart.Core.Helpers;
using ShoppingCart.Core.Interfaces;
using System;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class DeliveryCostCalculatorUnitTests
    {
        [Fact]
        public void Calculate_For_With_Null_Object_Should_Return_Argument_Null_Exception()
        {            
            var deliveryCostCalculator = new DeliveryCostCalculator(3.5, 2);

            Assert.Throws<ArgumentNullException>(() => deliveryCostCalculator.CalculateFor(null));
        }

        [Fact]
        public void Calculate_For_With_Fixed_Price_Should_Return_Correct_Result()
        {
            var shoppingCart = new Mock<IShoppingCart>();

            shoppingCart.Setup(cart => cart.NumberOfDeliveries).Returns(2);
            shoppingCart.Setup(cart => cart.NumberOfProducts).Returns(3);

            var deliveryCostCalculator = new DeliveryCostCalculator(3.5, 2);

            // (3.5 * 2) + (3 * 2) + 2.99 Fixed Price
            Assert.True(deliveryCostCalculator.CalculateFor(shoppingCart.Object) == 15.99);
        }

        [Fact]
        public void Calculate_For_With_Custom_Price_Should_Return_Correct_Result()
        {
            var shoppingCart = new Mock<IShoppingCart>();

            shoppingCart.Setup(cart => cart.NumberOfDeliveries).Returns(2);
            shoppingCart.Setup(cart => cart.NumberOfProducts).Returns(3);

            var deliveryCostCalculator = new DeliveryCostCalculator(3.5, 2, 4);

            // (3.5 * 2) + (3 * 2) + 4 Custom Price
            Assert.True(deliveryCostCalculator.CalculateFor(shoppingCart.Object) == 17);
        }

        [Fact]
        public void Calculate_For_With_Empty_Cart_Should_Return_Zero()
        {
            var shoppingCart = new Mock<IShoppingCart>();

            shoppingCart.Setup(cart => cart.NumberOfDeliveries).Returns(0);
            shoppingCart.Setup(cart => cart.NumberOfProducts).Returns(0);

            var deliveryCostCalculator = new DeliveryCostCalculator(3.5, 2);

            Assert.True(deliveryCostCalculator.CalculateFor(shoppingCart.Object) == 0);
        }
    }
}
