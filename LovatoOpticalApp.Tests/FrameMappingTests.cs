using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Mappings;
using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Entities.Enums;
using LovatoOpticalApp.Core.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace LovatoOpticalApp.Tests;

public class FrameMappingTests
{
    [Fact]
    public void Map_FrameRequestDtoToFrame_WithSpanishShapeName_MapsToEnum()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<FrameProfile>();
        });

        using var serviceProvider = services.BuildServiceProvider();
        var mapper = serviceProvider.GetRequiredService<IMapper>();

        var dto = new FrameRequestDto
        {
            Type = ProductTypeEnum.Frame,
            Name = "Armazón",
            Code = "A1",
            Material = "Acetato",
            Shape = "Cuadrado",
            Color = "Green",
            PurchasePrice = 100,
            SalePrice = 150,
            Quantity = 10,
            MinimumQuantity = 1,
            Description = "Ninguna"
        };

        var frame = mapper.Map<Frame>(dto);

        Assert.Equal(FrameShapeEnum.Square, frame.Shape);
        Assert.Equal(FrameMaterialEnum.Acetato, frame.Material);
    }
}
