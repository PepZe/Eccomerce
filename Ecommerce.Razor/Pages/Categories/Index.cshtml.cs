using Ecommerce.Database.Context;
using Ecommerce.Domain.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public List<Category> Categories { get; set; } 

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
            Categories = _dbContext.Categories.ToList();
        }
    }
}
