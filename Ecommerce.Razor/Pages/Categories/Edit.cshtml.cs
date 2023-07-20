using Ecommerce.Database.Context;
using Ecommerce.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Razor.Pages.Categories
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }
        private readonly ApplicationDbContext _dbContext;

        public EditModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet(int? id)
        {
            if (id == null)
                return;

            Category = _dbContext.Categories.Find(id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Something went wrong";
                return BadRequest();
            }

            _dbContext.Categories.Update(Category);
            _dbContext.SaveChanges();

            TempData["success"] = "Category was edited";
            return RedirectToPage("Index");
        }
    }
}
