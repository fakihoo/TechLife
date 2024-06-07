using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TechLife.Models.DTOs
{
    public class ShopStoreDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? ShopStoreName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Image { get; set; }
        [Required]
        public int GenreId { get; set; }
        public IFormFile? ImageFile { get; set; }
        public IEnumerable<SelectListItem>? GenreList { get; set; }
    }
}

