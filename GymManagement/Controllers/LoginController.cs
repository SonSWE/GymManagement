using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
