namespace TechLife.Models.DTOs
{
    public class MaintDashboardViewModel
    {
        public int TotalRequestsToday { get; set; }
        public int CompletedRequestsToday { get; set; }
        public List<Service> RequestHistory { get; set; }
    }
}
