using System.ComponentModel.DataAnnotations;

namespace CaseStudyExample.Models
{
    public class Categories
    {
        /*
        public Categories()
        {
            Products = new HashSet<Products>();
        }
        */
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; } = string.Empty;

        public ICollection<Products>? Products { get; set; }


    }
}
