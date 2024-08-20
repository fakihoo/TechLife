using System.ComponentModel.DataAnnotations;

namespace TechLife.Models
{
    public class SimService
    {
        [Key]
        public int SimServiceId { get; set; }
        [Required]
        public string SimServiceName { get; set; }
        [Required]
        public string SimServiceType { get; set; } = "Normal";
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string SimType { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal Dollars { get; set; } = 0;
        [Required]
        public int Amount { get; set; }
        public string ImgUrl { get; set; }
        public int Viewed { get; set; }
    }
}
