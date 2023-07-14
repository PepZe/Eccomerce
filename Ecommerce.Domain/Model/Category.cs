using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(100)]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be a between 1-100") ]
        public int DisplayOrder { get; set; }
    }
}
