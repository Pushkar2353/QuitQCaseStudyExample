using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudyExample.Models
{
    public class Reviews
    {
        [Key]
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [Column(TypeName = "TEXT")]
        public string? ReviewText { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;

        public virtual Products? Products { get; set; } = null!;
        public virtual Users? Users { get; set; } = null!;

    }
}
