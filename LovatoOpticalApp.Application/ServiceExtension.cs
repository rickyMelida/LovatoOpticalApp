using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Application.Mappings;
using LovatoOpticalApp.Application.Services;
using LovatoOpticalApp.Persistence;
using LovatoOpticalApp.Persistence.Interfaces;
using LovatoOpticalApp.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace LovatoOpticalApp.Application
{
    public static class ServiceExtension
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services.AddScoped(typeof(IProductRepository<>), typeof(ProductRepository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IFrameService, FrameService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IProductDetailStrategy, FrameProductStrategy>();
            services.AddAutoMapper(cfg => 
            {
                cfg.AddProfile<CustomerProfile>();
                cfg.AddProfile<FrameProfile>();
				cfg.AddProfile<ProductProfile>();
            });
        }
    }
}
