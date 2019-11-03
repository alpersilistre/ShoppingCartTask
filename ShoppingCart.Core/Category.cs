namespace ShoppingCart.Core
{
    public class Category
    {
        public string Title { get; }
        public Category Parent { get; }

        public Category(string title)
        {
            Title = title;
        }

        public Category(string title, Category parent) : this(title)
        {
            Parent = parent;
        }
    }
}
