using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Mappings;
using LovatoOpticalApp.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace LovatoOpticalApp.Tests;

public class CrystalBrandMappingTests
{
    [Fact]
    public void Map_CrystalBrandToDto_IncludesPricesAndBrandForeignKey()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<CrystalProfile>();
        });

        using var serviceProvider = services.BuildServiceProvider();
        var mapper = serviceProvider.GetRequiredService<IMapper>();
        var brandId = Guid.NewGuid();
        var brand = new CrystalBrand
        {
            Id = brandId,
            Name = "Essilor",
            Prices =
            [
                new CrystalBrandPrice
                {
                    Id = Guid.NewGuid(),
                    CrystalBrandId = brandId,
                    Price = 125000m
                }
            ]
        };

        var result = mapper.Map<CrystalBrandDto>(brand);

        var price = Assert.Single(result.Prices);
        Assert.Equal(brandId, result.Id);
        Assert.Equal(brandId, price.CrystalBrandId);
        Assert.Equal(125000m, price.Price);
    }
}
