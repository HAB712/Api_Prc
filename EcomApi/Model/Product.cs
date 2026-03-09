using System.ComponentModel.DataAnnotations.Schema;

namespace EcomApi.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string ProdName { get; set; }

        public decimal Price { get; set; }

        public string? ImagePath { get; set; }
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

    }
}
