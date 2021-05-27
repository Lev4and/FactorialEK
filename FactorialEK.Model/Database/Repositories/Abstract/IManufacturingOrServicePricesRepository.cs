using System;
using FactorialEK.Model.Database.Entities;

namespace FactorialEK.Model.Database.Repositories.Abstract
{
    public interface IManufacturingOrServicePricesRepository
    {
        bool ContainsManufacturingOrServicePriceByManufacturingOrServiceId(Guid manufacturingOrServiceId);

        bool SaveManufacturingOrServicePrice(ManufacturingOrServicePrice entity);

        ManufacturingOrServicePrice GetManufacturingOrServicePriceById(Guid id, bool track = false);
        
        ManufacturingOrServicePrice GetManufacturingOrServicePriceByManufacturingOrServiceId(Guid manufacturingOrServiceId, 
            bool track = false);

        void DeleteManufacturingOrServicePriceById(Guid id);
    }
}