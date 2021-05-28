using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactorialEK.AspNetCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Route("~/Admin")]
        [Route("~/Admin/Home")]
        [Route("~/Admin/Home/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}