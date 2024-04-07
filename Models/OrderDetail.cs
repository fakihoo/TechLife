using System.ComponentModel.DataAnnotations;

namespace TechLife.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }
        [Required]

        public int  ShopStoreId { get; set; }
        [Required]

        public int Quantity { get; set; }
        [Required]

        public double UnitPrice { get; set; }
        public ShopStore ShopStore { get; set; }
    }
}
