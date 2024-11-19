using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CaseStudyExample.Models
{
    public class Orders
    {
        /*
        public Orders()
        {
            OrderItems = new HashSet<OrderItems>();
            Payments = new HashSet<Payments>();
        }
        */
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ItemQuantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalAmount { get; set; }

        [Required]
        public string? ShippingAddress { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public virtual Users? Users { get; set; } = null!;
        public virtual Products? Products { get; set; } = null!;
        public virtual ICollection<Payments>? Payments { get; set; }

    }
    public enum OrderStatus
    {
        Pending,
        Shipped,
        Delivered,
        Cancelled
    }
}
