using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudyExample.Models
{
    public class Products
    {
        /*
        public Products()
        {
            Cart = new HashSet<Cart>();
            OrderItems = new HashSet<OrderItems>();
            Reviews = new HashSet<Reviews>();
            ProductsInventory = new HashSet<ProductsInventory>();
        }
        */
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product Description is required")]
        [Column(TypeName = "TEXT")]
        public string ProductDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        public decimal? ProductPrice { get; set; }

        [Required(ErrorMessage = "Stock Quantity is required")]
        public int? ProductStock { get; set; }
        public int CategoryId { get; set; }
        public int SellerId { get; set; }

        [Required(ErrorMessage = "Product Url is required")]
        public string? ProductUrl { get; set; }
        public IFormFile? ProductImage { get; set; }


        public virtual Categories? Categories { get; set; } = null!;
        public virtual Sellers? Seller { get; set; } = null!;
        public virtual ICollection<ProductsInventory>? ProductsInventory { get; set; }
        public virtual ICollection<Cart>? Cart { get; set; }
        public virtual ICollection<Reviews>? Reviews { get; set; }


    }
}
