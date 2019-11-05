using Ardalis.GuardClauses;

namespace ShoppingCart.Core
{
    public class ShoppingCartItem
    {
        public ShoppingCart ShoppingCart { get; }

        public Product Product { get; }

        private int quantity;
        public int Quantity
        {
            get
            {
                return quantity > 0 ? quantity : 0;
            }

            private set => value = quantity;
        }

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
            this.quantity = quantity;
        }

        public void IncreaseQuantity(int quantity = 1)
        {
            Guard.Against.Null(ShoppingCart, nameof(ShoppingCart));
            Guard.Against.Null(Product, nameof(Product));

            this.quantity += quantity;
        }

        public void DecreaseQuantity(int quantity = 1)
        {
            Guard.Against.Null(ShoppingCart, nameof(ShoppingCart));
            Guard.Against.Null(Product, nameof(Product));

            this.quantity -= quantity;
        }
    }
}
