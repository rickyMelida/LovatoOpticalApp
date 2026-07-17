using LovatoOpticalApp.Application.DTOs;

namespace LovatoOpticalApp.Application.Interfaces
{
	public interface IFrameService
	{
		Task<ApiServiceResponse> CreateFrame(FrameRequestDto frame);
	}
}