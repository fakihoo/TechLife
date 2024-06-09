namespace TechLife.Models.DTOs
{
    public record TopNSoldItemModel(string ShopStoreName, string Description, int TotalUnitSold);
    public record TopNSoldItemsVm(DateTime StartDate, DateTime EndDate, IEnumerable<TopNSoldItemModel> TopNSoldItems);
}
