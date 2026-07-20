using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Application.Interfaces
{
	public interface IProductService
	{
		Task<PagedResult<ProductResponse>> GetProducts(PaginationParams paginationParams);
		Task<ProductResponse> GetProductDetails(Guid productId, ProductTypeEnum productType);
	}
}