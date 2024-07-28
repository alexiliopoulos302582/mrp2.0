using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mrp2.Controllers
{
    
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();// This will render the view at Views/Home/Index.cshtml
        }
        [Authorize]
        public IActionResult LandingPage()
        {
            return View(); // This will render the view located at Views/Home/LandingPage.cshtml
        }





    }
}
