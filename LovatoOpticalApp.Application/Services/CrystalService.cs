using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Persistence;

namespace LovatoOpticalApp.Application.Services
{
	public class CrystalService : ICrystalService
	{
		private readonly IProductRepository<Crystal> _repository;
		private readonly IMapper _mapper;
		public CrystalService(IProductRepository<Crystal> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<Guid> CreateCrystal(CrystalDtoRequest crystalDto)
		{
			var crystal = _mapper.Map<Crystal>(crystalDto);
			await _repository.AddAsync(crystal);

			return crystal.Id;
		}

		public async Task<CrystalDtoResponse> GetCrystalById(Guid id)
		{
			var crystal = await _repository.GetByIdAsync(id);
			return _mapper.Map<CrystalDtoResponse>(crystal);
		}
	}
}