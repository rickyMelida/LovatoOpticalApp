using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Enums;
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

        public async Task<ApiServiceResponse> DeleteFrame(Guid frameId)
        {
            try
            {
                await _repository.DeleteAsync(frameId);

                return new ApiServiceResponse("Armazón eliminado correctamente", 200);
            }
            catch(Exception ex)
            {
                return new ApiServiceResponse($"Error al eliminar el armazón: {ex.Message}", 500);
            }
        }

        public async Task<FrameResponseDto> GetFrameById(Guid frameId)
		{
			var frame = await _repository.GetByIdAsync(frameId);

			if (frame == null)
				throw new KeyNotFoundException($"Frame with ID {frameId} not found.");

			return _mapper.Map<FrameResponseDto>(frame);
		}

		public async Task<List<FrameResponseDto>> GetFrames()
		{
			var frames = await _repository.GetAllAsync();

			return _mapper.Map<List<FrameResponseDto>>(frames);
		}

        public async Task<ApiServiceResponse> AddStock(Guid frameId, int quantityToAdd)
        {
            var frameEntity = await _repository.GetByIdAsync(frameId);
            if (frameEntity is null)
                return new ApiServiceResponse("Armazón no encontrado.", 404);

            frameEntity.UpdateStock(frameEntity.Quantity + quantityToAdd);

            await _repository.UpdateAsync(frameEntity);

            return new ApiServiceResponse("Stock actualizado correctamente", 200);
        }

        public async Task<ApiServiceResponse> UpdateFrame(FrameRequestDto frame)
        {
            if (!Guid.TryParse(frame.Id, out var frameId))
                return new ApiServiceResponse("El ID del armazón no es válido.", 400);

            if (string.IsNullOrWhiteSpace(frame.Name) || string.IsNullOrWhiteSpace(frame.Code))
                return new ApiServiceResponse("El nombre y el código del armazón son obligatorios.", 400);

            if (string.IsNullOrWhiteSpace(frame.Material) || string.IsNullOrWhiteSpace(frame.FrameType))
                return new ApiServiceResponse("El material y el tipo de armazón son obligatorios.", 400);

            var frameEntity = await _repository.GetByIdAsync(frameId);
            if (frameEntity is null)
                return new ApiServiceResponse("Armazón no encontrado.", 404);

            var material = _mapper.Map<FrameMaterialEnum>(frame.Material);
            var frameType = _mapper.Map<FrameTypeEnum>(frame.FrameType);

            frameEntity.Update(
                frame.Name,
                frame.Code,
                material,
                frameType,
                frame.Color,
                frame.PurchasePrice,
                frame.SalePrice,
                frame.Quantity,
                frame.MinimumQuantity,
                frame.Description);

            await _repository.UpdateAsync(frameEntity);

            return new ApiServiceResponse("Armazón actualizado correctamente", 200);
        }

        public async Task<List<FrameResponseDto>> SearchFrames(string query)
        {
            var frames = await _repository.SearchAsync(query);

            return _mapper.Map<List<FrameResponseDto>>(frames);
        }
    }
}