using Ecommerce.Domain.Model;

namespace Ecommerce.Common
{
    public static class PriceCalculator
    {
        public static double CalculateCartPrice(this ShoppingCart cart)
        {
            if (cart.Count <= 50)
            {
                return cart.Product.Price;
            }
            else if (cart.Count > 100)
            {
                return cart.Product.Price100;
            }
            else
            {
                return cart.Product.Price50;
            }
        }
    }
}
