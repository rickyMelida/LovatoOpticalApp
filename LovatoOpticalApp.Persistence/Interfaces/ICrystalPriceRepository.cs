using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Enums;

namespace LovatoOpticalApp.Persistence.Interfaces
{
	public interface ICrystalPriceRepository
	{
		Task<List<CrystalType>> GetCrystalPricesByTypeAsync(CrystalTypeEnum crystalType);
	}
}