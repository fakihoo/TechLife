using System.ComponentModel.DataAnnotations;

namespace TechLife.Models.DTOs
{
    public class ProfilePictureViewModel
    {
        [Required(ErrorMessage = "Please select a file.")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfilePicture { get; set; }
    }
}
