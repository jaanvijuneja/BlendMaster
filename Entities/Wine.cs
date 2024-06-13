using System.ComponentModel.DataAnnotations;

namespace BlendMaster.Entities
{
    public class Wine
    {
        [Key]
        public int WineId { get; set; }
        public string WineName { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string Introduction { get; set; }
        public string ImageUrl { get; set; }
    }
}
