using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;
using FactorialEK.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FactorialEK.Model.Database.Repositories.EntityFramework
{
    public class EFManufacturingOrServicesRepository : IManufacturingOrServicesRepository
    {
        private readonly FactorialEKDbContext _context;

        public EFManufacturingOrServicesRepository(FactorialEKDbContext context)
        {
            _context = context;
        }

        public bool ContainsManufacturingOrServiceByCategoryAndName(Guid categoryId, string name)
        {
            return _context.ManufacturingOrServices.SingleOrDefault(manufacturingOrService =>
                manufacturingOrService.CategoryManufacturingOrServiceId == categoryId &&
                manufacturingOrService.Name == name) != null;
        }

        public bool SaveManufacturingOrService(ManufacturingOrService entity)
        {
            if(entity.Id == default)
            {
                if (!ContainsManufacturingOrServiceByCategoryAndName(entity.CategoryManufacturingOrServiceId, entity.Name))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetManufacturingOrServiceById(entity.Id);

                if(oldVersionEntity.CategoryManufacturingOrServiceId != entity.CategoryManufacturingOrServiceId || 
                    oldVersionEntity.Name != entity.Name)
                {
                    if (!ContainsManufacturingOrServiceByCategoryAndName(entity.CategoryManufacturingOrServiceId, entity.Name))
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

        public int GetCountManufacturingOrServices(Guid? categoryId, string searchString)
        {
            return _context.ManufacturingOrServices
                .Include(manufacturingOrService => manufacturingOrService.Price)
                .Include(manufacturingOrService => manufacturingOrService.Category)
                .Where(manufacturingOrService =>
                    (categoryId != null
                        ? manufacturingOrService.CategoryManufacturingOrServiceId == categoryId
                        : true) &&
                    (searchString.Length > 0
                        ? EF.Functions.Like(manufacturingOrService.Name, $"%{searchString}%")
                        : true))
                .Count();
        }

        public ManufacturingOrService GetManufacturingOrServiceById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.ManufacturingOrServices
                    .Include(manufacturingOrService => manufacturingOrService.Price)
                    .Include(manufacturingOrService => manufacturingOrService.Photos)
                    .Include(manufacturingOrService => manufacturingOrService.Category)
                    .Include(manufacturingOrService => manufacturingOrService.MainPhoto)
                    .Include(manufacturingOrService => manufacturingOrService.Information)
                    .SingleOrDefault(manufacturingOrService => manufacturingOrService.Id == id);
            }
            else
            {
                return _context.ManufacturingOrServices
                    .Include(manufacturingOrService => manufacturingOrService.Price)
                    .Include(manufacturingOrService => manufacturingOrService.Photos)
                    .Include(manufacturingOrService => manufacturingOrService.Category)
                    .Include(manufacturingOrService => manufacturingOrService.MainPhoto)
                    .Include(manufacturingOrService => manufacturingOrService.Information)
                    .AsNoTracking().SingleOrDefault(manufacturingOrService => manufacturingOrService.Id == id);
            }
        }

        public IQueryable<ManufacturingOrService> GetManufacturingOrServices(bool track = false)
        {
            if (track)
            {
                return _context.ManufacturingOrServices
                    .Include(manufacturingOrService => manufacturingOrService.Price)
                    .Include(manufacturingOrService => manufacturingOrService.Photos)
                    .Include(manufacturingOrService => manufacturingOrService.Category)
                    .Include(manufacturingOrService => manufacturingOrService.MainPhoto)
                    .Include(manufacturingOrService => manufacturingOrService.Information);
            }
            else
            {
                return _context.ManufacturingOrServices
                    .Include(manufacturingOrService => manufacturingOrService.Price)
                    .Include(manufacturingOrService => manufacturingOrService.Photos)
                    .Include(manufacturingOrService => manufacturingOrService.Category)
                    .Include(manufacturingOrService => manufacturingOrService.MainPhoto)
                    .Include(manufacturingOrService => manufacturingOrService.Information);
            }
        }

        public IQueryable<ManufacturingOrService> GetManufacturingOrServices(Guid? categoryId, string searchString, int itemsPerPage, int numberPage,
            bool track = false)
        {
            if (track)
            {
                return _context.ManufacturingOrServices
                    .Include(manufacturingOrService => manufacturingOrService.Price)
                    .Include(manufacturingOrService => manufacturingOrService.Photos)
                    .Include(manufacturingOrService => manufacturingOrService.Category)
                    .Include(manufacturingOrService => manufacturingOrService.MainPhoto)
                    .Include(manufacturingOrService => manufacturingOrService.Information)
                    .Where(manufacturingOrService =>
                        (categoryId != null
                            ? manufacturingOrService.CategoryManufacturingOrServiceId == categoryId
                            : true) &&
                        (searchString.Length > 0
                            ? EF.Functions.Like(manufacturingOrService.Name, $"%{searchString}%")
                            : true))
                    .OrderBy(manufacturingOrService => manufacturingOrService.Id)
                    .Skip((numberPage - 1) * itemsPerPage)
                    .Take(itemsPerPage);
            }
            else
            {
                return _context.ManufacturingOrServices
                    .Include(manufacturingOrService => manufacturingOrService.Price)
                    .Include(manufacturingOrService => manufacturingOrService.Photos)
                    .Include(manufacturingOrService => manufacturingOrService.Category)
                    .Include(manufacturingOrService => manufacturingOrService.MainPhoto)
                    .Include(manufacturingOrService => manufacturingOrService.Information)
                    .Where(manufacturingOrService =>
                        (categoryId != null
                            ? manufacturingOrService.CategoryManufacturingOrServiceId == categoryId
                            : true) &&
                        (searchString.Length > 0
                            ? EF.Functions.Like(manufacturingOrService.Name, $"%{searchString}%")
                            : true))
                    .OrderBy(manufacturingOrService => manufacturingOrService.Id)
                    .Skip((numberPage - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .AsNoTracking();
            }
        }

        public void DeleteManufacturingOrServiceById(Guid id)
        {
            _context.ManufacturingOrServices.Remove(GetManufacturingOrServiceById(id));
            _context.SaveChanges();
        }
    }
}