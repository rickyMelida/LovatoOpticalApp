using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;

namespace LovatoOpticalApp.Application.Interfaces
{
	public interface IFrameService
	{
		Task<FrameResponseDto> GetFrameById(Guid frameId);
		Task<ApiServiceResponse> CreateFrame(FrameRequestDto frame);
		Task<List<FrameResponseDto>> GetFrames();
	}
}