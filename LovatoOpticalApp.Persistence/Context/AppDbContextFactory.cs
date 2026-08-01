using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LovatoOpticalApp.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Construye la configuración leyendo appsettings.json
            // Ojo con la ruta base: dotnet ef corre desde el proyecto donde ejecutás el comando
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // Options "vacías": tu OnConfiguring se encarga de usar Npgsql con el connection string
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            return new AppDbContext(optionsBuilder.Options, configuration);
        }
    }
}