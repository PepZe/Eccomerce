using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var categories = _categoryRepository.GetAll();
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

            _categoryRepository.Add(category);
            _categoryRepository.Save();
            TempData["success"] = "Category was created";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
                return BadRequest();

            var category = _categoryRepository.Get(c => c.Id == id);
            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View();

            _categoryRepository.Update(category);
            _categoryRepository.Save();
            TempData["success"] = "Category was edited";
            return RedirectToAction("Index");
        }


        [HttpGet, ActionName("Delete")]
        public IActionResult DeleteView(int? id)
        {

            if (id is null || id == 0)
                return BadRequest();

            var category = _categoryRepository.Get(c => c.Id == id);
            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
                return BadRequest();

            var category = _categoryRepository.Get(c => c.Id == id);
            if (category is null)
                return NotFound();

            _categoryRepository.Remove(category);
            _categoryRepository.Save();
            TempData["success"] = "Category was deleted";
            return View();
        }
    }
}
