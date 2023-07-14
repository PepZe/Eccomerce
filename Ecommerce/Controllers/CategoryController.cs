using Ecommerce.Database.Context;
using Ecommerce.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();
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

            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category was created";
            return View("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
                return BadRequest();

            var category = _dbContext.Categories.Find(id);
            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View();

            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category was edited";
            return RedirectToAction("Index");
        }


        [HttpGet, ActionName("Delete")]
        public IActionResult DeleteView(int? id)
        {

            if (id is null || id == 0)
                return BadRequest();

            var category = _dbContext.Categories.Find(id);
            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
                return BadRequest();

            var category = _dbContext.Categories.Find(id);
            if (category is null)
                return NotFound();

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category was deleted";
            return View();
        }
    }
}
