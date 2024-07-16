using WebApplication2.Entities;

namespace WebApplication2.Models
{
    public class OngoingOrdersViewModel
    {
        public Guid OrderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public OrderStatusType OrderStatus { get; set; }
        public List<string>? Products { get; set; }
    }
}
