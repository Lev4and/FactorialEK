using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;
using FactorialEK.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FactorialEK.Model.Database.Repositories.EntityFramework
{
    public class EFManufacturingOrServicePhotosRepository : IManufacturingOrServicePhotosRepository
    {
        private readonly FactorialEKDbContext _context;

        public EFManufacturingOrServicePhotosRepository(FactorialEKDbContext context)
        {
            _context = context;
        }

        public bool ContainsManufacturingOrServicePhotoByManufacturingOrServiceIdAndUrl(Guid manufacturingOrServiceId, string url)
        {
            return _context.ManufacturingOrServicePhotos.SingleOrDefault(manufacturingOrServicePhoto =>
                manufacturingOrServicePhoto.ManufacturingOrServiceId == manufacturingOrServiceId &&
                manufacturingOrServicePhoto.Url == url) != null;
        }

        public bool SaveManufacturingOrServicePhoto(ManufacturingOrServicePhoto entity)
        {
            if (!ContainsManufacturingOrServicePhotoByManufacturingOrServiceIdAndUrl(entity.ManufacturingOrServiceId,
                entity.Url))
            {
                if (entity.Id == default)
                {
                    _context.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    _context.Entry(entity).State = EntityState.Modified;
                }

                _context.SaveChanges();
                
                return true;
            }

            return false;
        }

        public ManufacturingOrServicePhoto GetManufacturingOrServicePhotoById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.ManufacturingOrServicePhotos.SingleOrDefault(manufacturingOrServicePhoto =>
                    manufacturingOrServicePhoto.Id == id);
            }
            else
            {
                return _context.ManufacturingOrServicePhotos.AsNoTracking().SingleOrDefault(manufacturingOrServicePhoto =>
                    manufacturingOrServicePhoto.Id == id);
            }
        }

        public ManufacturingOrServicePhoto GetManufacturingOrServicePhotoByUrl(string url, bool track = false)
        {
            if (track)
            {
                return _context.ManufacturingOrServicePhotos.SingleOrDefault(manufacturingOrServicePhoto =>
                    manufacturingOrServicePhoto.Url == url);
            }
            else
            {
                return _context.ManufacturingOrServicePhotos.AsNoTracking().SingleOrDefault(manufacturingOrServicePhoto =>
                    manufacturingOrServicePhoto.Url == url);
            }
        }

        public IQueryable<ManufacturingOrServicePhoto> GetManufacturingOrServicePhotosByManufacturingOrServiceId(Guid manufacturingOrServiceId, bool track = false)
        {
            if (track)
            {
                return _context.ManufacturingOrServicePhotos
                    .Where(manufacturingOrServicePhoto =>
                        manufacturingOrServicePhoto.ManufacturingOrServiceId == manufacturingOrServiceId);
            }
            else
            {
                return _context.ManufacturingOrServicePhotos
                    .Where(manufacturingOrServicePhoto =>
                        manufacturingOrServicePhoto.ManufacturingOrServiceId == manufacturingOrServiceId)
                    .AsNoTracking();
            }
        }

        public void DeleteManufacturingOrServicePhotoById(Guid id)
        {
            _context.ManufacturingOrServicePhotos.Remove(GetManufacturingOrServicePhotoById(id));
            _context.SaveChanges();
        }

        public void DeleteAllManufacturingOrServicePhotosByManufacturingOrServiceId(Guid manufacturingOrServiceId)
        {
            var photos = GetManufacturingOrServicePhotosByManufacturingOrServiceId(manufacturingOrServiceId);

            _context.ManufacturingOrServicePhotos.RemoveRange(photos.ToArray());
            _context.SaveChanges();
        }
    }
}