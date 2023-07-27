using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model;
using Ecommerce.Domain.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAll();

            return View(products);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            var productView = new ProductView()
            {
                CategorySelectList = _categoryRepository.GetAll().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            if (id is not null && id != 0)
            {
                productView.Product = _productRepository.Get(p => p.Id == id);
            }
            else
            {
                productView.Product = new Product();
            }


            return View(productView);
        }

        [HttpPost]
        public IActionResult Upsert(ProductView productView, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                productView.CategorySelectList = _categoryRepository.GetAll().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                return View(productView);
            }

            _productRepository.Add(productView.Product);
            _productRepository.Save();

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
