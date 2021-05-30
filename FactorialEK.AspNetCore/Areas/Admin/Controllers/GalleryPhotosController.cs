using FactorialEK.AspNetCore.Models;
using FactorialEK.AspNetCore.Service;
using FactorialEK.Model.Database;
using FactorialEK.Model.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FactorialEK.AspNetCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class GalleryPhotosController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly UploadFileService _uploadFileService;

        public GalleryPhotosController(DataManager dataManager, UploadFileService uploadFileService)
        {
            _dataManager = dataManager;
            _uploadFileService = uploadFileService;
        }

        public IActionResult Index()
        {
            var viewModel = new GalleryPhotosViewModel()
            {
                GalleryPhotos = _dataManager.GalleryPhotos.GetGalleryPhotos().ToList()
            };

            return View(viewModel);
        }

        public IActionResult Save()
        {
            return View(new GalleryPhoto() { CreatedAt = DateTime.Now });
        }

        [Route("~/Admin/GalleryPhotos/Save/{id}")]
        public IActionResult Save(Guid id)
        {
            return View(_dataManager.GalleryPhotos.GetGalleryPhotoById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save(GalleryPhoto galleryPhoto, IFormFile uploadedFile)
        {
            if(uploadedFile != null)
            {
                if(await _uploadFileService.UploadFileAsync(uploadedFile, $"images/upload/gallery"))
                {
                    galleryPhoto.Url = $"images/upload/gallery/{uploadedFile.FileName}";

                    if (_dataManager.GalleryPhotos.SaveGalleryPhoto(galleryPhoto))
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(galleryPhoto);
        }

        [Route("~/Admin/GalleryPhotos/Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _dataManager.GalleryPhotos.DeleteGalleryPhotoById(id);

            return RedirectToAction("Index");
        }
    }
}
