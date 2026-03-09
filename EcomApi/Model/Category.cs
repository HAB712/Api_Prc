namespace EcomApi.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CateName { get; set; }
        public ICollection<Product>? Products { get; set; }

    }
}
