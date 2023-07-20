using Ecommerce.Database.Context;
using Ecommerce.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Razor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }
        private ApplicationDbContext _dbContext { get; set; }

        public DeleteModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet(int? id)
        {
            if (id is null)
                return;

            Category = _dbContext.Categories.Find(id);
        }

        public IActionResult OnPost()
        {
            var toBeDeleted = _dbContext.Categories.Find(Category.Id);
            if (toBeDeleted is null)
            {
                TempData["error"] = "Category was not found";
                return NotFound();
            }

            _dbContext.Categories.Remove(toBeDeleted);
            _dbContext.SaveChanges();

            TempData["success"] = "Category was deleted";
            return RedirectToPage("Index");
        }
    }
}
