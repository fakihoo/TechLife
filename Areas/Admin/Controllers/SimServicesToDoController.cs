using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Models;
using TechLife.Models.DTOs;
using TechLife.Repository;
using TechLife.Repository.IRepository;
using TechLife.Utility;

namespace TechLife.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SimServicesToDoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StripeService _paymentService;
        public SimServicesToDoController(IUnitOfWork unitOfWork, StripeService paymentService)
        {
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
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
                    PhoneNumber = 12345678,   
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
            try
            {
                var session = await _paymentService.CreateCheckoutSession(cartItem.Price, "usd", cartItem.SimServiceId);

                TempData["SimServicesToDo"] = JsonConvert.SerializeObject(cartItem);

                return Redirect(session.Url);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while processing the request: " + ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet]
        [Authorize(Roles = SD.Role_Customer)]
        public async Task<IActionResult> PaymentSuccess()
        {
            // Retrieve the SimServicesToDo data from TempData
            var cartItemJson = TempData["SimServicesToDo"]?.ToString();
            if (cartItemJson == null)
            {
                return NotFound("No data available for the payment.");
            }

            var cartItem = JsonConvert.DeserializeObject<SimServicesToDo>(cartItemJson);

            if (cartItem == null)
            {
                return NotFound("Invalid data for the payment.");
            }

            // Retrieve the purchased service
            var purchasedService = await _unitOfWork.SimService.GetAsync(s => s.SimServiceId == cartItem.SimServiceId);
            if (purchasedService == null)
            {
                return NotFound("Purchased service not found.");
            }

            // Add the request to the SimServiceToDo Db
             _unitOfWork.SimServicesToDo.AddAsync(cartItem);
             _unitOfWork.Save(); // Ensure the change is saved to the database

            // Retrieve all SimService records
            var allServices = await _unitOfWork.SimService.GetAllAsync();

            List<SimService> recommendedBundles = new List<SimService>();

            // Determine recommendation logic based on SimType and SimServiceType
            if (purchasedService.SimType == "Mtc")
            {
                if (purchasedService.SimServiceType == "Dollars")
                {
                    // Recommend Mtc Internet bundles
                    recommendedBundles = allServices
                        .Where(s => s.SimServiceType == "Internet" && s.SimType == "Mtc" && s.Dollars <= purchasedService.Dollars)
                        .OrderByDescending(s => s.Viewed)
                        .ThenBy(s => s.Dollars)
                        .Take(3)
                        .ToList();
                }
                else if (purchasedService.SimServiceType == "Days")
                {
                    // Recommend Mtc Dollars bundles
                    recommendedBundles = allServices
                        .Where(s => s.SimServiceType == "Dollars" && s.SimType == "Mtc")
                        .OrderBy(s => s.Dollars)
                        .Take(3)
                        .ToList();
                }
            }
            else if (purchasedService.SimType == "Alfa")
            {
                if (purchasedService.SimServiceType == "Dollars")
                {
                    // Recommend Alfa Internet bundles
                    recommendedBundles = allServices
                        .Where(s => s.SimServiceType == "Internet" && s.SimType == "Alfa" && s.Price <= purchasedService.Dollars)
                        .OrderByDescending(s => s.Viewed)
                        .ThenBy(s => s.Price)
                        .Take(3)
                        .ToList();
                }
                else if (purchasedService.SimServiceType == "Days")
                {
                    // Recommend Alfa Dollars bundles
                    recommendedBundles = allServices
                        .Where(s => s.SimServiceType == "Dollars" && s.SimType == "Alfa")
                        .OrderBy(s => s.Dollars)
                        .Take(3)
                        .ToList();
                }
            }

            // Debug output
            foreach (var bundle in recommendedBundles)
            {
                Console.WriteLine($"Bundle: {bundle.SimServiceName}, Price: {bundle.Price}, Dollars: {bundle.Dollars}");
            }

            // Prepare the view model
            var viewModel = new PaymentSuccessViewModel
            {
                PurchasedService = purchasedService,
                RecommendedBundles = recommendedBundles
            };

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult PaymentFailed()
        {
            TempData["error"] = "Payment failed or was canceled. Please try again.";
            return RedirectToAction("Index", "SimService", new { area = "Customer" });
        }
        public async Task<IActionResult> ViewBundle(int id)
        {
            var bundle = await _unitOfWork.SimService.GetAsync(s => s.SimServiceId == id);

            if (bundle == null)
            {
                return NotFound("Bundle not found.");
            }

            // Increment the viewed count
            bundle.Viewed += 1;
            await _unitOfWork.SaveAsync();

            // Retrieve all services asynchronously
            var allServices = await _unitOfWork.SimService.GetAllAsync();

            // Filter recommended bundles in memory
            var recommendedBundles = allServices
                .Where(s => s.SimServiceType == "Internet" && s.SimType == bundle.SimType && s.SimServiceId != id)
                .Take(3)
                .ToList();

            // Create the view model
            var viewModel = new ViewBundleViewModel
            {
                MainBundle = bundle,
                RecommendedBundles = recommendedBundles
            };

            return View(viewModel);
        }
    }
}