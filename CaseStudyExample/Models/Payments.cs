using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CaseStudyExample.Models
{
    public class Payments
    {
        [Key]
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal AmountToPay { get; set; }

        [Required(ErrorMessage = "Payment Method is required.")]
        [EnumDataType(typeof(PaymentMethod))]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        [EnumDataType(typeof(PaymentStatus))]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public virtual Orders? Orders { get; set; } = null!;
        public virtual Users? Users { get; set; } = null!;
    }
    public enum PaymentMethod
    {
        [EnumMember(Value = "Credit Card")]
        CreditCard = 1,

        [EnumMember(Value = "Debit Card")]
        DebitCard = 2,

        [EnumMember(Value = "Net Banking")]
        NetBanking = 3,

        [EnumMember(Value = "UPI")]
        UPI = 4,

        [EnumMember(Value = "Cash on Delivery")]
        CashOnDelivery = 5
    }
    public enum PaymentStatus
    {
        Pending = 0, 
        Completed = 1, 
        Failed = 2
    }

}
