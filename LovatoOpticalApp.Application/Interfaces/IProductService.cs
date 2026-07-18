using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;

namespace LovatoOpticalApp.Application.Interfaces
{
	public interface IProductService
	{
		Task<PagedResult<ProductResponse>> GetProducts(PaginationParams paginationParams);
	}
}