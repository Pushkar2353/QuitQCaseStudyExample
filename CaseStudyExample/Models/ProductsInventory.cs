using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Numerics;

namespace CaseStudyExample.Models
{
    public class ProductsInventory
    {
        [Key]
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Current stock must be a positive value.")]
        public int CurrentStock { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Stock must be at least 1.")]
        public int MinimumStock { get; set; }
        public DateTime? LastRestockDate { get; set; } = DateTime.Now;
        public DateTime? NextRestockDate { get; set; } = DateTime.Now;
        public virtual Products? Products { get; set; } = null!;
    }
}
