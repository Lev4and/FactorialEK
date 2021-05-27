using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;

namespace FactorialEK.Model.Database.Repositories.Abstract
{
    public interface IManufacturingOrServicePhotosRepository
    {
        bool ContainsManufacturingOrServicePhotoByManufacturingOrServiceIdAndUrl(Guid manufacturingOrServiceId,
            string url);

        bool SaveManufacturingOrServicePhoto(ManufacturingOrServicePhoto entity);

        ManufacturingOrServicePhoto GetManufacturingOrServicePhotoById(Guid id, bool track = false);

        ManufacturingOrServicePhoto GetManufacturingOrServicePhotoByUrl(string url, bool track = false);

        IQueryable<ManufacturingOrServicePhoto> GetManufacturingOrServicePhotosByManufacturingOrServiceId(
            Guid manufacturingOrServiceId, bool track = false);

        void DeleteManufacturingOrServicePhotoById(Guid id);
    }
}