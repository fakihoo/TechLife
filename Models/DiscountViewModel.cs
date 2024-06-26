using System.ComponentModel.DataAnnotations;

namespace TechLife.Models
{
    public class DiscountViewModel
    {
        public int Id { get; set; }
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
        public double Discount { get; set; }
    }
}
