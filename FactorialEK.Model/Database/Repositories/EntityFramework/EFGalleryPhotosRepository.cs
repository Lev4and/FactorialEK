using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;
using FactorialEK.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FactorialEK.Model.Database.Repositories.EntityFramework
{
    public class EFGalleryPhotosRepository : IGalleryPhotosRepository
    {
        private readonly FactorialEKDbContext _context;

        public EFGalleryPhotosRepository(FactorialEKDbContext context)
        {
            _context = context;
        }

        public bool ContainsGalleryPhotoByUrl(string url)
        {
            return _context.GalleryPhotos.SingleOrDefault(photo => photo.Url == url) != null;
        }

        public bool SaveGalleryPhoto(GalleryPhoto entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsGalleryPhotoByUrl(entity.Url))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetGalleryPhotoById(entity.Id);

                if (oldVersionEntity.Url != entity.Url)
                {
                    if (!ContainsGalleryPhotoByUrl(entity.Url))
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

        public GalleryPhoto GetGalleryPhotoById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.GalleryPhotos.SingleOrDefault(photo => photo.Id == id);
            }
            else
            {
                return _context.GalleryPhotos.AsNoTracking().SingleOrDefault(photo => photo.Id == id);
            }
        }

        public GalleryPhoto GetGalleryPhotoByUrl(string url, bool track = false)
        {
            if (track)
            {
                return _context.GalleryPhotos.SingleOrDefault(photo => photo.Url == url);
            }
            else
            {
                return _context.GalleryPhotos.AsNoTracking().SingleOrDefault(photo => photo.Url == url);
            }
        }

        public IQueryable<GalleryPhoto> GetGalleryPhotos(int itemsPerPage, int numberPage,bool track = false)
        {
            if (track)
            {
                return _context.GalleryPhotos
                    .OrderByDescending(photo => photo.CreatedAt)
                    .Skip((numberPage - 1) * itemsPerPage)
                    .Take(itemsPerPage);
            }
            else
            {
                return _context.GalleryPhotos
                    .OrderByDescending(photo => photo.CreatedAt)
                    .Skip((numberPage - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .AsNoTracking();
            }
        }

        public void DeleteGalleryPhotoById(Guid id)
        {
            _context.GalleryPhotos.Remove(GetGalleryPhotoById(id));
            _context.SaveChanges();
        }
    }
}