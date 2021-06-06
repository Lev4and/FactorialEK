using FactorialEK.Model.Database;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FactorialEK.AspNetCore.Components
{
    public class LatestsNews : ViewComponent
    {
        private readonly DataManager _dataManager;

        public LatestsNews(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IViewComponentResult Invoke()
        {
            return View("Default", _dataManager.News.GetNews(2, 1).ToList());
        }
    }
}
