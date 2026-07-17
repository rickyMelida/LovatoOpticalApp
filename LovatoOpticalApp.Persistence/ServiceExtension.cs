
using LovatoOpticalApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LovatoOpticalApp.Persistence
{
    public static class ServiceExtension
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresConnection");
            
            services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));

			services.AddScoped<IProductRepository<Frame>, ProductRepository<Frame>>();

            return services;
        }
    }
}