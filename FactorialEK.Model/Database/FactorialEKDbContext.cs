using FactorialEK.Model.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FactorialEK.Model.Database
{
    public class FactorialEKDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<CategoryManufacturingOrService> CategoriesManufacturingOrService { get; set; }
        
        public DbSet<CategoryPhotoManufacturingOrService> CategoryPhotosManufacturingOrService { get; set; }
        
        public DbSet<GalleryPhoto> GalleryPhotos { get; set; }
        
        public DbSet<ManufacturingOrService> ManufacturingOrServices { get; set; }
        
        public DbSet<ManufacturingOrServiceInformation> ManufacturingOrServiceInformation { get; set; }
        
        public DbSet<ManufacturingOrServiceMainPhoto> ManufacturingOrServiceMainPhotos { get; set; }
        
        public DbSet<ManufacturingOrServicePhoto> ManufacturingOrServicePhotos { get; set; }
        
        public DbSet<ManufacturingOrServicePrice> ManufacturingOrServicePrices { get; set; }
        
        public DbSet<News> News { get; set; }

        public FactorialEKDbContext(DbContextOptions<FactorialEKDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-9CDGA5B;Database=FactorialEK;User ID=sa;Password=sa;Trusted_Connection=True;", b => b.MigrationsAssembly("FactorialEK.AspNetCore"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "B867520A-92DB-4658-BE39-84DA53A601C0",
                Name = "Администратор",
                NormalizedName = "АДМИНИСТРАТОР"
            });

            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "21F7B496-C675-4E8A-A34C-FC5EC0762FDB",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "egor.yaremenko.1998@gmail.com",
                NormalizedEmail = "EGOR.YAREMENKO.1998@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "EgorYaremenko29081998Alberkaz"),
                SecurityStamp = string.Empty
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "B867520A-92DB-4658-BE39-84DA53A601C0",
                UserId = "21F7B496-C675-4E8A-A34C-FC5EC0762FDB"
            });

            builder.Entity<CategoryManufacturingOrService>()
                .HasOne(category => category.Photo)
                .WithOne(photo => photo.CategoryManufacturingOrService)
                .HasForeignKey<CategoryPhotoManufacturingOrService>(photo => photo.CategoryManufacturingOrServiceId);

            builder.Entity<CategoryManufacturingOrService>()
                .HasMany(category => category.ManufacturingOrServices)
                .WithOne(manufacturingOrService => manufacturingOrService.Category)
                .HasForeignKey(manufacturingOrService => manufacturingOrService.CategoryManufacturingOrServiceId);

            builder.Entity<ManufacturingOrService>()
                .HasOne(manufacturingOrService => manufacturingOrService.Information)
                .WithOne(manufacturingOrServiceInformation => manufacturingOrServiceInformation.ManufacturingOrService)
                .HasForeignKey<ManufacturingOrServiceInformation>(manufacturingOrServiceInformation =>
                    manufacturingOrServiceInformation.ManufacturingOrServiceId);

            builder.Entity<ManufacturingOrService>()
                .HasOne(manufacturingOrService => manufacturingOrService.MainPhoto)
                .WithOne(manufacturingOrServiceMainPhoto => manufacturingOrServiceMainPhoto.ManufacturingOrService)
                .HasForeignKey<ManufacturingOrServiceMainPhoto>(manufacturingOrServiceMainPhoto =>
                    manufacturingOrServiceMainPhoto.ManufacturingOrServiceId);

            builder.Entity<ManufacturingOrService>()
                .HasOne(manufacturingOrService => manufacturingOrService.Price)
                .WithOne(manufacturingOrServicePrice => manufacturingOrServicePrice.ManufacturingOrService)
                .HasForeignKey<ManufacturingOrServicePrice>(manufacturingOrServicePrice =>
                    manufacturingOrServicePrice.ManufacturingOrServiceId);

            builder.Entity<ManufacturingOrService>()
                .HasMany(manufacturingOrService => manufacturingOrService.Photos)
                .WithOne(manufacturingOrServicePhoto => manufacturingOrServicePhoto.ManufacturingOrService)
                .HasForeignKey(manufacturingOrServicePhoto => manufacturingOrServicePhoto.ManufacturingOrServiceId);
        }
    }
}
