using FactorialEK.Model.Database;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FactorialEK.AspNetCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataManager _dataManager;

        public ProductController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Route("~/Product/Details/{id}")]
        public IActionResult Details(Guid id)
        {
            return View(_dataManager.ManufacturingOrServices.GetManufacturingOrServiceById(id));
        }
    }
}