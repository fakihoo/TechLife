using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.Models;
using TechLife.Repository;
using TechLife.Repository.IRepository;

namespace TechLife.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            List<Cart> objCartList = (List<Cart>)await _unitOfWork.Cart.GetAllAsync();
            return View(objCartList);
        }

        [Authorize(Roles = SD.Role_Customer)]
        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Shop productToAdd = await _unitOfWork.Shop.GetAsync(p => p.ShopId == id.Value);

            if (productToAdd != null)
            {
                Cart cartItem = new Cart
                {
                    CartName = productToAdd.Name,
                    CartPrice = productToAdd.Price,
                    CartQuantity = 1,
                    CartSubTotal = productToAdd.Price * 1,
                    ImgURL = productToAdd.ImgURL,
                    ShopId = productToAdd.ShopId,
                };

                await _unitOfWork.Cart.AddAsync(cartItem);
                _unitOfWork.Save();

                return View(cartItem);
            }

            return NotFound();
        }

        [Authorize(Roles = SD.Role_Customer)]
        [HttpPost]
        public async Task<IActionResult> Add(Cart cartItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (cartItem != null)
                    {
                        await _unitOfWork.Cart.AddAsync(cartItem);
                        _unitOfWork.Save();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return BadRequest("Invalid data received for cart item.");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "An error occurred while processing your request.");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Cart? CartFromDb = await _unitOfWork.Cart.GetAsync(u => u.CartID == id);

            if (CartFromDb == null)
                return NotFound();

            return View(CartFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            Cart? obj = await _unitOfWork.Cart.GetAsync(u => u.CartID == id);

            if (obj == null)
                return NotFound();

            _unitOfWork.Cart.Remove(obj);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }
    }
}