using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Application.Services
{
	public class AccessoryProductStrategy : IProductDetailStrategy
	{
		private readonly IAccessoryService _accessoryService;
		private readonly IMapper _mapper;

		public AccessoryProductStrategy(IAccessoryService accessoryService, IMapper mapper)
		{
			_accessoryService = accessoryService;
			_mapper = mapper;
		}

		public ProductTypeEnum Type => ProductTypeEnum.Accessory;

		public Task<ApiServiceResponse> AddStock(Guid productId, int quantityToAdd)
		{
			throw new NotImplementedException();
		}

		public async Task<ApiServiceResponse> DeleteProduct(Guid productId)
		{	
			try
			{
				return await _accessoryService.DeleteAccessory(productId);
			}
			catch (Exception ex)
			{
				return new ApiServiceResponse($"Error al eliminar el accesorio: {ex.Message}", 500);
			}
		}

		public async Task<ProductResponse> GetProductDetails(Guid productId)
		{
			var accessory = await _accessoryService.GetAccessoryById(productId);
			if (accessory == null)
				throw new KeyNotFoundException($"Accessory with ID {productId} not found.");

			return _mapper.Map<AccesoryResponseDto>(accessory);
		}
	}
}