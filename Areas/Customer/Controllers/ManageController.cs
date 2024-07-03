using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.IO;
using TechLife.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace TechLife.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ManageController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public ManageController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult UploadProfilePicture()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture(ProfilePictureViewModel model)
        {
            if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var fileName = Path.GetFileName(model.ProfilePicture.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/profile", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ProfilePicture.CopyToAsync(stream);
                    }

                    var profilePictureUrl = "/img/profile/" + fileName;

                    // Add ProfilePictureUrl as a claim
                    var existingClaim = (await _userManager.GetClaimsAsync(user)).FirstOrDefault(c => c.Type == "ProfilePictureUrl");
                    if (existingClaim != null)
                    {
                        await _userManager.RemoveClaimAsync(user, existingClaim);
                    }
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("ProfilePictureUrl", profilePictureUrl));

                    // Update the database
                    var commandText = "UPDATE AspNetUsers SET ProfilePictureUrl = @ProfilePictureUrl WHERE Id = @UserId";
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        using (var command = new SqlCommand(commandText, connection))
                        {
                            command.Parameters.AddWithValue("@ProfilePictureUrl", profilePictureUrl);
                            command.Parameters.AddWithValue("@UserId", user.Id);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
