using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }

        public string? ProductName { get; set; }

        public Guid CategoryId { get; set; }

        public Category? Category { get; set; }

        public decimal Price { get; set; }

        public string? ProductDescription { get; set; }

        public string? ImageUrl { get; set; }
    }
}
