using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Application.Interfaces
{
	public interface IProductTypeResolver
	{
		Task<ProductTypeEnum?> ResolveTypeAsync(int id);
	}
}