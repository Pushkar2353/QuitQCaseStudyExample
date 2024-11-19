using System.Security.Cryptography.Xml;
using System;
using System.ComponentModel.DataAnnotations;

namespace CaseStudyExample.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int CartQuantity { get; set; }
        public decimal? Amount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set;} = DateTime.Now;

        public virtual Products? Products { get; set; } = null!;
        public virtual Users? Users { get; set; } = null!;

    }
}
