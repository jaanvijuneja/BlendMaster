using System.ComponentModel.DataAnnotations;

namespace BlendMaster.Entities
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }
        public string TableName { get; set; }
    }
}
