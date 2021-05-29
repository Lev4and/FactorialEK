using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;

namespace FactorialEK.Model.Database.Repositories.Abstract
{
    public interface ICategoriesManufacturingOrServiceRepository
    {
        bool ContainsCategoryManufacturingOrServiceByName(string name);

        bool SaveCategoryManufacturingOrService(CategoryManufacturingOrService entity);

        int GetCountCategoriesManufacturingOrService();
        
        CategoryManufacturingOrService GetCategoryManufacturingOrServiceById(Guid id, bool track = false);

        CategoryManufacturingOrService GetCategoryManufacturingOrServiceByName(string name, bool track = false);

        IQueryable<CategoryManufacturingOrService> GetCategoriesManufacturingOrService(bool track = false);
        
        IQueryable<CategoryManufacturingOrService> GetCategoriesManufacturingOrService(int itemsPerPage, int numberPage,
            bool track = false);

        void DeleteCategoryManufacturingOrServiceById(Guid id);
    }
}