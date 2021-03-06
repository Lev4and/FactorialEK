using FactorialEK.AspNetCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FactorialEK.AspNetCore.Areas.Admin.Components
{
    public class UserProfile : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserProfile(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name);
            var role = _userManager.GetRolesAsync(user.Result);

            var viewModel = new UserProfileViewModel()
            {
                Name = user.Result.UserName,
                Role = role.Result.First()
            };

            return View("Default", viewModel);
        }
    }
}
