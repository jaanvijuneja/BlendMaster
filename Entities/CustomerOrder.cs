using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Entities
{
    public enum OrderStatusType
    {
        Preparing,
        Completed,
        Cancelled
    }

    public class CustomerOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }

        public decimal Total { get; set; }

        public DateTime CreatedDate { get; set; }

        public OrderStatusType OrderStatus { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }

        public int TableId { get; set; }
        public Table? Table { get; set; }
    }
}
