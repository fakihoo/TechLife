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
    public class SimServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SimServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MtcIndex()
        {
            List<SimService> objSimService = (List<SimService>)await _unitOfWork.SimService.GetAllAsync();
            return View(objSimService);
        }

        public async Task<IActionResult> AlfaIndex()
        {
            List<SimService> objSimService = (List<SimService>)await _unitOfWork.SimService.GetAllAsync();
            return View(objSimService);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SimService SimToDelete = await _unitOfWork.SimService.GetAsync(u => u.SimServiceId == id);

            if (SimToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.SimService.Remove(SimToDelete);
            _unitOfWork.Save();

            if (SimToDelete.SimType.Equals("Mtc"))
            {
                return RedirectToAction("MtcIndex");
            }
            else
            {
                return RedirectToAction("AlfaIndex");
            }
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SimService Obj)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.SimService.AddAsync(Obj);
                _unitOfWork.Save();

                if (Obj.SimType.Equals("Mtc"))
                {
                    return RedirectToAction("MtcIndex");
                }
                else
                {
                    return RedirectToAction("AlfaIndex");
                }
            }
            return View();
        }
    }
}