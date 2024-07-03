using Microsoft.Build.Framework;

namespace TechLife.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
