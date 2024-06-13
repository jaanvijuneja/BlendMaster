namespace BlendMaster.Models
{
    public class CartItemViewModel
    {
        public int WineId { get; set; }
        public string WineName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Quantity * Price;
    }
}
