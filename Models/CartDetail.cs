using System.ComponentModel.DataAnnotations;

namespace TechLife.Models
{
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCart_Id { get; set; }
        [Required]
        public int ShopStoreId { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
        public ShopStore ShopStore { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
