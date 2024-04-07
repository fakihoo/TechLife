namespace TechLife.Models.DTOs
{
    public class ShopDisplay
    {
        public IEnumerable<ShopStore> ShopStores { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string STerm { get; set; } = "";
        public int GenreId { get; set; } = 0;
    }
}
