using Microsoft.CodeAnalysis.Options;
using System.ComponentModel.DataAnnotations;

namespace TechLife.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string CustName { get; set; }
        public string Location { get; set; }
        public string ServiceType { get; set; }
        public string deviceModel { get; set; }
        public string message { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public Service()
        {
            CreatedAt = DateTime.Now;
            IsCompleted = false; 
        }

    }
}
