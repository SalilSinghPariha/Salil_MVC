using System.ComponentModel.DataAnnotations;

namespace Salil_MVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage ="Category name should not be more than 20")]
        public string Name { get; set; }
        [Range(1,20,ErrorMessage ="Display order should be between 1-20")]
        public int DisplayOrder { get; set; }
    }
}
