using System.ComponentModel.DataAnnotations;

namespace EcomApi.Model
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name Is Required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters")]
        public string CateName { get; set; }

    }
}
