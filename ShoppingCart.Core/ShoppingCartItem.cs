using System;

namespace ShoppingCart.Core
{
    public class ShoppingCartItem
    {
        public ShoppingCart ShoppingCart { get; }

        public Product Product { get; }

        public int Quantity { get; private set; } = 0;

        public double ItemPrice
        {
            get
            {
                return Product.Price * Quantity;
            }
        }

        public ShoppingCartItem(ShoppingCart cart, Product product, int quantity)
        {
            ShoppingCart = cart;
            Product = product;
            Quantity = quantity;
        }

        public void IncreaseQuantity(int quantity = 1)
        {
            if (ShoppingCart != null && Product != null)
            {
                Quantity += quantity;
            }
            else
            {
                throw new InvalidOperationException();
            }            
        }

        public void DecreaseQuantity(int quantity = 1)
        {
            if (ShoppingCart != null && Product != null)
            {
                Quantity -= quantity;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
