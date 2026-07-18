using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;

namespace LovatoOpticalApp.Application.Services
{
	public class ProductService : IProductService
	{
		private readonly IFrameService _frameService;

		public ProductService(IFrameService frameService)
		{
			_frameService = frameService;
		}

		public async Task<PagedResult<ProductResponse>> GetProducts(PaginationParams paginationParams)
		{
			var frames = await _frameService.GetFrames();
			return new PagedResult<ProductResponse>
			{
				Items = frames.Select(f => new ProductResponse
				{
					Id = f.Id,
					Name = f.Name,
					Description = f.Description,
					Price = f.Price
				}).ToList(),
				TotalCount = frames.Count(),
				PageNumber = paginationParams.PageNumber,
				PageSize = paginationParams.PageSize
			};
		}
	}
}