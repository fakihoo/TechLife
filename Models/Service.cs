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

    }
}
