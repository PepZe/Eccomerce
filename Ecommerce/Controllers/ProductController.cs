using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _productRepository.Add(product);
            _productRepository.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var product = _productRepository.Get(p => p.Id == id);
            if (product is null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (product is null)
                return BadRequest();

            _productRepository.Update(product);
            return RedirectToAction("Index");
        }

        [HttpGet("delete")]
        public IActionResult DeleteView(int? id)
        {
            if (id is null || id == 0)
                return BadRequest();

            var productToDelete = _productRepository.Get(p => p.Id == id);

            if (productToDelete is null)
                return NotFound();

            return View(productToDelete);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
                return BadRequest();

            var productToBeDeleted = _productRepository.Get(p => p.Id == id);
            if (productToBeDeleted is null)
                return NotFound();

            _productRepository.Remove(productToBeDeleted);

            return RedirectToAction("Index");
        }
    }
}
