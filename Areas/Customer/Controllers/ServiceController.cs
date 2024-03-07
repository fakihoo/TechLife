using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.Models;
using TechLife.Repository;
using TechLife.Repository.IRepository;

namespace TechLife.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            List<Service> objServiceList = (List<Service>)await _unitOfWork.Service.GetAllAsync();
            return View(objServiceList);
        }

        [Authorize(Roles = SD.Role_Customer)]
        public IActionResult PHIndex()
        {
            return View();
        }

        [Authorize(Roles = SD.Role_Customer)]
        public IActionResult PCIndex()
        {
            return View();
        }

        [Authorize(Roles = SD.Role_Customer)]
        public IActionResult TVIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Service Obj)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Service.AddAsync(Obj);
                _unitOfWork.Save();
                TempData["success"] = "Service Was Requested Successfully, The Team Will Message You Within 15 minutes";
                return RedirectToAction("PHIndex");
            }
            return NotFound();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Service obj = await _unitOfWork.Service.GetAsync(u => u.ServiceId == id);

            if (obj == null)
                return NotFound();

            _unitOfWork.Service.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}