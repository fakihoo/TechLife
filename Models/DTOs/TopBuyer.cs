namespace TechLife.Models.DTOs
{
    public class TopBuyer
    {
        public string UserId { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalAmountSpent { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
