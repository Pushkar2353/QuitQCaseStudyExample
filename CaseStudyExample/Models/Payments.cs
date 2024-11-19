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
        CreditCard,

        [EnumMember(Value = "Debit Card")]
        DebitCard,

        [EnumMember(Value = "Net Banking")]
        NetBanking,

        [EnumMember(Value = "UPI")]
        UPI,

        [EnumMember(Value = "Cash on Delivery")]
        CashOnDelivery
    }
    public enum PaymentStatus
    {
        Pending, 
        Completed, 
        Failed
    }

}
