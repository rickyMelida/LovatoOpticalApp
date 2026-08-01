using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;

namespace LovatoOpticalApp.Application.Interfaces
{
	public interface IAccessoryService
	{
		Task<List<AccesoryResponseDto>> GetAllAccessories();
		Task<AccesoryResponseDto> GetAccessoryById(Guid id);
		Task<ApiServiceResponse> CreateAccessory(AccessoryRequestDto accessoryDto);
		Task<ApiServiceResponse> UpdateAccessory(AccessoryRequestDto accessoryDto);
		Task<ApiServiceResponse> DeleteAccessory(Guid id);
	}
}