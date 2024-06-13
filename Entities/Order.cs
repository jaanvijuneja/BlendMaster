using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlendMaster.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int TableID { get; set; }
        public decimal OrderMoney { get; set; }
        public DateTime OrderTime { get; set; }
    }
}
