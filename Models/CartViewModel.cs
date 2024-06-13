using System.Collections.Generic;

namespace BlendMaster.Models
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public decimal TotalPrice { get; set; }
        public int TableId { get; set; }
    }
}
