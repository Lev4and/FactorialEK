using Microsoft.AspNetCore.Mvc;

namespace FactorialEK.AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Администратор"))
                {
                    return Redirect("~/Admin/Home/Index/");
                }
            }

            
            return View();
        }
    }
}
