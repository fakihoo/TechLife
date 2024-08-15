namespace TechLife.Models.DTOs
{
    public class PaymentSuccessViewModel
    {
        public SimService PurchasedService { get; set; }
        public IEnumerable<SimService> RecommendedBundles { get; set; }
    }
}
