using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Persistence;

namespace LovatoOpticalApp.Application.Services
{
	public class AccessoryService : IAccessoryService
	{
		private readonly IProductRepository<Accessory> _repository;
		private readonly IMapper _mapper;
		public AccessoryService(IProductRepository<Accessory> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<ApiServiceResponse> CreateAccessory(AccessoryRequestDto accessoryDto)
		{
			var accessory = _mapper.Map<Accessory>(accessoryDto);
			await _repository.AddAsync(accessory);

			return new ApiServiceResponse("Accesorio creado correctamente", 200);
		}

		public async Task<ApiServiceResponse> DeleteAccessory(Guid id)
		{
			try
			{
				await _repository.DeleteAsync(id);
				return new ApiServiceResponse("Accesorio eliminado correctamente", 200);
			}
			catch (Exception)
			{
				return new ApiServiceResponse("Error al eliminar el accesorio", 500);
			}
		}

		public async Task<AccesoryResponseDto> GetAccessoryById(Guid id)
		{
			var accessory = await _repository.GetByIdAsync(id);
			return _mapper.Map<AccesoryResponseDto>(accessory);
		}

		public async Task<List<AccesoryResponseDto>> GetAllAccessories()
		{
			var accessories = await _repository.GetAllAsync();

			return _mapper.Map<List<AccesoryResponseDto>>(accessories);
		}

		public async Task<ApiServiceResponse> UpdateAccessory(AccessoryRequestDto accessoryDto)
		{
			var accessory = await _repository.GetByIdAsync(accessoryDto.Id);
			if (accessory == null)
			{
				return new ApiServiceResponse("Accesorio no encontrado", 404);
			}

			_mapper.Map(accessoryDto, accessory);
			await _repository.UpdateAsync(accessory);

			return new ApiServiceResponse("Accesorio actualizado correctamente", 200);
			
		}
	}
}