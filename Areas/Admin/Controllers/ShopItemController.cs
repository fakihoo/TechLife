using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using TechLife.Repository;
using TechLife.Shared;
using Microsoft.AspNetCore.Authorization;
using TechLife.Models.DTOs;
using TechLife.Models;

namespace TechLife.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ShopItemController : Controller
    {
        private readonly IShopItemRepository _shopItemRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IFileService _fileService;

        public ShopItemController(IShopItemRepository bookRepo, IGenreRepository genreRepo, IFileService fileService)
        {
            _shopItemRepo = bookRepo;
            _genreRepo = genreRepo;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _shopItemRepo.GetShops();
            return View(books);
        }

        public async Task<IActionResult> AddShopItem()
        {
            var genreSelectList = (await _genreRepo.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
            });
            ShopStoreDTO ShopItemToAdd = new() { GenreList = genreSelectList };
            return View(ShopItemToAdd);
        }

        [HttpPost]
        public async Task<IActionResult> AddShopItem(ShopStoreDTO ShopItemToADd)
        {
            var genreSelectList = (await _genreRepo.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
            });
            ShopItemToADd.GenreList = genreSelectList;

            if (!ModelState.IsValid)
                return View(ShopItemToADd);

            try
            {
                if (ShopItemToADd.ImageFile != null)
                {
                    if (ShopItemToADd.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException("Image file can not exceed 1 MB");
                    }
                    string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                    string imageName = await _fileService.SaveFile(ShopItemToADd.ImageFile, allowedExtensions);
                    ShopItemToADd.Image = imageName;
                }
                // manual mapping of BookDTO -> Book
                ShopStore shop = new()
                {
                    Id = ShopItemToADd.Id,
                    Name = ShopItemToADd.ShopStoreName,
                    Description = ShopItemToADd.Description,
                    ImgUrl = ShopItemToADd.Image,
                    GenreId = ShopItemToADd.GenreId,
                    price = ShopItemToADd.Price
                };
                await _shopItemRepo.AddShopItem(shop);
                TempData["successMessage"] = "Item is added successfully";
                return RedirectToAction(nameof(AddShopItem));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(ShopItemToADd);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(ShopItemToADd);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on saving data";
                return View(ShopItemToADd);
            }
        }

        public async Task<IActionResult> UpdateShopItem(int id)
        {
            var shop = await _shopItemRepo.GetShopItemById(id);
            if (shop == null)
            {
                TempData["errorMessage"] = $"Item with the id: {id} does not found";
                return RedirectToAction(nameof(Index));
            }
            var genreSelectList = (await _genreRepo.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
                Selected = genre.Id == shop.GenreId
            });
            ShopStoreDTO shopItemToUpdate = new()
            {
                GenreList = genreSelectList,
                ShopStoreName = shop.Name,
                Description = shop.Description,
                GenreId = shop.GenreId,
                Price = shop.price,
                Image = shop.ImgUrl
            };
            return View(shopItemToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShopItem(ShopStoreDTO shopItemToUpdate)
        {
            var genreSelectList = (await _genreRepo.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
                Selected = genre.Id == shopItemToUpdate.GenreId
            });
            shopItemToUpdate.GenreList = genreSelectList;

            if (!ModelState.IsValid)
                return View(shopItemToUpdate);

            try
            {
                string oldImage = "";
                if (shopItemToUpdate.ImageFile != null)
                {
                    if (shopItemToUpdate.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException("Image file can not exceed 1 MB");
                    }
                    string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                    string imageName = await _fileService.SaveFile(shopItemToUpdate.ImageFile, allowedExtensions);
                    // hold the old image name. Because we will delete this image after updating the new
                    oldImage = shopItemToUpdate.Image;
                    shopItemToUpdate.Image = imageName;
                }
                // manual mapping of ShopStoreDTO -> ShopStore
                ShopStore shop = new()
                {
                    Id = shopItemToUpdate.Id,
                    Name = shopItemToUpdate.ShopStoreName,
                    Description = shopItemToUpdate.Description,
                    GenreId = shopItemToUpdate.GenreId,
                    price = shopItemToUpdate.Price,
                    ImgUrl = shopItemToUpdate.Image
                };
                await _shopItemRepo.UpdateShopItem(shop);
                // if image is updated, then delete it from the folder too
                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileService.DeleteFile(oldImage);
                }
                TempData["successMessage"] = "Item is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(shopItemToUpdate);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(shopItemToUpdate);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on saving data";
                return View(shopItemToUpdate);
            }
        }

        public async Task<IActionResult> DeleteShopItem(int id)
        {
            try
            {
                var shop = await _shopItemRepo.GetShopItemById(id);
                if (shop == null)
                {
                    TempData["errorMessage"] = $"Item with the id: {id} does not found";
                }
                else
                {
                    await _shopItemRepo.DeleteShopItem(shop);
                    if (!string.IsNullOrWhiteSpace(shop.ImgUrl))
                    {
                        _fileService.DeleteFile(shop.ImgUrl);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on deleting the data";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
