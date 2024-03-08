using BAL.Services.Auth;
using BAL.Services.category;
using BAL.Services.Category;
using BAL.Services.Department;
using BAL.Services.Designation;
using BAL.Services.Image;
using BAL.Services.Land;
using BAL.Services.Marriage;
using BAL.Services.View;
using Microsoft.Extensions.DependencyInjection;

namespace BAL.DependencyResolver
{
    public static class DIResolver
    {
        public static IServiceCollection DIBALResolver(this IServiceCollection services)
        {
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IMarriageService, MarriageService>();
            services.AddScoped<ILandService, LandService>();

            services.AddScoped<IViewService, ViewService>();



            return services;
        }
    }
}
