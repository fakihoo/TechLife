using Microsoft.AspNetCore.Mvc;

namespace TechLife.Areas.Customer.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
