using FactorialEK.AspNetCore.Service;
using FactorialEK.Model.Database;
using FactorialEK.Model.Database.Repositories.Abstract;
using FactorialEK.Model.Database.Repositories.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FactorialEK.AspNetCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.Bind("Project", new Config());

            services.AddTransient<ICategoriesManufacturingOrServiceRepository, EFCategoriesManufacturingOrServiceRepository>();
            services.AddTransient<ICategoryPhotosManufacturingOrServiceRepository, EFCategoryPhotosManufacturingOrServiceRepository>();
            services.AddTransient<IGalleryPhotosRepository, EFGalleryPhotosRepository>();
            services.AddTransient<IManufacturingOrServiceInformationRepository, EFManufacturingOrServiceInformationRepository>();
            services.AddTransient<IManufacturingOrServiceMainPhotosRepository, EFManufacturingOrServiceMainPhotosRepository>();
            services.AddTransient<IManufacturingOrServicePhotosRepository, EFManufacturingOrServicePhotosRepository>();
            services.AddTransient<IManufacturingOrServicePricesRepository, EFManufacturingOrServicePricesRepository>();
            services.AddTransient<IManufacturingOrServicesRepository, EFManufacturingOrServicesRepository>();
            services.AddTransient<INewsRepository, EFNewsRepository>();
            services.AddTransient<DataManager>();
            services.AddTransient<UploadFileService>();
            services.AddDbContext<FactorialEKDbContext>((options) =>
            {
                options.UseSqlServer(Config.ConnectionString);
            });
            
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<FactorialEKDbContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "factorialEKAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("Администратор"); });
            });

            services.AddControllersWithViews(x =>
            {
                x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "AdminArea",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
