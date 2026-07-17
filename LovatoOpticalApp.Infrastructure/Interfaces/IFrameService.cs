using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;

namespace LovatoOpticalApp.Application.Interfaces
{
	public interface IFrameService
	{
		Task<ApiServiceResponse> CreateFrame(FrameRequestDto frame);
	}
}