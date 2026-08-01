using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Mappings;
using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Entities.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace LovatoOpticalApp.Tests;

public class AccessoryMappingTests
{
    [Fact]
    public void Map_AccessoryRequestDtoToAccessory_SetsType()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<AccessoryProfile>();
        });

        using var serviceProvider = services.BuildServiceProvider();
        var mapper = serviceProvider.GetRequiredService<IMapper>();

        var dto = new AccessoryRequestDto
        {
            Type = ProductTypeEnum.Accessory,
            Name = "Lente de sol",
            PurchasePrice = 100,
            SalePrice = 150,
            Quantity = 10,
            MinimumQuantity = 1,
            Description = "Accesorio de prueba"
        };

        var accessory = mapper.Map<Accessory>(dto);

        Assert.Equal(ProductTypeEnum.Accessory, accessory.Type);
    }
}
