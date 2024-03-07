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
        public string PhoneNumber { get; set; }
        [Required]
        public string SimType { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string ImgUrl { get; set; }
    }
}
