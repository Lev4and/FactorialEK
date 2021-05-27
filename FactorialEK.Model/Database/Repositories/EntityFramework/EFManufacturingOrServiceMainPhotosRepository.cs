using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;
using FactorialEK.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FactorialEK.Model.Database.Repositories.EntityFramework
{
    public class EFManufacturingOrServiceMainPhotosRepository : IManufacturingOrServiceMainPhotosRepository
    {
        private readonly FactorialEKDbContext _context;

        public EFManufacturingOrServiceMainPhotosRepository(FactorialEKDbContext context)
        {
            _context = context;
        }

        public bool ContainsManufacturingOrServiceMainPhotoByManufacturingOrServiceId(Guid manufacturingOrServiceId)
        {
            return _context.ManufacturingOrServiceMainPhotos.SingleOrDefault(manufacturingOrServiceMainPhoto =>
                manufacturingOrServiceMainPhoto.ManufacturingOrServiceId == manufacturingOrServiceId) != null;
        }

        public bool SaveManufacturingOrServiceMainPhoto(ManufacturingOrServiceMainPhoto entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsManufacturingOrServiceMainPhotoByManufacturingOrServiceId(entity.ManufacturingOrServiceId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();
                    
                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetManufacturingOrServiceMainPhotoById(entity.Id);

                if (oldVersionEntity.ManufacturingOrServiceId != entity.ManufacturingOrServiceId)
                {
                    if (!ContainsManufacturingOrServiceMainPhotoByManufacturingOrServiceId(entity.ManufacturingOrServiceId))
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

        public ManufacturingOrServiceMainPhoto GetManufacturingOrServiceMainPhotoById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.ManufacturingOrServiceMainPhotos
                    .SingleOrDefault(manufacturingOrServiceMainPhoto => manufacturingOrServiceMainPhoto.Id == id);
            }
            else
            {
                return _context.ManufacturingOrServiceMainPhotos.AsNoTracking()
                    .SingleOrDefault(manufacturingOrServiceMainPhoto => manufacturingOrServiceMainPhoto.Id == id);
            }
        }

        public ManufacturingOrServiceMainPhoto GetManufacturingOrServiceMainPhotoByUrl(string url, bool track = false)
        {
            if (track)
            {
                return _context.ManufacturingOrServiceMainPhotos
                    .SingleOrDefault(manufacturingOrServiceMainPhoto => manufacturingOrServiceMainPhoto.Url == url);
            }
            else
            {
                return _context.ManufacturingOrServiceMainPhotos.AsNoTracking()
                    .SingleOrDefault(manufacturingOrServiceMainPhoto => manufacturingOrServiceMainPhoto.Url == url);
            }
        }

        public void DeleteManufacturingOrServiceMainPhotoById(Guid id)
        {
            _context.ManufacturingOrServiceMainPhotos.Remove(GetManufacturingOrServiceMainPhotoById(id));
            _context.SaveChanges();
        }
    }
}