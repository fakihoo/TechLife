using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechLife.Models;

namespace TechLife.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Condition { get; set; }
        public string Location { get; set; }
        public int GenreId { get; set; }
        public List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>(); // Ensure default initialization
        public int Views { get; set; }
        public int Likes { get; set; }
        public bool IsLiked { get; set; }
        public int Saves { get; set; }
        public string UserId { get; set; }
        public string Contact { get; set; }

        // To upload images
        public List<IFormFile> ImageFiles { get; set; }

        // For genres dropdown
        public List<SelectListItem> GenreList { get; set; }
        public DateTime UploadedAt { get; set; }
        public string UserProfilePictureUrl { get; set; }
        public string UserEmail { get; set; }
        public bool IsSold { get; set; }
    }

    public class ImageViewModel
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
