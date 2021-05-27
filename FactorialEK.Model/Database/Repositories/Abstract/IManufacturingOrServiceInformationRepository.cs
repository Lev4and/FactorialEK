using System;
using FactorialEK.Model.Database.Entities;

namespace FactorialEK.Model.Database.Repositories.Abstract
{
    public interface IManufacturingOrServiceInformationRepository
    {
        bool ContainsManufacturingOrServiceInformationByManufacturingOrServiceId(Guid manufacturingOrServiceId);

        bool SaveManufacturingOrServiceInformation(ManufacturingOrServiceInformation entity);

        ManufacturingOrServiceInformation GetManufacturingOrServiceInformationById(Guid id, bool track = false);

        ManufacturingOrServiceInformation GetManufacturingOrServiceInformationByManufacturingOrServiceId(
            Guid manufacturingOrServiceId, bool track = false);

        void DeleteManufacturingOrServiceInformationById(Guid id);
    }
}