using FactorialEK.AspNetCore.Models;
using FactorialEK.AspNetCore.Service;
using FactorialEK.Model.Database;
using FactorialEK.Model.Database.Entities;
using FactorialEK.Model.Transliteration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactorialEK.AspNetCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ManufacturingOrServicesController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly UploadFileService _uploadFileService;

        public ManufacturingOrServicesController(DataManager dataManager, UploadFileService uploadFileService)
        {
            _dataManager = dataManager;
            _uploadFileService = uploadFileService;
        }

        public IActionResult Index()
        {
            var viewModel = new ManufacturingOrServicesViewModel()
            {
                ManufacturingOrServices = _dataManager.ManufacturingOrServices.GetManufacturingOrServices().ToList()
            };

            return View(viewModel);
        }

        private void NormalizeManufacturingOrServiceMainPhoto(ManufacturingOrServiceMainPhoto mainPhoto, IFormFile uploadedFile, string manufacturingOrServiceName)
        {
            if (mainPhoto != null)
            {
                if(uploadedFile == null)
                {
                    if (string.IsNullOrEmpty(mainPhoto.Url))
                    {
                        mainPhoto = null;
                    }
                }
                else
                {
                    mainPhoto.Url = $"images/upload/manufacturing-or-services/{manufacturingOrServiceName.Unidecode()}/{uploadedFile.FileName}";
                }
            }
        }

        private void NormalizeManufacturingOrServicePhotos(ManufacturingOrService model, List<IFormFile> uploadedFiles, string manufacturingOrServiceName)
        {
            if ((model.Photos != null ? model.Photos.Count == 0 : true) && (uploadedFiles == null ? true : uploadedFiles.Count == 0))
            {
                model.Photos = null;
            }
            else
            {
                if (model.Photos == null)
                {
                    model.Photos = new List<ManufacturingOrServicePhoto>();
                }

                foreach (var file in uploadedFiles)
                {
                    model.Photos.Add(new ManufacturingOrServicePhoto()
                    {
                        Url = $"images/upload/manufacturing-or-services/{manufacturingOrServiceName.Unidecode()}/{file.FileName}"
                    });
                }
            }
        }

        private void NormalizeManufacturingOrServiceInformation(ManufacturingOrServiceInformation information)
        {
            if (information != null)
            {
                if (string.IsNullOrEmpty(information.ShortDescription))
                {
                    information = null;
                }
            }
        }

        private void NormalizeManufacturingOrServicePrice(ManufacturingOrServicePrice price)
        {
            if (price != null)
            {
                if (price.LowerLimitPrice == null && price.UpperLimitPrice == null && price.SecondPrice == null 
                    && !price.IsIndividualCalculation && !price.IsUponRequest)
                {
                    price = null;
                }
            }
        }

        private void SaveManufacturingOrServiceMainPhoto(Guid manufacturingOrServiceId, ManufacturingOrServiceMainPhoto mainPhoto)
        {
            if (mainPhoto != null)
            {
                mainPhoto.ManufacturingOrServiceId = manufacturingOrServiceId;

                _dataManager.ManufacturingOrServiceMainPhotos.SaveManufacturingOrServiceMainPhoto(mainPhoto);
            }
        }

        private void SaveManufacturingOrServicePhotos(Guid manufacturingOrServiceId, ICollection<ManufacturingOrServicePhoto> photos)
        {
            if (manufacturingOrServiceId != default)
            {
                _dataManager.ManufacturingOrServicePhotos
                    .DeleteAllManufacturingOrServicePhotosByManufacturingOrServiceId(manufacturingOrServiceId);
            }

            if (photos != null)
            {
                foreach (var photo in photos)
                {
                    photo.Id = default;
                    photo.ManufacturingOrServiceId = manufacturingOrServiceId;

                    _dataManager.ManufacturingOrServicePhotos.SaveManufacturingOrServicePhoto(photo);
                }
            }
        }

        private void SaveManufacturingOrServiceInformation(Guid manufacturingOrServiceId, ManufacturingOrServiceInformation information)
        {
            if (information != null)
            {
                information.ManufacturingOrServiceId = manufacturingOrServiceId;

                _dataManager.ManufacturingOrServiceInformation.SaveManufacturingOrServiceInformation(information);
            }
        }

        private void SaveManufacturingOrServicePrice(Guid manufacturingOrServiceId, ManufacturingOrServicePrice price)
        {
            if (price != null)
            {
                price.ManufacturingOrServiceId = manufacturingOrServiceId;

                _dataManager.ManufacturingOrServicePrices.SaveManufacturingOrServicePrice(price);
            }
        }

        public IActionResult Save()
        {
            return View(new ManufacturingOrServiceViewModel()
            {
                ManufacturingOrService = new ManufacturingOrService()
                {
                    Information = new ManufacturingOrServiceInformation(),
                    MainPhoto = new ManufacturingOrServiceMainPhoto(),
                    Photos = new List<ManufacturingOrServicePhoto>(),
                    Category = new CategoryManufacturingOrService(),
                    AddedAt = DateTime.Now
                },
                Categories = _dataManager.CategoriesManufacturingOrService.GetCategoriesManufacturingOrService().ToList()
            });
        }

        [Route("~/Admin/ManufacturingOrServices/Save/{id}")]
        public IActionResult Save(Guid id)
        {
            var viewModel = new ManufacturingOrServiceViewModel()
            {
                ManufacturingOrService = _dataManager.ManufacturingOrServices.GetManufacturingOrServiceById(id),
                Categories = _dataManager.CategoriesManufacturingOrService.GetCategoriesManufacturingOrService().ToList()
            };

            if(viewModel.ManufacturingOrService.Information == null)
            {
                viewModel.ManufacturingOrService.Information = new ManufacturingOrServiceInformation();
            }

            if (viewModel.ManufacturingOrService.MainPhoto == null)
            {
                viewModel.ManufacturingOrService.MainPhoto = new ManufacturingOrServiceMainPhoto();
            }

            if (viewModel.ManufacturingOrService.Photos == null)
            {
                viewModel.ManufacturingOrService.Photos = new List<ManufacturingOrServicePhoto>();
            }

            if (viewModel.ManufacturingOrService.Price == null)
            {
                viewModel.ManufacturingOrService.Price = new ManufacturingOrServicePrice();
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ManufacturingOrServiceViewModel viewModel, IFormFile uploadedFile, List<IFormFile> uploadedFiles)
        {
            var manufacturingOrService = viewModel.ManufacturingOrService;

            NormalizeManufacturingOrServicePrice(manufacturingOrService.Price);
            NormalizeManufacturingOrServiceInformation(manufacturingOrService.Information);
            NormalizeManufacturingOrServicePhotos(manufacturingOrService, uploadedFiles, manufacturingOrService.Name);
            NormalizeManufacturingOrServiceMainPhoto(manufacturingOrService.MainPhoto, uploadedFile, manufacturingOrService.Name);

            if (uploadedFile != null)
            {
                await _uploadFileService.UploadFileAsync(uploadedFile, $"images/upload/manufacturing-or-services/{manufacturingOrService.Name.Unidecode()}");
            }

            if (uploadedFiles != null)
            {
                await _uploadFileService.UploadFilesAsync(uploadedFiles, $"images/upload/manufacturing-or-services/{manufacturingOrService.Name.Unidecode()}");
            }

            if (_dataManager.ManufacturingOrServices.SaveManufacturingOrService(viewModel.ManufacturingOrService))
            {
                SaveManufacturingOrServiceInformation(manufacturingOrService.Id, manufacturingOrService.Information);
                SaveManufacturingOrServiceMainPhoto(manufacturingOrService.Id, manufacturingOrService.MainPhoto);
                SaveManufacturingOrServicePhotos(manufacturingOrService.Id, manufacturingOrService.Photos);
                SaveManufacturingOrServicePrice(manufacturingOrService.Id, manufacturingOrService.Price);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [Route("~/Admin/ManufacturingOrServices/Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _dataManager.ManufacturingOrServices.DeleteManufacturingOrServiceById(id);

            return RedirectToAction("Index");
        }
    }
}
