using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Enums;

namespace LovatoOpticalApp.Persistence.Interfaces
{
	public interface ICrystalPriceRepository
	{
		Task<CrystalType> GetCrystalPricesByTypeAsync(CrystalTypeEnum crystalType);
		Task<List<CrystalBrand>> GetMultifocalPrices();
	}
}