using Microsoft.AspNetCore.Mvc;

namespace FactorialEK.AspNetCore.Controllers
{
    public class AccountController : Controller
    {
        [Route("~/Account/Login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}