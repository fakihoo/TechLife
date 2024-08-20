using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechLife.Models
{
    public class SimServicesToDo
    {
        [Key]
        public int SimServicesToDoId { get; set; }
        public string SimServicesToDoName { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string CustName { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Please enter a valid 8-digit phone number")]
        public int PhoneNumber { get; set; }
        public string SimType { get; set; }
        public double Price { get; set; }
        public int Viewed { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public int SimServiceId { get; set; }
        [ForeignKey("SimServiceId")]
        public SimService SimService { get; set; }
    }
}
