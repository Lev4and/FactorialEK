using FactorialEK.Model.Database;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FactorialEK.AspNetCore.Components
{
    public class LatestsManufacturingOrServices : ViewComponent
    {
        private readonly DataManager _dataManager;

        public LatestsManufacturingOrServices(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IViewComponentResult Invoke()
        {
            return View("Default", _dataManager.ManufacturingOrServices.GetLatestManufacturingOrServices(10).ToList());
        }
    }
}
