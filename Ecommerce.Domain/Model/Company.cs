using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Model
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Address? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
