using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TechLife.Data;
using TechLife.Models.DTOs;

namespace TechLife.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;
        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TopNSoldItemModel>> GetTopNSellingBooksByDate(DateTime startDate, DateTime endDate)
        {
            var startDateParam = new SqlParameter("@startDate", startDate);
            var endDateParam = new SqlParameter("@endDate", endDate);
            var topFiveSoldItems = await _context.Database.SqlQueryRaw<TopNSoldItemModel>("exec Usp_GetTopNSellingItemsByDate @startDate,@endDate", startDateParam, endDateParam).ToListAsync();
            return topFiveSoldItems;
        }
        public async Task<List<TopBuyer>> GetTopBuyersByOrderCount(int topN)
        {
            var topBuyers = await _context.Orders
                .Where(o => !o.isDeleted)
                .GroupBy(o => new { o.UserId, o.Name, o.Email, o.Address })
                .Select(g => new TopBuyer
                {
                    UserId = g.Key.UserId,
                    OrderCount = g.Count(),
                    Name = g.Key.Name,
                    Email = g.Key.Email,
                    Address = g.Key.Address
                })
                .OrderByDescending(tb => tb.OrderCount)
                .Take(topN)
                .ToListAsync();

            return topBuyers;
        }

        public async Task<List<TopBuyer>> GetTopBuyersByTotalAmountSpent(int topN)
        {
            var topBuyers = await _context.Orders
        .Where(o => !o.isDeleted)
        .SelectMany(o => o.OrderDetail.Select(od => new { o.UserId, o.Name, o.Email, o.Address, TotalAmount = od.UnitPrice * od.Quantity }))
        .GroupBy(x => new { x.UserId, x.Name, x.Email, x.Address })
        .Select(g => new TopBuyer
        {
            UserId = g.Key.UserId,
            Name = g.Key.Name,
            Email = g.Key.Email,
            Address = g.Key.Address,
            TotalAmountSpent = (decimal)g.Sum(x => x.TotalAmount)
        })
        .OrderByDescending(tb => tb.TotalAmountSpent)
        .Take(topN)
        .ToListAsync();

            return topBuyers;
        }

    }

    public interface IReportRepository
    {
        Task<IEnumerable<TopNSoldItemModel>> GetTopNSellingBooksByDate(DateTime startDate, DateTime endDate);
        Task<List<TopBuyer>> GetTopBuyersByTotalAmountSpent(int topN);
        Task<List<TopBuyer>> GetTopBuyersByOrderCount(int topN);
    }
}
