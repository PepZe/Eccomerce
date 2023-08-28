using Ecommerce.Domain.Model;

namespace Ecommerce.Models
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCart> ShoppingCarts { get; set; }
        public double OrderTotal { get; set; }
    }
}
