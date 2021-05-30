using FactorialEK.AspNetCore.Models;
using FactorialEK.Model.Database;
using FactorialEK.Model.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FactorialEK.AspNetCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly DataManager _dataManager;

        public NewsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult Index()
        {
            var viewModel = new NewsViewModel()
            {
                News = _dataManager.News.GetNews().ToList()
            };

            return View(viewModel);
        }

        public IActionResult Save()
        {
            return View(new News() { CreatedAt = DateTime.Now });
        }

        [Route("~/Admin/News/Save/{id}")]
        public IActionResult Save(Guid id)
        {
            return View(_dataManager.News.GetNewsById(id));
        }

        [HttpPost]
        public IActionResult Save(News news)
        {
            if (ModelState.IsValid)
            {
                if (_dataManager.News.SaveNews(news))
                {
                    return RedirectToAction("Index");
                }
            }

            return View(news);
        }

        [Route("~/Admin/News/Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _dataManager.News.DeleteNewsById(id);

            return RedirectToAction("Index");
        }
    }
}
