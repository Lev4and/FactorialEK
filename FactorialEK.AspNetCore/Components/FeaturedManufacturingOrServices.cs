using FactorialEK.Model.Database;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FactorialEK.AspNetCore.Components
{
    public class FeaturedManufacturingOrServices : ViewComponent
    {
        private readonly DataManager _dataManager;

        public FeaturedManufacturingOrServices(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IViewComponentResult Invoke()
        {
            return View("Default", _dataManager.ManufacturingOrServices.GetFeaturedManufacturingOrServices(10).ToList());
        }
    }
}
