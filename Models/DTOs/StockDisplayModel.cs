namespace TechLife.Models.DTOs
{
    public class StockDisplayModel
    {
        public int Id { get; set; }
        public int ShopStoreId { get; set; }
        public int Quantity { get; set; }
        public string? ShopeStoreName { get; set; }
    }
}
