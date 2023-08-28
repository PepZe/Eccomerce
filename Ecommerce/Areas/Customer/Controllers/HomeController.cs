using Ecommerce.Common;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Ecommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet]
        public IActionResult Details(int productId)
        {
            var shoppingCart = new ShoppingCart()
            {
                ProductId = productId,
                Product = _productRepository.Get(p => p.Id == productId, "Category"),
                Count = 1
            };

            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            var existentCart = _shoppingCartRepository.Get(cart => cart.ApplicationUserId == userId && cart.Product.Id == shoppingCart.ProductId, includeProperties: "Product");
            if (existentCart != null)
            {
                existentCart.Count += shoppingCart.Count;
                existentCart.Price = existentCart.CalculateCartPrice();
                _shoppingCartRepository.Update(existentCart);
            }
            else
            {
                shoppingCart.Price = shoppingCart.CalculateCartPrice();
                _shoppingCartRepository.Add(shoppingCart);
            }

            _shoppingCartRepository.Save();
            TempData["success"] = "Product added to cart";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            var products = _productRepository.GetAll().ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}