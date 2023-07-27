using Ecommerce.Database.Repositories;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
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

            _unitOfWork.Product.Add(product);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var product = _unitOfWork.Product.Get(p => p.Id == id);
            if (product is null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (product is null)
                return BadRequest();

            _unitOfWork.Product.Update(product);
            return RedirectToAction("Index");
        }

        [HttpGet("delete")]
        public IActionResult DeleteView(int? id)
        {
            if (id is null || id == 0)
                return BadRequest();

            var productToDelete = _unitOfWork.Product.Get(p => p.Id == id);

            if (productToDelete is null)
                return NotFound();

            return View(productToDelete);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
                return BadRequest();

            var productToBeDeleted = _unitOfWork.Product.Get(p => p.Id == id);
            if (productToBeDeleted is null)
                return NotFound();

            _unitOfWork.Product.Remove(productToBeDeleted);

            return RedirectToAction("Index");
        }
    }
}
