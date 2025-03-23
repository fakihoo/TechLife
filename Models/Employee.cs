using System;
using System.ComponentModel.DataAnnotations;

namespace TechLife.Models
{
    public class Employee
    {
        public int Id { get; set; }

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

        // These fields will be automatically generated
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
