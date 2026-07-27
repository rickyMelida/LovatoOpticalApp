using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;

namespace LovatoOpticalApp.Application.Interfaces
{
	public interface IFrameService
	{
		Task<FrameResponseDto> GetFrameById(Guid frameId);
		Task<ApiServiceResponse> CreateFrame(FrameRequestDto frame);
		Task<ApiServiceResponse> UpdateFrame(FrameRequestDto frame);
		Task<ApiServiceResponse> DeleteFrame(Guid frameId);
		Task<List<FrameResponseDto>> GetFrames();
		Task<List<FrameResponseDto>> SearchFrames(string query);
		Task<ApiServiceResponse> AddStock(Guid frameId, int quantityToAdd);
	}
}