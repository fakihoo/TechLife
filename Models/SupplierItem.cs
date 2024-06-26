using System;
using System.ComponentModel.DataAnnotations;

namespace TechLife.Models
{
    public class SupplierItem
    {
        public int SupplierItemId { get; set; }

        [Required(ErrorMessage = "Supplier is required.")]
        public int? SupplierId { get; set; }

        [Required(ErrorMessage = "Shop Store is required.")]
        public int? ShopStoreId { get; set; }

        [Required(ErrorMessage = "Purchase Date is required.")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        public Supplier Supplier { get; set; }
        public ShopStore ShopStore { get; set; }
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
