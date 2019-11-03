using Ardalis.GuardClauses;
using System;

namespace ShoppingCart.Core.Helpers
{
    public static class GuardExtensions
    {
        public static void Negative(this IGuardClause guardClause, int quantity, string parameterName)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity can't be less than 1", parameterName);
        }
    }
}
