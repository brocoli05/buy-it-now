using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BuyItNowWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)] // property-level validation
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage ="Display Order must be between 1-100")] // property-level validation
        public int DisplayOrder { get; set; }
    }
}
