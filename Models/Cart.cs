using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechLife.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        [Required]
        public string CartName { get; set;}
        public double CartPrice { get; set; }
        public int CartQuantity { get; set; }
        public double CartSubTotal { get; set;}
        public string ImgURL { get; set; }
        public int ShopId { get; set; }
        [ForeignKey("ShopId")]
        public Shop Shop { get; set; }

    }
}
