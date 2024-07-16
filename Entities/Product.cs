using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string? ProductDescription { get; set; }
        public string? ImageUrl { get; set; }
    }
}
