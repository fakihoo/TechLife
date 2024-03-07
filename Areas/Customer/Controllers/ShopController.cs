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
    public class ShopController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShopController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            List<Shop> objShopList = (List<Shop>)await _unitOfWork.Shop.GetAllAsync();
            return View(objShopList);
        }

        public async Task<IActionResult> Views(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shop ShopFromDB = await _unitOfWork.Shop.GetAsync(u => u.ShopId == id);
            if (ShopFromDB == null)
            {
                return NotFound();
            }

            return View(ShopFromDB);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Shop Obj)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Shop.AddAsync(Obj);
                 _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shop shopToDelete = await _unitOfWork.Shop.GetAsync(u => u.ShopId == id);

            if (shopToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.Shop.Remove(shopToDelete);
             _unitOfWork.Save();

            return RedirectToAction("Index");
        }
    }
}