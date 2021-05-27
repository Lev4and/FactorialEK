using System;
using System.Linq;
using FactorialEK.Model.Database.Entities;

namespace FactorialEK.Model.Database.Repositories.Abstract
{
    public interface IGalleryPhotosRepository
    {
        bool ContainsGalleryPhotoByUrl(string url);

        bool SaveGalleryPhoto(GalleryPhoto entity);

        GalleryPhoto GetGalleryPhotoById(Guid id, bool track = false);

        GalleryPhoto GetGalleryPhotoByUrl(string url, bool track = false);

        IQueryable<GalleryPhoto> GetGalleryPhotos(int itemsPerPage, int numberPage, bool track = false);

        void DeleteGalleryPhotoById(Guid id);
    }
}