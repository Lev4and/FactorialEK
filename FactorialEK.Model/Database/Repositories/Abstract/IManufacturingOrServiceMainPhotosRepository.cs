using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;

namespace FactorialEK.Model.Database.Repositories.Abstract
{
    public interface IManufacturingOrServiceMainPhotosRepository
    {
        bool ContainsManufacturingOrServiceMainPhotoByManufacturingOrServiceId(Guid manufacturingOrServiceId);

        bool SaveManufacturingOrServiceMainPhoto(ManufacturingOrServiceMainPhoto entity);

        ManufacturingOrServiceMainPhoto GetManufacturingOrServiceMainPhotoById(Guid id, bool track = false);

        ManufacturingOrServiceMainPhoto GetManufacturingOrServiceMainPhotoByUrl(string url, bool track = false);

        void DeleteManufacturingOrServiceMainPhotoById(Guid id);
    }
}