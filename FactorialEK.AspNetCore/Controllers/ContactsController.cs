using Microsoft.AspNetCore.Mvc;

namespace FactorialEK.AspNetCore.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
