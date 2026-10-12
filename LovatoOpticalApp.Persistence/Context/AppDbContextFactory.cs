using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace LovatoOpticalApp.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var configurationDirectory = new[]
                {
                    currentDirectory,
                    Path.Combine(currentDirectory, "LovatoOpticalApp"),
                    Path.Combine(currentDirectory, "..", "LovatoOpticalApp")
                }
                .Select(Path.GetFullPath)
                .FirstOrDefault(path => File.Exists(Path.Combine(path, "appsettings.json")));

            if (configurationDirectory is null)
            {
                throw new InvalidOperationException("No se encontró appsettings.json en el directorio actual ni en el proyecto LovatoOpticalApp.");
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(configurationDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("No se encontró la cadena ConnectionStrings:DefaultConnection en la configuración de diseño.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options, configuration);
        }
    }
}