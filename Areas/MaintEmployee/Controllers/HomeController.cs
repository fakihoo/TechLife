using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLife.Models.DTOs;
using TechLife.Repository;

namespace TechLife.Areas.MaintEmployee.Controllers
{
    [Area("MaintEmployee")]
    [Authorize(Roles = SD.Role_MaintEmployee)]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;

            // Fetch the updated counts
            var totalRequestsToday = (await _unitOfWork.Service.GetAllAsync(s => s.CreatedAt.Date == today)).Count();
            var completedRequestsToday = (await _unitOfWork.Service.GetAllAsync(s => s.CreatedAt.Date == today && s.IsCompleted)).Count();

            var model = new MaintDashboardViewModel
            {
                TotalRequestsToday = totalRequestsToday - completedRequestsToday,
                CompletedRequestsToday = completedRequestsToday,
                RequestHistory = (List<Models.Service>)await _unitOfWork.Service.GetAllAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> TodaysTasks()
        {
            var today = DateTime.Today;
            var tasks = await _unitOfWork.Service.GetAllAsync(s => s.CreatedAt.Date == today && !s.IsCompleted);
            return View(tasks);
        }

        [Authorize(Roles = SD.Role_MaintEmployee)]
        public async Task<IActionResult> MarkAsDone(int serviceId)
        {
            var service = await _unitOfWork.Service.GetAsync(s => s.ServiceId == serviceId);
            if (service != null)
            {
                if (!service.IsCompleted)
                {
                    service.IsCompleted = true;
                    _unitOfWork.Service.Update(service);
                    await _unitOfWork.SaveAsync();

                    // Recalculate the counts
                    var today = DateTime.Today;
                    var totalRequestsToday = (await _unitOfWork.Service.GetAllAsync(s => s.CreatedAt.Date == today)).Count();
                    var completedRequestsToday = (await _unitOfWork.Service.GetAllAsync(s => s.CreatedAt.Date == today && s.IsCompleted)).Count();

                    // Update TempData
                    TempData["TotalRequestsToday"] = totalRequestsToday - completedRequestsToday;
                    TempData["CompletedRequestsToday"] = completedRequestsToday;
                }

                TempData["success"] = "Task marked as done.";
            }

            return RedirectToAction("Index");
        }   
        public async Task<IActionResult> CompletedTasks(DateTime? StartDate, DateTime? EndDate)
        {
            // Fetch all completed tasks
            var completedTasks = await _unitOfWork.Service.GetAllAsync(s => s.IsCompleted);

            // Apply filtering if dates are provided
            if (StartDate.HasValue)
            {
                completedTasks = completedTasks.Where(s => s.CompletedAt >= StartDate.Value);
            }

            if (EndDate.HasValue)
            {
                completedTasks = completedTasks.Where(s => s.CompletedAt <= EndDate.Value);
            }

            return View(completedTasks);
        }




    }
}
