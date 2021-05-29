using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;
using FactorialEK.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FactorialEK.Model.Database.Repositories.EntityFramework
{
    public class EFCategoryPhotosManufacturingOrServiceRepository : ICategoryPhotosManufacturingOrServiceRepository
    {
        private readonly FactorialEKDbContext _context;

        public EFCategoryPhotosManufacturingOrServiceRepository(FactorialEKDbContext context)
        {
            _context = context;
        }

        public bool ContainsCategoryPhotoManufacturingOrServiceByCategoryId(Guid categoryId)
        {
            return _context.CategoryPhotosManufacturingOrService.SingleOrDefault(photo =>
                photo.CategoryManufacturingOrServiceId == categoryId) != null;
        }

        public bool SaveCategoryPhotoManufacturingOrService(CategoryPhotoManufacturingOrService entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsCategoryPhotoManufacturingOrServiceByCategoryId(entity.CategoryManufacturingOrServiceId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetCategoryPhotoManufacturingOrServiceByCategoryId(entity.CategoryManufacturingOrServiceId);

                if (oldVersionEntity.CategoryManufacturingOrServiceId != entity.CategoryManufacturingOrServiceId)
                {
                    if (!ContainsCategoryPhotoManufacturingOrServiceByCategoryId(entity.CategoryManufacturingOrServiceId))
                    {
                        _context.Entry(entity).State = EntityState.Modified;
                        _context.SaveChanges();

                        return true;
                    }
                }
                else
                {
                    _context.Entry(entity).State = EntityState.Modified;
                    _context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public CategoryPhotoManufacturingOrService GetCategoryPhotoManufacturingOrServiceById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.CategoryPhotosManufacturingOrService.SingleOrDefault(photo => photo.Id == id);
            }
            else
            {
                return _context.CategoryPhotosManufacturingOrService.AsNoTracking()
                    .SingleOrDefault(photo => photo.Id == id);
            }
        }

        public CategoryPhotoManufacturingOrService GetCategoryPhotoManufacturingOrServiceByCategoryId(Guid categoryId,
            bool track = false)
        {
            if (track)
            {
                return _context.CategoryPhotosManufacturingOrService
                    .SingleOrDefault(photo => photo.CategoryManufacturingOrServiceId == categoryId);
            }
            else
            {
                return _context.CategoryPhotosManufacturingOrService.AsNoTracking()
                    .SingleOrDefault(photo => photo.CategoryManufacturingOrServiceId == categoryId);
            }
        }

        public void DeleteCategoryPhotoManufacturingOrServiceById(Guid id)
        {
            _context.CategoryPhotosManufacturingOrService.Remove(GetCategoryPhotoManufacturingOrServiceById(id));
            _context.SaveChanges();
        }
    }
}