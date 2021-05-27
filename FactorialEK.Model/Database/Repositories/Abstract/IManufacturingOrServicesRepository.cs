using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;

namespace FactorialEK.Model.Database.Repositories.Abstract
{
    public interface IManufacturingOrServicesRepository
    {
        bool ContainsManufacturingOrServiceByCategoryAndName(Guid categoryId, string name);

        bool SaveManufacturingOrService(ManufacturingOrService entity);

        int GetCountManufacturingOrServices(Guid? categoryId, string searchString);

        ManufacturingOrService GetManufacturingOrServiceById(Guid id, bool track = false);

        IQueryable<ManufacturingOrService> GetManufacturingOrServices(Guid? categoryId, string searchString, int itemsPerPage, int numberPage,
            bool track = false);

        void DeleteManufacturingOrServiceById(Guid id);
    }
}