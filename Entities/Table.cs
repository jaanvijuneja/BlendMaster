using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Entities
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        public string? TableName { get; set; }
    }
}
