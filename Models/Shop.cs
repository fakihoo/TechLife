using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechLife.Models
{
    public class Shop
    {
        public int ShopId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }

        [RegularExpression(@"^img\/.*\.(jfif|jpg)$", ErrorMessage = "The ImgURL must start with 'img/' and end with '.jfif' or '.jpg'")]
        public string ImgURL { get; set; }
    }
}
