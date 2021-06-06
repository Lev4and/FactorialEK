using FactorialEK.AspNetCore.Models;
using FactorialEK.Model.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FactorialEK.AspNetCore.Controllers
{
    public class BlogController : Controller
    {
        private readonly DataManager _dataManager;

        public BlogController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult Index()
        {
            var viewModel = new BlogViewModel()
            {
                NumberPage = 1,
                ItemsPerPage = 15,
                CountItems = _dataManager.News.GetCountNews(),
                News = _dataManager.News.GetNews(15, 1).ToList()
            };

            viewModel.GeneratePagination();

            return View(viewModel);
        }

        [Route("~/Blog/page={page}")]
        public IActionResult Index(int page)
        {
            var viewModel = new BlogViewModel()
            {
                NumberPage = page,
                ItemsPerPage = 15,
                CountItems = _dataManager.News.GetCountNews(),
                News = _dataManager.News.GetNews(15, page).ToList()
            };

            viewModel.GeneratePagination();

            return View(viewModel);
        }

        [Route("~/Blog/{id}")]
        public IActionResult Details(Guid id)
        {
            return View(_dataManager.News.GetNewsById(id));
        }

        [Route("~/Shop/PrevPage/currentPage={currentPage}")]
        public IActionResult PrevPage(int currentPage)
        {
            return Redirect($"~/Blog/page={(currentPage > 1 ? currentPage - 1 : 1)}");
        }

        [Route("~/Shop/NextPage/currentPage={currentPage}&itemsPerPage={itemsPerPage}&countItems={countItems}")]
        public IActionResult NextPage(int currentPage, int itemsPerPage, int countItems)
        {
            var countPages = countItems % itemsPerPage != 0 ? (countItems / itemsPerPage) + 1 : countItems / itemsPerPage;

            return Redirect($"~/Blog/page={(currentPage < countPages ? currentPage + 1 : countPages)}");
        }
    }
}