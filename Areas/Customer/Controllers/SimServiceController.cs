using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.Models;
using TechLife.Repository;
using TechLife.Repository.IRepository;
using X.PagedList;
using X.PagedList.Extensions;

namespace TechLife.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class SimServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
        public SimServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> MtcIndex(string searchTerm, int page = 1)
        {
            var simServices = (await _unitOfWork.SimService.GetAllAsync())
                .Where(s => s.SimType.Equals("Mtc", StringComparison.OrdinalIgnoreCase))
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                simServices = simServices.Where(s => s.SimServiceName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Pagination
            int pageSize = 12;
            var totalItems = simServices.Count();
            var pagedItems = simServices
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new PaginatedList<SimService>(pagedItems, totalItems, page, pageSize);

            // Pass search term through ViewData
            ViewData["SearchTerm"] = searchTerm;

            return View(model);
        }
        public async Task<IActionResult> AlfaIndex(string searchTerm, int page = 1)
        {
            var simServices = (await _unitOfWork.SimService.GetAllAsync())
                .Where(s => s.SimType.Equals("Alfa", StringComparison.OrdinalIgnoreCase))
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                simServices = simServices.Where(s => s.SimServiceName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Pagination
            int pageSize = 12;
            var totalItems = simServices.Count();
            var pagedItems = simServices
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new PaginatedList<SimService>(pagedItems, totalItems, page, pageSize);

            // Pass search term through ViewData
            ViewData["SearchTerm"] = searchTerm;

            return View(model);
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

            return RedirectToAction("Manage");
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(SimService obj, IFormFile ImgFile)
        {
            // Process the file upload
            if (ImgFile != null && ImgFile.Length > 0)
            {
                // Ensure the images directory exists
                Directory.CreateDirectory(_imagePath);

                // Generate a unique file name and save the image
                var fileName = Path.GetFileName(ImgFile.FileName);
                var filePath = Path.Combine(_imagePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImgFile.CopyToAsync(stream);
                }

                obj.ImgUrl = $"img/{fileName}";
            }
            else
            {
                // Handle case where no image file is uploaded
                // Optionally, you can set a default image URL or handle this scenario as needed
                obj.ImgUrl = "/images/default.jpg";
            }
            await _unitOfWork.SimService.AddAsync(obj);
            _unitOfWork.Save();

            return RedirectToAction("Manage");
        }
        public async Task<IActionResult> Manage(int page = 1)
        {
            int pageSize = 10; // Number of items per page
            var simServices = (await _unitOfWork.SimService.GetAllAsync()).ToList();

            var totalItems = simServices.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var pagedItems = simServices
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new PaginatedList<SimService>(pagedItems, totalItems, page, pageSize);

            return View(model);
        }
        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simService = await _unitOfWork.SimService.GetAsync(s => s.SimServiceId == id);

            if (simService == null)
            {
                return NotFound();
            }

            return View(simService);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, SimService simService, IFormFile ImgFile)
        {
            // Retrieve the existing entity from the database
            var existingService = await _unitOfWork.SimService.GetAsync(s => s.SimServiceId == id);

            if (existingService == null)
            {
                return NotFound();
            }

            // Update properties of the existing entity
            existingService.SimServiceName = simService.SimServiceName;
            existingService.Price = simService.Price;
            existingService.Dollars = simService.Dollars;
            existingService.Amount = simService.Amount;
            existingService.SimType = simService.SimType;
            existingService.SimServiceType = simService.SimServiceType;
            existingService.PhoneNumber = simService.PhoneNumber;

            // Check if a new image file is provided
            if (ImgFile != null && ImgFile.Length > 0)
            {
                // Ensure the images directory exists
                Directory.CreateDirectory(_imagePath);

                // Generate a unique file name and save the image
                var fileName = Path.GetFileName(ImgFile.FileName);
                var filePath = Path.Combine(_imagePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImgFile.CopyToAsync(stream);
                }

                // Update the image URL
                existingService.ImgUrl = $"img/{fileName}";
            }
            // If no new image is uploaded, retain the existing image URL

            // Update the sim service in the database
            _unitOfWork.SimService.Update(existingService);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Manage));
        }

        private bool SimServiceExists(int id)
        {
            var service = _unitOfWork.SimService.GetAsync(s => s.SimServiceId == id).Result;
            return service != null;
        }

    }
}