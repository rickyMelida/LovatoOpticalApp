using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Application.Interfaces
{
	public interface IProductDetailStrategy
	{
		ProductTypeEnum Type { get; }
		Task<ProductResponse> GetProductDetails(Guid productId);
	}
}