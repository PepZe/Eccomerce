using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var categories = _unitOfWork.Category.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
                return View();

            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            TempData["success"] = "Category was created";
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
                return BadRequest();

            var category = _unitOfWork.Category.Get(c => c.Id == id);
            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View();

            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
            TempData["success"] = "Category was edited";
            return RedirectToAction("Index");
        }


        [HttpGet, ActionName("Delete")]
        public IActionResult DeleteView(int? id)
        {

            if (id is null || id == 0)
                return BadRequest();

            var category = _unitOfWork.Category.Get(c => c.Id == id);
            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
                return BadRequest();

            var category = _unitOfWork.Category.Get(c => c.Id == id);
            if (category is null)
                return NotFound();

            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category was deleted";
            return View();
        }
    }
}
