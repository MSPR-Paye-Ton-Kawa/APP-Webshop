namespace WebShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }  // Managed externally
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Payment> Payments { get; set; }

        // Nouvelle propriété pour stocker le montant total
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }

    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }

        public Order? Order { get; set; }
        public int ProductId { get; set; }  // Managed externally
        public int Quantity { get; set; }
    }

    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
    }
}