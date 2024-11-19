using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace CaseStudyExample.Models
{
    public class Administrator : Users
    {
        [Key]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public int AdminPhoneNumber { get; set; }
        public virtual Users? Users { get; set; } = null!;


    }
}
