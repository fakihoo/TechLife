using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechLife.Models
{
    public class ShopStore
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? Name { get; set; }
        public string ImgUrl { get; set; }
        public double price { get; set; }
        public double discount { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }
        [NotMapped]
        public string GenreName { get; set; }

    }
}
