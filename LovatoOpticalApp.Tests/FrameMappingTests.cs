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
    public void Map_FrameRequestDtoToFrame_WithSpanishFrameTypeName_MapsToEnum()
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
            FrameType = "Hilo",
            Color = "Green",
            PurchasePrice = 100,
            SalePrice = 150,
            Quantity = 10,
            MinimumQuantity = 1,
            Description = "Ninguna"
        };

        var frame = mapper.Map<Frame>(dto);

        Assert.Equal(FrameTypeEnum.Hilo, frame.FrameType);
        Assert.Equal(FrameMaterialEnum.Acetato, frame.Material);
    }

    [Fact]
    public void Map_FrameToFrameResponseDto_MapsSalePriceAndType()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<FrameProfile>();
        });

        using var serviceProvider = services.BuildServiceProvider();
        var mapper = serviceProvider.GetRequiredService<IMapper>();

        var frame = new Frame("Armazón", "A1", FrameMaterialEnum.Acetato, FrameTypeEnum.Hilo, "Green", 100, 150, 10, 1);

        var dto = mapper.Map<FrameResponseDto>(frame);

        Assert.Equal(frame.Id, dto.Id);
        Assert.Equal(frame.Name, dto.Name);
        Assert.Equal(100m, dto.PurchasePrice);
        Assert.Equal(150m, dto.SalePrice);
        Assert.Equal(150m, dto.Price);
        Assert.Equal(frame.Quantity, dto.Quantity);
        Assert.Equal(frame.MinimumQuantity, dto.MinimumQuantity);
        Assert.Equal(frame.Color, dto.Color);
        Assert.Equal(frame.Code, dto.Code);
        Assert.Equal(frame.Material, dto.Material);
        Assert.Equal(frame.FrameType, dto.FrameType);
        Assert.Equal(ProductTypeEnum.Frame, dto.Type);
        Assert.Equal(string.Empty, dto.Description);
    }
}
