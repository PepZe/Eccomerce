using Ecommerce.Common;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public CartController(IShoppingCartRepository shoppingCartRepository, UserManager<IdentityUser> userManager)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);

            var carts = _shoppingCartRepository.GetAll(cart => cart.ApplicationUserId == userId, includeProperties: "Product").ToList();

            double orderTotal = 0;
            foreach (var cart in carts)
            {
                orderTotal += cart.Price * cart.Count;
            }

            var shoppingCart = new ShoppingCartViewModel()
            {
                ShoppingCarts = carts,
                OrderTotal = orderTotal
            };

            return View(shoppingCart);
        }

        public IActionResult Summary()
        {
            return View();
        }

        public IActionResult PlusBtn(int cartId)
        {
            var cart = _shoppingCartRepository.Get(cart => cart.Id == cartId);
            cart.Count += 1;

            cart.Price = cart.CalculateCartPrice();
            _shoppingCartRepository.Update(cart);
            _shoppingCartRepository.Save();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult MinusBtn(int cartId)
        {
            var cart = _shoppingCartRepository.Get(cart => cart.Id == cartId);
            if (cart.Count <= 1)
                _shoppingCartRepository.Remove(cart);
            else
            {
                cart.Count -= 1;
                cart.Price = cart.CalculateCartPrice();
                _shoppingCartRepository.Update(cart);
            }

            _shoppingCartRepository.Save();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveBtn(int cartId)
        {
            var cart = _shoppingCartRepository.Get(cart => cart.Id == cartId);

            _shoppingCartRepository.Remove(cart);
            _shoppingCartRepository.Save();

            return RedirectToAction(nameof(Index));
        }      
    }
}
