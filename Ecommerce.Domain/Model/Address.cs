using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Model
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
    }
}
