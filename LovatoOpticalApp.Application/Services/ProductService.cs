using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Application.Services
{
	public class ProductService : IProductService
	{
		private readonly IFrameService _frameService;
		private readonly Dictionary<ProductTypeEnum, IProductDetailStrategy> _strategies;

		public ProductService(IFrameService frameService, IEnumerable<IProductDetailStrategy> strategies)
		{
			_frameService = frameService;
			_strategies = strategies.ToDictionary(s => s.Type);
		}

		public Task<ProductResponse> GetProductDetails(Guid productId, ProductTypeEnum productType)
		{
			var strategy = _strategies.GetValueOrDefault(productType);
			if (strategy == null)
				throw new InvalidOperationException("Tipo de producto no compatible");

			return strategy.GetProductDetails(productId);
		}

		public async Task<PagedResult<ProductResponse>> GetProducts(PaginationParams paginationParams)
		{
			var frames = await _frameService.GetFrames();
			var pageNumber = paginationParams.PageNumber > 0 ? paginationParams.PageNumber : 1;
			var pageSize = paginationParams.PageSize > 0 ? paginationParams.PageSize : 10;
			var totalCount = frames.Count();
			var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
			var safePageNumber = Math.Min(pageNumber, Math.Max(totalPages, 1));
			var skip = (safePageNumber - 1) * pageSize;

			var pagedItems = frames
				.Skip(skip)
				.Take(pageSize)
				.Select(f => new ProductResponse
				{
					Id = f.Id,
					Name = f.Name,
					PurchasePrice = f.PurchasePrice,
					SalePrice = f.SalePrice,
					Quantity = f.Quantity,
					MinimumQuantity = f.MinimumQuantity,
					Type = f.Type,
				})
				.ToList();

			return new PagedResult<ProductResponse>
			{
				Items = pagedItems,
				TotalCount = totalCount,
				PageNumber = safePageNumber,
				PageSize = pageSize
			};
		}
	}
}