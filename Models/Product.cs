using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TechLife.ViewModels;

namespace TechLife.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string Condition { get; set; }

        [Required]
        public string Location { get; set; }
        [Required]
        public string Contact { get; set; }

        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public List<Image> Images { get; set; } = new List<Image>(); // Ensure default initialization

        public int Views { get; set; }
        public int Likes { get; set; }
        public int Saves { get; set; }
        public bool IsSold { get; set; }
        public string UserId { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
