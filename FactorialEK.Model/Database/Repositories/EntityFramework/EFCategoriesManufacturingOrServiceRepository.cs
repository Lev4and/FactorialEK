using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;
using FactorialEK.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FactorialEK.Model.Database.Repositories.EntityFramework
{
    public class EFCategoriesManufacturingOrServiceRepository : ICategoriesManufacturingOrServiceRepository
    {
        private readonly FactorialEKDbContext _context;
        private ICategoriesManufacturingOrServiceRepository _categoriesManufacturingOrServiceRepositoryImplementation;

        public EFCategoriesManufacturingOrServiceRepository(FactorialEKDbContext context)
        {
            _context = context;
        }

        public bool ContainsCategoryManufacturingOrServiceByName(string name)
        {
            return _context.CategoriesManufacturingOrService.SingleOrDefault(category => category.Name == name) != null;
        }

        public bool SaveCategoryManufacturingOrService(CategoryManufacturingOrService entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsCategoryManufacturingOrServiceByName(entity.Name))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetCategoryManufacturingOrServiceById(entity.Id);

                if (oldVersionEntity.Name != entity.Name)
                {
                    if (!ContainsCategoryManufacturingOrServiceByName(entity.Name))
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

        public int GetCountCategoriesManufacturingOrService()
        {
            return _context.CategoriesManufacturingOrService.Count();
        }

        public CategoryManufacturingOrService GetCategoryManufacturingOrServiceById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.CategoriesManufacturingOrService
                    .Include(category => category.Photo)
                    .SingleOrDefault(category => category.Id == id);
            }
            else
            {
                return _context.CategoriesManufacturingOrService.AsNoTracking()
                    .Include(category => category.Photo)
                    .SingleOrDefault(category => category.Id == id);
            }
        }

        public CategoryManufacturingOrService GetCategoryManufacturingOrServiceByName(string name, bool track = false)
        {
            if (track)
            {
                return _context.CategoriesManufacturingOrService
                    .Include(category => category.Photo)
                    .SingleOrDefault(category => category.Name == name);
            }
            else
            {
                return _context.CategoriesManufacturingOrService.AsNoTracking()
                    .Include(category => category.Photo)
                    .SingleOrDefault(category => category.Name == name);
            }
        }

        public IQueryable<CategoryManufacturingOrService> GetCategoriesManufacturingOrService(bool track = false)
        {
            if (track)
            {
                return _context.CategoriesManufacturingOrService
                    .Include(category => category.Photo);
            }
            else
            {
                return _context.CategoriesManufacturingOrService
                    .Include(category => category.Photo)
                    .AsNoTracking();
            }
        }

        public IQueryable<CategoryManufacturingOrService> GetCategoriesManufacturingOrService(int itemsPerPage, int numberPage, bool track = false)
        {
            if (track)
            {
                return _context.CategoriesManufacturingOrService
                    .Include(category => category.Photo)
                    .OrderBy(category => category.Id)
                    .Skip((numberPage - 1) * itemsPerPage)
                    .Take(itemsPerPage);
            }
            else
            {
                return _context.CategoriesManufacturingOrService
                    .Include(category => category.Photo)
                    .OrderBy(category => category.Id)
                    .Skip((numberPage - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .AsNoTracking();
            }
        }

        public void DeleteCategoryManufacturingOrServiceById(Guid id)
        {
            _context.CategoriesManufacturingOrService.Remove(GetCategoryManufacturingOrServiceById(id));
            _context.SaveChanges();
        }
    }
}