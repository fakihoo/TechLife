using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Models;
using TechLife.Repository;
using TechLife.Repository.IRepository;

namespace TechLife.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SimServicesToDoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SimServicesToDoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            List<SimServicesToDo> objCartList = (List<SimServicesToDo>)await _unitOfWork.SimServicesToDo.GetAllAsync();
            return View(objCartList);
        }

        [Authorize(Roles = SD.Role_Customer)]
        [HttpGet]
        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SimService simService = await _unitOfWork.SimService.GetAsync(p => p.SimServiceId == id.Value);

            if (simService != null)
            {
                SimServicesToDo cartItem = new SimServicesToDo
                {
                    SimServicesToDoName = simService.SimServiceName,
                    SimType = simService.SimType,
                    Price = (double)simService.Price,
                    CustName = "Default Name",
                    PhoneNumber = 1234567890,
                    SimServiceId = simService.SimServiceId
                };

                return View(cartItem);
            }

            return NotFound();
        }

        [Authorize(Roles = SD.Role_Customer)]
        [HttpPost]
        public async Task<IActionResult> Add(SimServicesToDo cartItem)
        {
            ModelState.Clear();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(cartItem);
            }

            try
            {
                await _unitOfWork.SimServicesToDo.AddAsync(cartItem);
                _unitOfWork.Save();
                TempData["success"] = "Service Was Requested Successfully, The Team Will Respond Within 15 minutes";
                return RedirectToAction("Index", "SimService", new { area = "Customer" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while processing the request: " + ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            SimServicesToDo? CartFromDb = await _unitOfWork.SimServicesToDo.GetAsync(u => u.SimServicesToDoId == id);

            if (CartFromDb == null)
                return NotFound();

            return View(CartFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            SimServicesToDo? obj = await _unitOfWork.SimServicesToDo.GetAsync(u => u.SimServicesToDoId == id);

            if (obj == null)
                return NotFound();

            _unitOfWork.SimServicesToDo.Remove(obj);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }
    }
}