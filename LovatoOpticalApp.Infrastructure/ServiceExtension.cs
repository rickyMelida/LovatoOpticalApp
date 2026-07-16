using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Application.Mappings;
using LovatoOpticalApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LovatoOpticalApp.Application
{
    public static class ServiceExtension
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddAutoMapper(cfg => cfg.AddProfile<CustomerProfile>());
        }
    }
}
