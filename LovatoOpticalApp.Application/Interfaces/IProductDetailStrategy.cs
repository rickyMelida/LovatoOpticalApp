using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Application.Interfaces
{
	public interface IProductDetailStrategy
	{
		ProductTypeEnum Type { get; }
		Task<ProductResponse> GetProductDetails(Guid productId);
		Task<ApiServiceResponse> DeleteProduct(Guid productId);
		Task<ApiServiceResponse> AddStock(Guid productId, int quantityToAdd);
	}
}