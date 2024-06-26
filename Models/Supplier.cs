using System.ComponentModel.DataAnnotations;

namespace TechLife.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Contact { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        // The SupplierItems collection should not be required.
        public ICollection<SupplierItem> SupplierItems { get; set; } = new List<SupplierItem>();
    }
}
