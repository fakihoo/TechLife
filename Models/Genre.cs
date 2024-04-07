using System.ComponentModel.DataAnnotations;

namespace TechLife.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string GenreName { get; set; }
        public List<ShopStore> ShopStores { get; set; }
    }
}
