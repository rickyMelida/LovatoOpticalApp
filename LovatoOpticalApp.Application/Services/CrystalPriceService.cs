using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Persistence.Interfaces;

namespace LovatoOpticalApp.Application.Services
{
	public class CrystalPriceService : ICrystalPriceService
	{
		private readonly IMapper _mapper;
		private readonly ICrystalPriceRepository _crystalPriceRepository;

		public CrystalPriceService(IMapper mapper, ICrystalPriceRepository crystalPriceRepository)
		{
			_mapper = mapper;
			_crystalPriceRepository = crystalPriceRepository;
		}

		public async Task<CrystalTypeDto> GetCrystalPriceByTypeAsync(CrystalTypeEnum crystalType)
		{
			var crystalTypes = await _crystalPriceRepository.GetCrystalPricesByTypeAsync(crystalType);
			var result = _mapper.Map<CrystalTypeDto>(crystalTypes);
			return result;
		}
	}
}