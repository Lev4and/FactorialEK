using FactorialEK.AspNetCore.Models;
using FactorialEK.AspNetCore.Service;
using FactorialEK.Model.Database;
using FactorialEK.Model.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FactorialEK.AspNetCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly UploadFileService _uploadFileService;

        public CategoriesController(DataManager dataManager, UploadFileService uploadFileService)
        {
            _dataManager = dataManager;
            _uploadFileService = uploadFileService;
        }

        public IActionResult Index()
        {
            var viewModel = new CategoriesViewModel()
            {
                Categories = _dataManager.CategoriesManufacturingOrService.GetCategoriesManufacturingOrService().ToList()
            };

            return View(viewModel);
        }

        public IActionResult Save()
        {
            var viewModel = new CategoryViewModel()
            {
                Name = "",
                PhotoUrl = "",
                Description = "",
                CategoryId = default,
            };

            return View(viewModel);
        }

        [Route("~/Admin/Categories/Save/{id}")]
        public IActionResult Save(Guid id)
        {
            var category = _dataManager.CategoriesManufacturingOrService.GetCategoryManufacturingOrServiceById(id);
            var viewModel = new CategoryViewModel()
            {
                Name = category.Name,
                CategoryId = category.Id,
                PhotoUrl = (category.Photo != null ? category.Photo.Url : ""),
                Description = category.Description
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryViewModel viewModel, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                var phoroUrl = (!string.IsNullOrEmpty(viewModel.PhotoUrl) ? viewModel.PhotoUrl : null);
                var categoty = new CategoryManufacturingOrService()
                {
                    Name = viewModel.Name,
                    Id = viewModel.CategoryId,
                    Description = viewModel.Description
                };

                if(uploadedFile != null)
                {
                    if(await _uploadFileService.UploadFileAsync(uploadedFile, "images/upload/categories/"))
                    {
                        phoroUrl = $"images/upload/categories/{uploadedFile.FileName}";
                    }
                }

                if (_dataManager.CategoriesManufacturingOrService.SaveCategoryManufacturingOrService(categoty))
                {
                    categoty = _dataManager.CategoriesManufacturingOrService
                        .GetCategoryManufacturingOrServiceById(categoty.Id, true);

                    if(categoty.Photo == null)
                    {
                        if (!string.IsNullOrEmpty(phoroUrl))
                        {
                            categoty.Photo = new CategoryPhotoManufacturingOrService();
                            categoty.Photo.CategoryManufacturingOrServiceId = categoty.Id;
                            categoty.Photo.Url = phoroUrl;

                            _dataManager.CategoriesManufacturingOrService.SaveCategoryManufacturingOrService(categoty);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(phoroUrl))
                        {
                            categoty.Photo.Url = phoroUrl;
                        }
                        else
                        {
                            categoty.Photo = null;
                        }

                        _dataManager.CategoriesManufacturingOrService.SaveCategoryManufacturingOrService(categoty);
                    }

                    return RedirectToAction("Index");
                }
            }

            return View(viewModel);
        }

        [Route("~/Admin/Categories/Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _dataManager.CategoriesManufacturingOrService.DeleteCategoryManufacturingOrServiceById(id);

            return RedirectToAction("Index");
        }
    }
}
