using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public enum BillStatusType
{
    Unpaid,
    Pending,
    Paid,
    Refunded,
    Disputed
}

public enum PaymentMethodType
{
    CreditCard,
    DebitCard,
    PayPal,
    ApplePay,
    GooglePay
}

namespace WebApplication2.Entities
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BillId { get; set; }

        public Guid OrderId { get; set; }
        public CustomerOrder? CustomerOrder { get; set; }

        public int TableId { get; set; }
        public Table? Table { get; set; }

        public DateTime BillDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public decimal Tax { get; set; }

        public decimal AmountPaid { get; set; }

        public PaymentMethodType PaymentMethod { get; set; }

        public BillStatusType Status { get; set; }
    }
}
