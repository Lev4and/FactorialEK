using FactorialEK.Model.Database;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FactorialEK.AspNetCore.Components
{
    public class CategoriesManufacturingOrService : ViewComponent
    {
        private readonly DataManager _dataManager;

        public CategoriesManufacturingOrService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IViewComponentResult Invoke()
        {
            return View("Default", _dataManager.CategoriesManufacturingOrService.GetCategoriesManufacturingOrService().ToList());
        }
    }
}
