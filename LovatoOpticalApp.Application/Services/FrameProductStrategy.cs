using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Application.Services
{
	public class FrameProductStrategy : IProductDetailStrategy
	{
		private readonly IFrameService _frameService;
		private readonly IMapper _mapper;

		public FrameProductStrategy(IFrameService frameService, IMapper mapper)
		{
			_frameService = frameService;
			_mapper = mapper;
		}

		public ProductTypeEnum Type => ProductTypeEnum.Frame;

        public async Task<ApiServiceResponse> DeleteProduct(Guid productId)
        {
            return await _frameService.DeleteFrame(productId);
        }

        public async Task<ApiServiceResponse> AddStock(Guid productId, int quantityToAdd)
        {
            return await _frameService.AddStock(productId, quantityToAdd);
        }

        public async Task<ProductResponse> GetProductDetails(Guid productId)
		{
			var frame = await _frameService.GetFrameById(productId);

			if (frame == null)
				throw new KeyNotFoundException($"Frame with ID {productId} not found.");

			return _mapper.Map<FrameResponseDto>(frame);
		}
	}
}