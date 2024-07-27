using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Entities
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderDetailId { get; set; }

        [Required]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Unit price must be a non-negative value.")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        public Guid OrderId { get; set; }
        public CustomerOrder? CustomerOrder { get; set; }
    }
}
