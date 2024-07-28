using Microsoft.AspNetCore.Mvc;

namespace Mrp2.Controllers
{
    public class KpisController : Controller
    {
        public IActionResult Index()
        {
            return View("Kpis");
        }
    }
}
