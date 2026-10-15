using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LovatoOpticalApp.Persistence.Repository
{
	public class CrystalPriceRepository : ICrystalPriceRepository
	{
		private readonly AppDbContext _context;
		public CrystalPriceRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<List<CrystalType>> GetCrystalPricesByTypeAsync(CrystalTypeEnum crystalType)
		{
			try
			{
				return await _context.CrystalTypes.Where(c => c.Type == crystalType).ToListAsync();
			}
			catch (Exception ex)
			{
				// Log the exception or handle it as needed
				throw new Exception("An error occurred while retrieving crystal prices.", ex);
			}
		}
	}
}