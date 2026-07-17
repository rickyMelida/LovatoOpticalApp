using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Persistence;

namespace LovatoOpticalApp.Application.Services
{


	public class FrameService : IFrameService
	{
		private readonly IProductRepository<Frame> _repository;
		private readonly IMapper _mapper;

		public FrameService(IProductRepository<Frame> repository, IMapper mapper) =>
			(_repository, _mapper) = (repository, mapper);

		public async Task<ApiServiceResponse> CreateFrame(FrameRequestDto frame)
		{
			var frameEntity = _mapper.Map<Frame>(frame);
			await _repository.AddAsync(frameEntity);

			return new ApiServiceResponse("Armazon creado correctamente", 200);
		}
	}
}