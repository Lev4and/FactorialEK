using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;

namespace FactorialEK.Model.Database.Repositories.Abstract
{
    public interface ICategoryPhotosManufacturingOrServiceRepository
    {
        bool ContainsCategoryPhotoManufacturingOrServiceByCategoryId(Guid categoryId);

        bool SaveCategoryPhotoManufacturingOrService(CategoryPhotoManufacturingOrService entity);
        
        CategoryPhotoManufacturingOrService GetCategoryPhotoManufacturingOrServiceById(Guid id, bool track = false);
        
        CategoryPhotoManufacturingOrService GetCategoryPhotoManufacturingOrServiceByCategoryId(Guid categoryId, bool track = false);

        void DeleteCategoryPhotoManufacturingOrServiceById(Guid id);
    }
}