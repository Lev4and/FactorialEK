using FactorialEK.AspNetCore.Models;
using FactorialEK.Model.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FactorialEK.AspNetCore.Controllers
{
    public class ShopController : Controller
    {
        private readonly DataManager _dataManager;

        public ShopController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Route("~/Shop/{categoryId}")]
        public IActionResult Index(Guid categoryId)
        {
            var viewModel = new ShopViewModel()
            {
                NumberPage = 1,
                ItemsPerPage = 15,
                CategoryId = categoryId,
                CountItems = _dataManager.ManufacturingOrServices.GetCountManufacturingOrServices(categoryId, ""),
                ManufacturingOrServices = _dataManager.ManufacturingOrServices.GetManufacturingOrServices(categoryId, "", 
                    15, 1).ToList()
            };

            viewModel.GeneratePagination();

            return View(viewModel);
        }

        [Route("~/Shop/{categoryId}/page={page}")]
        public IActionResult Index(Guid categoryId, int page)
        {
            var viewModel = new ShopViewModel()
            {
                NumberPage = page,
                ItemsPerPage = 15,
                CategoryId = categoryId,
                CountItems = _dataManager.ManufacturingOrServices.GetCountManufacturingOrServices(categoryId, ""),
                ManufacturingOrServices = _dataManager.ManufacturingOrServices.GetManufacturingOrServices(categoryId, "",
                    15, page).ToList()
            };

            viewModel.GeneratePagination();

            return View(viewModel);
        }

        [Route("~/Shop/PrevPage/categoryId={categoryId}&currentPage={currentPage}")]
        public IActionResult PrevPage(Guid categoryId, int currentPage)
        {
            return Redirect($"~/Shop/{categoryId}/page={(currentPage > 1 ? currentPage - 1 : 1)}");
        }

        [Route("~/Shop/NextPage/categoryId={categoryId}&currentPage={currentPage}&itemsPerPage={itemsPerPage}&countItems={countItems}")]
        public IActionResult NextPage(Guid categoryId, int currentPage, int itemsPerPage, int countItems)
        {
            var countPages = countItems % itemsPerPage != 0 ? (countItems / itemsPerPage) + 1 : countItems / itemsPerPage;

            return Redirect($"~/Shop/{categoryId}/page={(currentPage < countPages ? currentPage + 1 : countPages)}");
        }
    }
}