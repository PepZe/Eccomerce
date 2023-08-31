using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public Address Address { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
