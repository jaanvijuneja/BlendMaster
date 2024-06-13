using System.ComponentModel.DataAnnotations;

namespace BlendMaster.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
