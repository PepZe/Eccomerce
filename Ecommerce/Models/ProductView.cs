using Ecommerce.Domain.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Models
{
    public class ProductView
    {
        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
    }
}
