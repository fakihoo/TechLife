using System.ComponentModel.DataAnnotations;

namespace TechLife.Models.DTOs
{
    public class EmployeeCreateViewModel
    {
            [Required]
            public string Name { get; set; }

            [Required]
            public string Role { get; set; } 

            [Required]
            public string Contact { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            public string Location { get; set; }

            [Required]
            public string WorkLocation { get; set; }

            [Required]
            public string FatherName { get; set; }

            [Required]
            public DateTime DateOfBirth { get; set; }

            [Required]
            public DateTime JoiningDate { get; set; }

            [Required]
            public decimal Salary { get; set; }

            public string ImageProfile { get; set; }
        }
}
