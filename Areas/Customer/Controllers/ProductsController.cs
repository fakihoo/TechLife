using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TechLife.Data;
using TechLife.Models;
using TechLife.Repository;
using TechLife.ViewModels;

namespace TechLife.Controllers
{
    [Area("Customer")]
    [Authorize]

    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(ApplicationDbContext context, IProductRepository productRepository, IGenreRepository genreRepository, IWebHostEnvironment webHostEnvironment, IShoppingCartRepository shoppingCartRepository, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _productRepository = productRepository;
            _genreRepository = genreRepository;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Product
        public IActionResult Index()
        {
            var products = _productRepository.GetAllProducts();
            var productViewModels = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                Condition = p.Condition,
                Location = p.Location,
                GenreId = p.GenreId,
                Views = p.Views,
                Likes = p.Likes,
                Saves = p.Saves,
                UserId = p.UserId,
                Contact = p.Contact,
                UploadedAt = p.UploadedAt,
                Images = p.Images.Select(i => new ImageViewModel { Id = i.Id, Url = i.Url }).ToList()
            }).ToList();

            return View(productViewModels);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new ProductViewModel
            {
                Images = new List<ImageViewModel>(),
                GenreList = (await _genreRepository.GetGenres())
                    .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.GenreName })
                    .ToList()
            };

            // Setting the UserId to the currently logged-in user
            var userId = GetUserId();
            viewModel.UserId = userId;

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel viewModel)
        {         
                var product = new Product
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Price = viewModel.Price,
                    Condition = viewModel.Condition,
                    Location = viewModel.Location,
                    GenreId = viewModel.GenreId,
                    Views = viewModel.Views,
                    Likes = viewModel.Likes,
                    Saves = viewModel.Saves,
                    UserId = viewModel.UserId,
                    Contact = viewModel.Contact,
                    UploadedAt = DateTime.Now,
                    Images = new List<Image>() // Initialize to an empty list
                };
                
                if (viewModel.UserId == null)
                {
                product.UserId = GetUserId();
                if(product.UserId == null)
                {
                    throw new InvalidOperationException("UserId not valid");
                }
                }
                if (viewModel.ImageFiles != null && viewModel.ImageFiles.Count > 0)
                {
                    foreach (var file in viewModel.ImageFiles)
                    {
                        var imageUrl = await UploadImage(file);
                        product.Images.Add(new Image { Url = imageUrl });
                    }
                }
                
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
        }

        private async Task<string> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            // Create the directory if it doesn't exist
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Generate a unique file name
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadPath, uniqueFileName);

            // Save the file to the specified path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return the relative URL to the saved image
            return "/img/" + uniqueFileName;
        }
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
        public IActionResult CheckOffer(int id)
        {
            var product = _productRepository.GetProductById(id); // Fetch product from repository
            if (product == null)
            {
                return NotFound(); // Handle case where product is not found
            }
            var user = _userManager.Users.FirstOrDefault(u => u.Id == product.UserId);
            if (user == null)
            {
                return NotFound(); // Handle case where user is not found
            }
            // Retrieve profile picture URL from claims
            var userClaims = _userManager.GetClaimsAsync(user).Result;
            var profilePictureUrlClaim = userClaims.FirstOrDefault(c => c.Type == "ProfilePictureUrl");
            var profilePictureUrl = profilePictureUrlClaim?.Value ?? "/img/default-profile-picture1.jpg";
            // Debugging: Check if product.Images is populated
            if (product.Images == null || !product.Images.Any())
            {
                Console.WriteLine("No images found for product");
            }
            else
            {
                Console.WriteLine($"{product.Images.Count} images found for product");
            }

            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Condition = product.Condition,
                Location = product.Location,
                Contact = product.Contact,
                UploadedAt = product.UploadedAt,
                UserId = product.UserId,
                Images = product.Images.Select(i => new ImageViewModel { Url = i.Url }).ToList(), // Map images accordingly
                UserProfilePictureUrl = profilePictureUrl ?? "/img/default-profile-picture1.jpg",
                UserEmail = user.Email
            };

            // Debugging: Check if viewModel.Images is populated
            if (viewModel.Images == null || !viewModel.Images.Any())
            {
                Console.WriteLine("No images found in viewModel");
            }
            else
            {
                Console.WriteLine($"{viewModel.Images.Count} images found in viewModel");
            }

            return View(viewModel);
        }
        public async Task<IActionResult> MyProducts(string userId)
        {
            if (userId == null)
            {
                // Handle case where userId is null
                return BadRequest("User ID cannot be null");
            }

            var products = await _context.Products
                .Include(p => p.Images)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            var viewModel = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                Condition = p.Condition,
                Location = p.Location,
                GenreId = p.GenreId,
                Views = p.Views,
                Likes = p.Likes,
                Saves = p.Saves,
                UserId = p.UserId,
                Contact = p.Contact,
                UploadedAt = p.UploadedAt,
                Images = p.Images.Select(i => new ImageViewModel { Url = i.Url }).ToList()
            }).ToList();

            return View(viewModel);
        }


    }
}
