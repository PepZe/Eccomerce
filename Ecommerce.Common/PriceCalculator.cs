using Ecommerce.Domain.Model;

namespace Ecommerce.Common
{
    public static class PriceCalculator
    {
        public static double CalculateCartPrice(this ShoppingCart cart, Product product = null)
        {
            if (product == null)
                product = cart.Product;

            if (cart.Count <= 50)
            {
                return product.Price;
            }
            else if (cart.Count > 100)
            {
                return product.Price100;
            }
            else
            {
                return product.Price50;
            }
        }
    }
}
