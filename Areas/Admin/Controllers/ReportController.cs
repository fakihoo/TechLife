using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLife.Models.DTOs;
using TechLife.Repository;

namespace TechLife.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = SD.Role_Admin)]
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public async Task<ActionResult> TopFiveSellingBooks(DateTime? sDate = null, DateTime? eDate = null)
        {
            try
            {
                // by default, get last 7 days record
                DateTime startDate = sDate ?? DateTime.UtcNow.AddDays(-7);
                DateTime endDate = eDate ?? DateTime.UtcNow;
                var topFiveSellingBooks = await _reportRepository.GetTopNSellingBooksByDate(startDate, endDate);
                var vm = new TopNSoldItemsVm(startDate, endDate, topFiveSellingBooks);
                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Something went wrong";
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> TopBuyersByOrderCount(int topN = 10)
        {
            var topBuyers = await _reportRepository.GetTopBuyersByOrderCount(topN);
            return View(topBuyers);
        }

        public async Task<IActionResult> TopBuyersByTotalAmountSpent(int topN = 10)
        {
            var topBuyers = await _reportRepository.GetTopBuyersByTotalAmountSpent(topN);
            return View(topBuyers);
        }
    }
}
