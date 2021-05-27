using FactorialEK.Model.Database.Repositories.Abstract;

namespace FactorialEK.Model.Database
{
    public class DataManager
    {
        public ICategoriesManufacturingOrServiceRepository CategoriesManufacturingOrService { get; private set; }
        
        public ICategoryPhotosManufacturingOrServiceRepository CategoryPhotosManufacturingOrService { get; private set; }
        
        public IGalleryPhotosRepository GalleryPhotos { get; private set; }
        
        public IManufacturingOrServiceInformationRepository ManufacturingOrServiceInformation { get; private set; }
        
        public IManufacturingOrServiceMainPhotosRepository ManufacturingOrServiceMainPhotos { get; private set; }
        
        public IManufacturingOrServicePhotosRepository ManufacturingOrServicePhotos { get; private set; }
        
        public IManufacturingOrServicePricesRepository ManufacturingOrServicePrices { get; private set; }
        
        public IManufacturingOrServicesRepository ManufacturingOrServices { get; private set; }
        
        public INewsRepository News { get; private set; }
        
        public DataManager(ICategoriesManufacturingOrServiceRepository categoriesManufacturingOrService, ICategoryPhotosManufacturingOrServiceRepository categoryPhotosManufacturingOrService, IGalleryPhotosRepository galleryPhotos, IManufacturingOrServiceInformationRepository manufacturingOrServiceInformation, IManufacturingOrServiceMainPhotosRepository manufacturingOrServiceMainPhotos, IManufacturingOrServicePhotosRepository manufacturingOrServicePhotos, IManufacturingOrServicePricesRepository manufacturingOrServicePrices, IManufacturingOrServicesRepository manufacturingOrServices, INewsRepository news)
        {
            CategoriesManufacturingOrService = categoriesManufacturingOrService;
            CategoryPhotosManufacturingOrService = categoryPhotosManufacturingOrService;
            GalleryPhotos = galleryPhotos;
            ManufacturingOrServiceInformation = manufacturingOrServiceInformation;
            ManufacturingOrServiceMainPhotos = manufacturingOrServiceMainPhotos;
            ManufacturingOrServicePhotos = manufacturingOrServicePhotos;
            ManufacturingOrServicePrices = manufacturingOrServicePrices;
            ManufacturingOrServices = manufacturingOrServices;
            News = news;
        }
    }
}
