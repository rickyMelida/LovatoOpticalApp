using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Application.Mappings;
using LovatoOpticalApp.Application.Services;
using LovatoOpticalApp.Persistence;
using LovatoOpticalApp.Persistence.Interfaces;
using LovatoOpticalApp.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LovatoOpticalApp.Application
{
	public static class ServiceExtension
	{
		public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
			services.AddScoped(typeof(IProductRepository<>), typeof(ProductRepository<>));
			services.AddScoped<ICustomerRepository, CustomerRepository>();
			services.AddScoped<IRecipeRepository, RecipeRepository>();
			services.AddScoped<ICrystalRepository, CrystalRepository>();
			services.AddScoped<IGlassesCaseRepository, GlassesCaseRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<IRecipeService, RecipeService>();
			services.AddScoped<IFrameService, FrameService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IAccessoryService, AccessoryService>();
			services.AddScoped<IProductDetailStrategy, FrameProductStrategy>();
			services.AddScoped<IProductDetailStrategy, AccessoryProductStrategy>();
			services.AddScoped<ICustomerRecipeUnitOfWork, CustomerRecipeUnitOfWork>();
			services.AddScoped<IOrderService, OrderService>();

			services.AddAutoMapper(cfg => 
			{
				cfg.AddProfile<CustomerProfile>();
				cfg.AddProfile<FrameProfile>();
				cfg.AddProfile<ProductProfile>();
				cfg.AddProfile<AccessoryProfile>();
			});
		}
	}
}
