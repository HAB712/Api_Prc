namespace ConsumeApi.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public IFormFile? ImageFile { get; set; }  // Upload
        public string ImagePath { get; set; } = string.Empty; // Display
        public int CategoryId { get; set; }


    }
}
