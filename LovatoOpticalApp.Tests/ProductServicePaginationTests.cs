using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Application.Services;
using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Tests;

public class ProductServicePaginationTests
{
    [Fact]
    public async Task GetProducts_ReturnsRequestedPage()
    {
        var frameService = new FakeFrameService();
        var service = new ProductService(frameService);

        var result = await service.GetProducts(new PaginationParams { PageNumber = 2, PageSize = 2 });

        Assert.Equal(2, result.PageNumber);
        Assert.Equal(2, result.PageSize);
        Assert.Equal(3, result.TotalPages);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal("Frame 3", result.Items[0].Name);
        Assert.Equal("Frame 4", result.Items[1].Name);
    }

    private sealed class FakeFrameService : IFrameService
    {
        public Task<ApiServiceResponse> CreateFrame(FrameRequestDto frame) => throw new NotImplementedException();

        public Task<List<FrameResponseDto>> GetFrames()
        {
            var frames = new List<FrameResponseDto>
            {
                CreateFrameDto("Frame 1"),
                CreateFrameDto("Frame 2"),
                CreateFrameDto("Frame 3"),
                CreateFrameDto("Frame 4"),
                CreateFrameDto("Frame 5")
            };

            return Task.FromResult(frames);
        }

        private static FrameResponseDto CreateFrameDto(string name) => new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            PurchasePrice = 10m,
            SalePrice = 20m,
            Quantity = 5,
            MinimumQuantity = 1,
            Type = ProductTypeEnum.Frame
        };

		public Task<FrameResponseDto> GetFrameById(Guid frameId)
		{
			throw new NotImplementedException();
		}

		public Task<ApiServiceResponse> UpdateFrame(FrameRequestDto frame)
		{
			throw new NotImplementedException();
		}

		public Task<ApiServiceResponse> DeleteFrame(Guid frameId)
		{
			throw new NotImplementedException();
		}

		public Task<List<FrameResponseDto>> SearchFrames(string query)
		{
			throw new NotImplementedException();
		}

		public Task<ApiServiceResponse> AddStock(Guid frameId, int quantityToAdd)
		{
			throw new NotImplementedException();
		}
	}
}
