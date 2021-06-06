using Microsoft.AspNetCore.Mvc;

namespace FactorialEK.AspNetCore.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        public IActionResult ForDistributors()
        {
            return View();
        }

        public IActionResult Certificates()
        {
            return View();
        }
    }
}
