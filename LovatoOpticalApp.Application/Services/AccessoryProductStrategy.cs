using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Application.Services
{
	public class AccessoryProductStrategy : IProductDetailStrategy
	{
		private readonly IAccessoryService _accessoryService;

		public AccessoryProductStrategy(IAccessoryService accessoryService)
		{
			_accessoryService = accessoryService ?? throw new ArgumentNullException(nameof(accessoryService));
		}

		public ProductTypeEnum Type => ProductTypeEnum.Accessory;

		public Task<ApiServiceResponse> AddStock(Guid productId, int quantityToAdd)
		{
			throw new NotImplementedException();
		}

		public Task<ApiServiceResponse> DeleteProduct(Guid productId)
		{
			throw new NotImplementedException();
		}

		public async Task<ProductResponse> GetProductDetails(Guid productId)
		{
			var accessory = await _accessoryService.GetAccessoryById(productId);
			if (accessory == null)
				throw new KeyNotFoundException($"Accessory with ID {productId} not found.");

			return new ProductResponse
			{
				Id = accessory.Id,
				Name = accessory.Name,
				PurchasePrice = accessory.PurchasePrice,
				SalePrice = accessory.SalePrice,
				Quantity = accessory.Quantity,
				MinimumQuantity = accessory.MinimumQuantity,
				Type = accessory.Type,
				Description = accessory.Description
			};
		}
	}
}