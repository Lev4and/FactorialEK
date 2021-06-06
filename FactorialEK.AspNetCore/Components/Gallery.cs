using FactorialEK.Model.Database;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FactorialEK.AspNetCore.Components
{
    public class Gallery : ViewComponent
    {
        private readonly DataManager _dataManager;

        public Gallery(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IViewComponentResult Invoke()
        {
            return View("Default", _dataManager.GalleryPhotos.GetGalleryPhotos().ToList());
        }
    }
}
